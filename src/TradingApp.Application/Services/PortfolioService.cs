using TradingApp.Application.DTOs.Portfolio;
using TradingApp.Application.Interfaces.Repositories;
using TradingApp.Application.Interfaces.Services;

namespace TradingApp.Application.Services;

public class PortfolioService : IPortfolioService
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PortfolioPositionResponse>> GetPortfolioAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var positions = await _unitOfWork.PortfolioPositions.GetByUserIdAsync(userId, cancellationToken);
        
        return positions.Select(p => new PortfolioPositionResponse
        {
            AssetSymbol = p.Asset.Symbol,
            Quantity = p.Quantity,
            AverageBuyPrice = p.AverageBuyPrice,
            CurrentPrice = p.Asset.CurrentPrice
        });
    }
}
