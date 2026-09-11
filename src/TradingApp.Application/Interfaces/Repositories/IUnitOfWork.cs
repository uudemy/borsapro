namespace TradingApp.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IWalletRepository Wallets { get; }
    IAssetRepository Assets { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
