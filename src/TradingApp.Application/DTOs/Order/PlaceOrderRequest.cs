using System.ComponentModel.DataAnnotations;
using TradingApp.Domain.Enums;

namespace TradingApp.Application.DTOs.Order;

public class PlaceOrderRequest
{
    [Required]
    public string AssetSymbol { get; set; } = string.Empty;

    [Required]
    public OrderSide Side { get; set; }

    [Required]
    [Range(0.00000001, double.MaxValue)]
    public decimal Quantity { get; set; }
}
