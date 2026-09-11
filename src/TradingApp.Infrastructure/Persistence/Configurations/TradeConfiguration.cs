using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingApp.Domain.Entities;

namespace TradingApp.Infrastructure.Persistence.Configurations;

public class TradeConfiguration : IEntityTypeConfiguration<Trade>
{
    public void Configure(EntityTypeBuilder<Trade> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.AssetId);
        builder.HasIndex(x => x.CreatedAt);

        builder.Property(x => x.Price).HasColumnType("decimal(18,8)");
        builder.Property(x => x.Quantity).HasColumnType("decimal(24,8)");

        builder.HasOne(x => x.BuyOrder)
            .WithMany()
            .HasForeignKey(x => x.BuyOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.SellOrder)
            .WithMany()
            .HasForeignKey(x => x.SellOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Asset)
            .WithMany()
            .HasForeignKey(x => x.AssetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
