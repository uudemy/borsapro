using TradingApp.Application.Interfaces.Repositories;

namespace TradingApp.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly TradingDbContext _context;

    public UnitOfWork(TradingDbContext context, IUserRepository users, IWalletRepository wallets, IAssetRepository assets)
    {
        _context = context;
        Users = users;
        Wallets = wallets;
        Assets = assets;
    }

    public IUserRepository Users { get; }
    public IWalletRepository Wallets { get; }
    public IAssetRepository Assets { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
