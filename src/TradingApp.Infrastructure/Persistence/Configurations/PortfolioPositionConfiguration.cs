using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingApp.Domain.Entities;

namespace TradingApp.Infrastructure.Persistence.Configurations;

public class PortfolioPositionConfiguration : IEntityTypeConfiguration<PortfolioPosition>
{
    public void Configure(EntityTypeBuilder<PortfolioPosition> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.UserId, x.AssetId }).IsUnique();

        builder.Property(x => x.Quantity).HasColumnType("decimal(24,8)");
        builder.Property(x => x.AverageBuyPrice).HasColumnType("decimal(18,8)");

        builder.HasOne(x => x.User)
            .WithMany(x => x.PortfolioPositions)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Asset)
            .WithMany()
            .HasForeignKey(x => x.AssetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
