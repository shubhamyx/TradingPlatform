using TradingPlatform.Domain.Enums;

namespace TradingPlatform.Domain.Entities;

public class Instrument
{
    public Guid Id { get; private set; }
    public string Ticker { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public AssetType AssetType { get; private set; }
    public decimal TickSize { get; private set; }

    private Instrument() { } // for EF Core

    public Instrument(string ticker, string name, AssetType assetType, decimal tickSize)
    {
        if (string.IsNullOrWhiteSpace(ticker))
            throw new ArgumentException("Ticker cannot be empty.", nameof(ticker));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        if (tickSize <= 0)
            throw new ArgumentException("Tick size must be greater than zero.", nameof(tickSize));

        Id = Guid.NewGuid();
        Ticker = ticker.Trim().ToUpperInvariant();
        Name = name.Trim();
        AssetType = assetType;
        TickSize = tickSize;
    }
}