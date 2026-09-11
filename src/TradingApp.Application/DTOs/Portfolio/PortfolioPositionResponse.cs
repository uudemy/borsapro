namespace TradingApp.Application.DTOs.Portfolio;

public class PortfolioPositionResponse
{
    public string AssetSymbol { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal AverageBuyPrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal TotalValue => Quantity * CurrentPrice;
    public decimal ProfitLoss => (CurrentPrice - AverageBuyPrice) * Quantity;
}
