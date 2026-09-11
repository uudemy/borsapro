using TradingApp.Domain.Common;

namespace TradingApp.Domain.Entities;

public class PriceAlert : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid AssetId { get; set; }
    
    public decimal TargetPrice { get; set; }
    public bool IsGreaterThan { get; set; } // true: price > TargetPrice, false: price < TargetPrice
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public User User { get; set; } = null!;
    public Asset Asset { get; set; } = null!;
}
