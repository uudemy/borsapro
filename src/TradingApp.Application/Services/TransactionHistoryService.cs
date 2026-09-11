using TradingApp.Application.DTOs.Transaction;
using TradingApp.Application.Interfaces.Repositories;
using TradingApp.Application.Interfaces.Services;

namespace TradingApp.Application.Services;

public class TransactionHistoryService : ITransactionHistoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionHistoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TransactionHistoryResponse>> GetUserTransactionsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var orders = await _unitOfWork.Orders.GetByUserIdAsync(userId, cancellationToken);
        
        // Şimdilik sadece Emirleri (Order) getiriyoruz. Gelişmiş bir sistemde Cüzdan hareketleri (Deposit/Withdraw) 
        // için ayrı bir tablo (Ledger/Transaction) tutulur. Bu prototipte siparişleri işlem geçmişi olarak sunuyoruz.
        
        var transactions = orders.Select(o => new TransactionHistoryResponse
        {
            Id = o.Id,
            Type = o.Side == Domain.Enums.OrderSide.Buy ? "Buy Market" : "Sell Market",
            AssetSymbol = o.Asset.Symbol,
            Amount = o.FilledQuantity,
            Price = o.AverageFillPrice,
            TotalValue = o.FilledQuantity * o.AverageFillPrice,
            CreatedAt = o.CreatedAt
        });

        return transactions.OrderByDescending(t => t.CreatedAt);
    }
}
