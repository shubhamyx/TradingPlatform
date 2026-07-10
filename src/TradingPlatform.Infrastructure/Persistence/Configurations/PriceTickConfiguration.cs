using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingPlatform.Domain.Entities;

namespace TradingPlatform.Infrastructure.Persistence.Configurations;

public class PriceTickConfiguration : IEntityTypeConfiguration<PriceTick>
{
    public void Configure(EntityTypeBuilder<PriceTick> builder)
    {
        builder.HasKey(pt => pt.Id);

        builder.Property(pt => pt.Price)
            .HasColumnType("decimal(18,8)");

        builder.HasIndex(pt => new { pt.InstrumentId, pt.Timestamp });

        builder.HasOne<Instrument>()
            .WithMany()
            .HasForeignKey(pt => pt.InstrumentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}