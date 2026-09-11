using Microsoft.EntityFrameworkCore;
using TradingApp.Application.Interfaces.Repositories;
using TradingApp.Domain.Entities;

namespace TradingApp.Infrastructure.Persistence.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly TradingDbContext _context;

    public WalletRepository(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<Wallet?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId, cancellationToken);
    }

    public async Task AddAsync(Wallet wallet, CancellationToken cancellationToken = default)
    {
        await _context.Wallets.AddAsync(wallet, cancellationToken);
    }

    public void Update(Wallet wallet)
    {
        _context.Wallets.Update(wallet);
    }
}
