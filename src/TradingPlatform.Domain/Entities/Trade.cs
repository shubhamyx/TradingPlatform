using TradingPlatform.Domain.Enums;

namespace TradingPlatform.Domain.Entities;

public class Trade
{
    public Guid Id { get; private set; }
    public Guid BuyOrderId { get; private set; }
    public Guid SellOrderId { get; private set; }
    public Guid InstrumentId { get; private set; }
    public decimal Price { get; private set; }
    public decimal Quantity { get; private set; }
    public DateTime ExecutedAt { get; private set; }

    private Trade() { } // for EF Core

    public Trade(Guid buyOrderId, Guid sellOrderId, Guid instrumentId,
                 decimal price, decimal quantity)
    {
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero.", nameof(price));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        Id = Guid.NewGuid();
        BuyOrderId = buyOrderId;
        SellOrderId = sellOrderId;
        InstrumentId = instrumentId;
        Price = price;
        Quantity = quantity;
        ExecutedAt = DateTime.UtcNow;
    }
}