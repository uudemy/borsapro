using TradingApp.Domain.Enums;

namespace TradingApp.Application.DTOs.Transaction;

public class TransactionHistoryResponse
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty; // "Deposit", "Withdraw", "Buy Market", "Sell Market"
    public string AssetSymbol { get; set; } = string.Empty; // "USD" for deposits, "BTC" for trades
    public decimal Amount { get; set; } // Quantity of asset or USD amount
    public decimal Price { get; set; } // Price per unit (0 for deposit/withdraw)
    public decimal TotalValue { get; set; } // Total USD value
    public DateTime CreatedAt { get; set; }
}
