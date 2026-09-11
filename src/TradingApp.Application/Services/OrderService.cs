using TradingApp.Application.DTOs.Order;
using TradingApp.Application.Interfaces.Repositories;
using TradingApp.Application.Interfaces.Services;
using TradingApp.Domain.Entities;
using TradingApp.Domain.Enums;

namespace TradingApp.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OrderResponse>> GetOrdersAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var orders = await _unitOfWork.Orders.GetByUserIdAsync(userId, cancellationToken);
        return orders.Select(o => new OrderResponse
        {
            Id = o.Id,
            AssetSymbol = o.Asset.Symbol,
            Side = o.Side,
            OrderType = o.OrderType,
            Status = o.Status,
            Quantity = o.Quantity,
            FilledQuantity = o.FilledQuantity,
            AverageFillPrice = o.AverageFillPrice,
            CreatedAt = o.CreatedAt
        });
    }

    public async Task<OrderResponse> PlaceMarketOrderAsync(Guid userId, PlaceOrderRequest request, CancellationToken cancellationToken = default)
    {
        var asset = await _unitOfWork.Assets.GetBySymbolAsync(request.AssetSymbol, cancellationToken);
        if (asset == null || !asset.IsActive)
            throw new Exception("Asset not found or inactive.");

        var wallet = await _unitOfWork.Wallets.GetByUserIdAsync(userId, cancellationToken);
        if (wallet == null)
            throw new Exception("Wallet not found.");

        var totalValue = request.Quantity * asset.CurrentPrice;

        var order = new Order
        {
            UserId = userId,
            AssetId = asset.Id,
            Side = request.Side,
            OrderType = OrderType.Market,
            Status = OrderStatus.Filled, // Assuming instant fill for market orders
            Quantity = request.Quantity,
            FilledQuantity = request.Quantity,
            AverageFillPrice = asset.CurrentPrice,
            CreatedAt = DateTime.UtcNow
        };

        if (request.Side == OrderSide.Buy)
        {
            if (wallet.AvailableBalance < totalValue)
                throw new Exception("Insufficient balance to place buy order.");

            wallet.AvailableBalance -= totalValue;
            _unitOfWork.Wallets.Update(wallet);

            var position = await _unitOfWork.PortfolioPositions.GetByUserIdAndAssetIdAsync(userId, asset.Id, cancellationToken);
            if (position == null)
            {
                position = new PortfolioPosition
                {
                    UserId = userId,
                    AssetId = asset.Id,
                    Quantity = request.Quantity,
                    AverageBuyPrice = asset.CurrentPrice
                };
                await _unitOfWork.PortfolioPositions.AddAsync(position, cancellationToken);
            }
            else
            {
                // Calculate new average buy price
                var totalCost = (position.Quantity * position.AverageBuyPrice) + totalValue;
                position.Quantity += request.Quantity;
                position.AverageBuyPrice = totalCost / position.Quantity;
                _unitOfWork.PortfolioPositions.Update(position);
            }
        }
        else if (request.Side == OrderSide.Sell)
        {
            var position = await _unitOfWork.PortfolioPositions.GetByUserIdAndAssetIdAsync(userId, asset.Id, cancellationToken);
            if (position == null || position.Quantity < request.Quantity)
                throw new Exception("Insufficient asset quantity to sell.");

            position.Quantity -= request.Quantity;
            _unitOfWork.PortfolioPositions.Update(position);

            wallet.AvailableBalance += totalValue;
            _unitOfWork.Wallets.Update(wallet);
        }

        await _unitOfWork.Orders.AddAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new OrderResponse
        {
            Id = order.Id,
            AssetSymbol = asset.Symbol,
            Side = order.Side,
            OrderType = order.OrderType,
            Status = order.Status,
            Quantity = order.Quantity,
            FilledQuantity = order.FilledQuantity,
            AverageFillPrice = order.AverageFillPrice,
            CreatedAt = order.CreatedAt
        };
    }
}
