using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingApp.Application.Interfaces.Repositories;
using TradingApp.Domain.Entities;
using TradingApp.Domain.Enums;

namespace TradingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AssetController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAssets(CancellationToken cancellationToken)
    {
        var assets = await _unitOfWork.Assets.GetAllAsync(cancellationToken);
        return Ok(assets);
    }

    [HttpPost("seed")]
    [AllowAnonymous]
    public async Task<IActionResult> SeedAssets(CancellationToken cancellationToken)
    {
        var existingAssets = await _unitOfWork.Assets.GetAllAsync(cancellationToken);
        if (existingAssets.Any())
            return Ok(new { Message = "Assets already seeded." });

        var assets = new List<Asset>
        {
            new Asset { Symbol = "BTC", Name = "Bitcoin", AssetType = AssetType.Crypto, Currency = "USD", CurrentPrice = 64000, PreviousClose = 62000, DailyVolume = 1000000 },
            new Asset { Symbol = "ETH", Name = "Ethereum", AssetType = AssetType.Crypto, Currency = "USD", CurrentPrice = 3400, PreviousClose = 3300, DailyVolume = 500000 },
            new Asset { Symbol = "AAPL", Name = "Apple Inc.", AssetType = AssetType.Stock, Currency = "USD", CurrentPrice = 185, PreviousClose = 180, DailyVolume = 5000000 },
            new Asset { Symbol = "TSLA", Name = "Tesla Inc.", AssetType = AssetType.Stock, Currency = "USD", CurrentPrice = 210, PreviousClose = 220, DailyVolume = 8000000 }
        };

        await _unitOfWork.Assets.AddRangeAsync(assets, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(new { Message = "Assets seeded successfully." });
    }
}
