using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingApp.Domain.Entities;

namespace TradingApp.Infrastructure.Persistence.Configurations;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasKey(x => x.Id);

        // A user can have one wallet per currency (e.g., one TRY wallet, one USD wallet)
        builder.HasIndex(x => new { x.UserId, x.Currency }).IsUnique();

        builder.Property(x => x.Currency).IsRequired().HasMaxLength(10);
        builder.Property(x => x.AvailableBalance).HasColumnType("decimal(24,8)");
        builder.Property(x => x.LockedBalance).HasColumnType("decimal(24,8)");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Wallets)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
