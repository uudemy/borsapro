using Microsoft.Extensions.Logging;
using TradingApp.Application.Interfaces.Repositories;
using TradingApp.Application.Interfaces.Services;

namespace TradingApp.Infrastructure.Services;

public class MarketDataService : IMarketDataService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MarketDataService> _logger;
    private readonly Random _random = new Random();

    public MarketDataService(IUnitOfWork unitOfWork, ILogger<MarketDataService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task UpdateAssetPricesAsync(CancellationToken cancellationToken = default)
    {
        var assets = await _unitOfWork.Assets.GetAllAsync(cancellationToken);

        foreach (var asset in assets)
        {
            // Fiyatı rastgele % -2 ile +2 arasında değiştir
            var changePercentage = (decimal)(_random.NextDouble() * 4 - 2) / 100m;
            var changeAmount = asset.CurrentPrice * changePercentage;
            
            asset.PreviousClose = asset.CurrentPrice;
            asset.CurrentPrice = Math.Round(asset.CurrentPrice + changeAmount, 2);

            if(asset.CurrentPrice <= 0)
                asset.CurrentPrice = 0.01m; // Fiyat eksiye düşmesin

            //_logger.LogInformation($"Updated {asset.Symbol} price to {asset.CurrentPrice}");
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
