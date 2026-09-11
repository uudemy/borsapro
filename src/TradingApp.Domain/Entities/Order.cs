using TradingApp.Domain.Common;
using TradingApp.Domain.Enums;

namespace TradingApp.Domain.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid AssetId { get; set; }
    
    public OrderSide Side { get; set; }
    public OrderType OrderType { get; set; }
    public OrderStatus Status { get; set; }
    
    public decimal Price { get; set; } // Required for Limit, 0 for Market
    public decimal Quantity { get; set; }
    public decimal FilledQuantity { get; set; }
    public decimal RemainingQuantity { get; set; }
    public decimal AverageFillPrice { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public Asset Asset { get; set; } = null!;
}
