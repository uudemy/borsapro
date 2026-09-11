using TradingApp.Application.DTOs.Transaction;

namespace TradingApp.Application.Interfaces.Services;

public interface ITransactionHistoryService
{
    Task<IEnumerable<TransactionHistoryResponse>> GetUserTransactionsAsync(Guid userId, CancellationToken cancellationToken = default);
}
