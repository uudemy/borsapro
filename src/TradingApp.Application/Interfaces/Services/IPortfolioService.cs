using TradingApp.Application.DTOs.Portfolio;

namespace TradingApp.Application.Interfaces.Services;

public interface IPortfolioService
{
    Task<IEnumerable<PortfolioPositionResponse>> GetPortfolioAsync(Guid userId, CancellationToken cancellationToken = default);
}
