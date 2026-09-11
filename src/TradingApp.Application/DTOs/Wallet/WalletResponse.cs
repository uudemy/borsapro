namespace TradingApp.Application.DTOs.Wallet;

public class WalletResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }
    public string Currency { get; set; } = string.Empty;
}
