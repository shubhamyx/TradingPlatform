using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingPlatform.Domain.Entities;

namespace TradingPlatform.Infrastructure.Persistence.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Quantity)
            .HasColumnType("decimal(18,8)");

        builder.Property(p => p.AvgEntryPrice)
            .HasColumnType("decimal(18,8)");

        builder.HasIndex(p => new { p.UserId, p.InstrumentId })
            .IsUnique(); // one position row per user+instrument pair

        builder.HasOne<Instrument>()
            .WithMany()
            .HasForeignKey(p=>p.InstrumentId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}