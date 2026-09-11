using TradingApp.Domain.Common;

namespace TradingApp.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // e.g., "OrderExecuted", "PriceAlert"
    public bool IsRead { get; set; } = false;

    // Navigation Property
    public User User { get; set; } = null!;
}
