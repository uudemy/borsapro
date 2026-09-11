using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingApp.Domain.Entities;

namespace TradingApp.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.AssetId);
        builder.HasIndex(x => x.Status);

        builder.Property(x => x.Price).HasColumnType("decimal(18,8)");
        builder.Property(x => x.Quantity).HasColumnType("decimal(24,8)");
        builder.Property(x => x.FilledQuantity).HasColumnType("decimal(24,8)");
        builder.Property(x => x.RemainingQuantity).HasColumnType("decimal(24,8)");
        builder.Property(x => x.AverageFillPrice).HasColumnType("decimal(18,8)");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Asset)
            .WithMany()
            .HasForeignKey(x => x.AssetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
