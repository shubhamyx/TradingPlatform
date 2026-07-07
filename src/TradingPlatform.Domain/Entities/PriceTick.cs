namespace TradingPlatform.Domain.Entities;

public class PriceTick
{
    public Guid Id { get; private set; }
    public Guid InstrumentId { get; private set; }
    public decimal Price { get; private set; }
    public DateTime Timestamp { get; private set; }

    private PriceTick() { } // for EF Core

    public PriceTick(Guid instrumentId, decimal price, DateTime timestamp)
    {
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero.", nameof(price));

        Id = Guid.NewGuid();
        InstrumentId = instrumentId;
        Price = price;
        Timestamp = timestamp;
    }
}