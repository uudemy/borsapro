using TradingApp.Domain.Common;

namespace TradingApp.Domain.Entities;

public class PortfolioPosition : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid AssetId { get; set; }
    public decimal Quantity { get; set; }
    public decimal AverageBuyPrice { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public Asset Asset { get; set; } = null!;
}
