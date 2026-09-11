using TradingApp.Domain.Enums;

namespace TradingApp.Application.DTOs.Order;

public class OrderResponse
{
    public Guid Id { get; set; }
    public string AssetSymbol { get; set; } = string.Empty;
    public OrderSide Side { get; set; }
    public OrderType OrderType { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Quantity { get; set; }
    public decimal FilledQuantity { get; set; }
    public decimal AverageFillPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}
