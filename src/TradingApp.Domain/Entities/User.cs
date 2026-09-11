using TradingApp.Domain.Common;

namespace TradingApp.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public bool IsEmailVerified { get; set; } = false;

    // Navigation Properties
    public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<PortfolioPosition> PortfolioPositions { get; set; } = new List<PortfolioPosition>();
}
