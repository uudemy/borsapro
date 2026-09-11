using TradingApp.Application.DTOs.Wallet;

namespace TradingApp.Application.Interfaces.Services;

public interface IWalletService
{
    Task<WalletResponse> GetWalletAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<WalletResponse> DepositAsync(Guid userId, decimal amount, CancellationToken cancellationToken = default);
    Task<WalletResponse> WithdrawAsync(Guid userId, decimal amount, CancellationToken cancellationToken = default);
}
