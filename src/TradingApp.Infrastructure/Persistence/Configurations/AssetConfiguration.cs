using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingApp.Domain.Entities;

namespace TradingApp.Infrastructure.Persistence.Configurations;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.Symbol).IsUnique();
        
        builder.Property(x => x.Symbol).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Currency).IsRequired().HasMaxLength(10);
        
        builder.Property(x => x.CurrentPrice).HasColumnType("decimal(18,8)");
        builder.Property(x => x.PreviousClose).HasColumnType("decimal(18,8)");
        builder.Property(x => x.DailyVolume).HasColumnType("decimal(24,8)");
    }
}
