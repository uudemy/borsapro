using TradingApp.Domain.Entities;

namespace TradingApp.Application.Interfaces.Repositories;

public interface IAssetRepository
{
    Task<IEnumerable<Asset>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Asset?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken = default);
}
