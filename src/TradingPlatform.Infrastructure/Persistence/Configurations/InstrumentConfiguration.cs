using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingPlatform.Domain.Entities;

namespace TradingPlatform.Infrastructure.Persistence.Configurations;

public class InstrumentConfiguration : IEntityTypeConfiguration<Instrument>
{
    public void Configure(EntityTypeBuilder<Instrument> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Ticker)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(i => i.AssetType)
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(i => i.TickSize)
            .HasColumnType("decimal(18,8)");

        builder.HasIndex(i => i.Ticker)
            .IsUnique(); // no duplicate tickers
    }
}