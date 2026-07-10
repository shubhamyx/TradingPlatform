using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingPlatform.Domain.Entities;

namespace TradingPlatform.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Side)
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(o => o.Type)
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(o => o.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(o => o.TimeInForce)
            .HasConversion<string>()
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(o => o.Quantity)
            .HasColumnType("decimal(18,8)");

        builder.Property(o => o.RemainingQuantity)
            .HasColumnType("decimal(18,8)");

        builder.Property(o => o.Price)
            .HasColumnType("decimal(18,8)");

        builder.HasIndex(o => o.UserId);
        builder.HasIndex(o => o.InstrumentId);
        builder.HasIndex(o => o.Status);

        builder.HasOne<Instrument>()
            .WithMany()
            .HasForeignKey(o => o.InstrumentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}