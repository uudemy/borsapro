using TradingApp.Domain.Entities;

namespace TradingApp.Application.Interfaces.Repositories;

public interface IPortfolioPositionRepository
{
    Task<IEnumerable<PortfolioPosition>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<PortfolioPosition?> GetByUserIdAndAssetIdAsync(Guid userId, Guid assetId, CancellationToken cancellationToken = default);
    Task AddAsync(PortfolioPosition position, CancellationToken cancellationToken = default);
    void Update(PortfolioPosition position);
}
