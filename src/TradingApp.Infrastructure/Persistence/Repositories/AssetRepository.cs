using Microsoft.EntityFrameworkCore;
using TradingApp.Application.Interfaces.Repositories;
using TradingApp.Domain.Entities;

namespace TradingApp.Infrastructure.Persistence.Repositories;

public class AssetRepository : IAssetRepository
{
    private readonly TradingDbContext _context;

    public AssetRepository(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Asset>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Assets.ToListAsync(cancellationToken);
    }

    public async Task<Asset?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken = default)
    {
        return await _context.Assets.FirstOrDefaultAsync(a => a.Symbol == symbol, cancellationToken);
    }
}
