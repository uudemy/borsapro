using TradingApp.Domain.Common;

namespace TradingApp.Domain.Entities;

public class Watchlist : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid AssetId { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public Asset Asset { get; set; } = null!;
}
