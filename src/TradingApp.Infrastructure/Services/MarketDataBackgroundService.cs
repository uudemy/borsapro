using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TradingApp.Application.Interfaces.Services;

namespace TradingApp.Infrastructure.Services;

public class MarketDataBackgroundService : BackgroundService
{
    private readonly ILogger<MarketDataBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(10); // Her 10 saniyede bir

    public MarketDataBackgroundService(ILogger<MarketDataBackgroundService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Market Data Background Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var marketDataService = scope.ServiceProvider.GetRequiredService<IMarketDataService>();
                    await marketDataService.UpdateAssetPricesAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating market data.");
            }

            await Task.Delay(_updateInterval, stoppingToken);
        }

        _logger.LogInformation("Market Data Background Service is stopping.");
    }
}
