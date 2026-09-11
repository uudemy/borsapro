using Microsoft.EntityFrameworkCore;
using TradingApp.Domain.Entities;

namespace TradingApp.Infrastructure.Persistence;

public class TradingDbContext : DbContext
{
    public TradingDbContext(DbContextOptions<TradingDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } => Set<User>();
    public DbSet<Asset> Assets { get; set; } => Set<Asset>();
    public DbSet<Wallet> Wallets { get; set; } => Set<Wallet>();
    public DbSet<Order> Orders { get; set; } => Set<Order>();
    public DbSet<Trade> Trades { get; set; } => Set<Trade>();
    public DbSet<PortfolioPosition> PortfolioPositions { get; set; } => Set<PortfolioPosition>();
    public DbSet<Watchlist> Watchlists { get; set; } => Set<Watchlist>();
    public DbSet<PriceAlert> PriceAlerts { get; set; } => Set<PriceAlert>();
    public DbSet<Notification> Notifications { get; set; } => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TradingDbContext).Assembly);
    }
}
