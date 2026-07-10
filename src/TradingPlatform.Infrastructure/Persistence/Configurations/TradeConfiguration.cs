using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingPlatform.Domain.Entities;

namespace TradingPlatform.Infrastructure.Persistence.Configurations;

public class TradeConfiguration : IEntityTypeConfiguration<Trade>
{
    public void Configure(EntityTypeBuilder<Trade> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Price)
            .HasColumnType("decimal(18,8)");

        builder.Property(t => t.Quantity)
            .HasColumnType("decimal(18,8)");

        builder.HasIndex(t => t.InstrumentId);
        builder.HasIndex(t => t.ExecutedAt);

        builder.HasOne<Instrument>()
            .WithMany()
            .HasForeignKey(t => t.InstrumentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(t => t.BuyOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(t => t.SellOrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}