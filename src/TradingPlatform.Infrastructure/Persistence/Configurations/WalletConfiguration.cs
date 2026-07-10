using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingPlatform.Domain.Entities;

namespace TradingPlatform.Infrastructure.Persistence.Configurations;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.Balance)
            .HasColumnType("decimal(18,8)");

        builder.Property(w => w.Currency)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(w => w.RowVersion)
            .IsRowVersion(); // <-- this is the concurrency token, finally wired up

        builder.HasIndex(w => w.UserId)
            .IsUnique(); // one wallet per user
    }
}