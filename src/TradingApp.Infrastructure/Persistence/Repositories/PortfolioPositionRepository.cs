using Microsoft.EntityFrameworkCore;
using TradingApp.Application.Interfaces.Repositories;
using TradingApp.Domain.Entities;

namespace TradingApp.Infrastructure.Persistence.Repositories;

public class PortfolioPositionRepository : IPortfolioPositionRepository
{
    private readonly TradingDbContext _context;

    public PortfolioPositionRepository(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PortfolioPosition>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.PortfolioPositions
            .Include(p => p.Asset)
            .Where(p => p.UserId == userId && p.Quantity > 0)
            .ToListAsync(cancellationToken);
    }

    public async Task<PortfolioPosition?> GetByUserIdAndAssetIdAsync(Guid userId, Guid assetId, CancellationToken cancellationToken = default)
    {
        return await _context.PortfolioPositions
            .FirstOrDefaultAsync(p => p.UserId == userId && p.AssetId == assetId, cancellationToken);
    }

    public async Task AddAsync(PortfolioPosition position, CancellationToken cancellationToken = default)
    {
        await _context.PortfolioPositions.AddAsync(position, cancellationToken);
    }

    public void Update(PortfolioPosition position)
    {
        _context.PortfolioPositions.Update(position);
    }
}
