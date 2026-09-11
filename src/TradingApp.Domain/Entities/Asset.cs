using TradingApp.Domain.Common;
using TradingApp.Domain.Enums;

namespace TradingApp.Domain.Entities;

public class Asset : BaseEntity
{
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public AssetType AssetType { get; set; }
    public string Currency { get; set; } = string.Empty; // e.g., TRY, USD, USDT
    public decimal CurrentPrice { get; set; }
    public decimal PreviousClose { get; set; }
    public decimal DailyVolume { get; set; }
    public bool IsActive { get; set; } = true;
}
