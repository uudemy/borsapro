using Microsoft.EntityFrameworkCore;
using TradingApp.Domain.Entities;

namespace TradingApp.Infrastructure.Persistence;

public class TradingDbContext : DbContext
{
    public TradingDbContext(DbContextOptions<TradingDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Trade> Trades { get; set; }
    public DbSet<PortfolioPosition> PortfolioPositions { get; set; }
    public DbSet<Watchlist> Watchlists { get; set; }
    public DbSet<PriceAlert> PriceAlerts { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TradingDbContext).Assembly);
    }
}
