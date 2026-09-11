using TradingApp.Domain.Common;

namespace TradingApp.Domain.Entities;

public class Trade : BaseEntity
{
    public Guid BuyOrderId { get; set; }
    public Guid SellOrderId { get; set; }
    public Guid AssetId { get; set; }
    public Guid BuyerUserId { get; set; }
    public Guid SellerUserId { get; set; }
    
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }

    // Navigation Properties
    public Order BuyOrder { get; set; } = null!;
    public Order SellOrder { get; set; } = null!;
    public Asset Asset { get; set; } = null!;
}
