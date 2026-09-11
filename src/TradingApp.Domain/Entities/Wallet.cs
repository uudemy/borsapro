using TradingApp.Domain.Common;

namespace TradingApp.Domain.Entities;

public class Wallet : BaseEntity
{
    public Guid UserId { get; set; }
    public string Currency { get; set; } = string.Empty; // TRY, USD, USDT
    public decimal AvailableBalance { get; set; }
    public decimal LockedBalance { get; set; }

    // Navigation Property
    public User User { get; set; } = null!;
}
