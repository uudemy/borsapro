using System.ComponentModel.DataAnnotations;

namespace TradingApp.Application.DTOs.Wallet;

public class WithdrawRequest
{
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }
}
