using TradingApp.Application.DTOs.Order;

namespace TradingApp.Application.Interfaces.Services;

public interface IOrderService
{
    Task<OrderResponse> PlaceMarketOrderAsync(Guid userId, PlaceOrderRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrderResponse>> GetOrdersAsync(Guid userId, CancellationToken cancellationToken = default);
}
