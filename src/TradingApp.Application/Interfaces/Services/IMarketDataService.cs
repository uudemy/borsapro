namespace TradingApp.Application.Interfaces.Services;

public interface IMarketDataService
{
    Task UpdateAssetPricesAsync(CancellationToken cancellationToken = default);
}
