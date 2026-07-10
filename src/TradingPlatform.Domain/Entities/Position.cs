

namespace TradingPlatform.Domain.Entities;

public class Position
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid InstrumentId { get; private set; }

    public decimal Quantity { get; private set; }
    public decimal AvgEntryPrice { get; private set; }

    private Position() { } // for EF Core

    public Position(Guid userId, Guid instrumentId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        InstrumentId = instrumentId;
        Quantity = 0;
        AvgEntryPrice = 0;
    }

    public void Increase(decimal fillQuantity, decimal fillPrice)
    {
        if (fillQuantity <= 0)
            throw new ArgumentException("Fill quantity must be greater than zero.", nameof(fillQuantity));

        decimal totalCost = (Quantity * AvgEntryPrice) + (fillQuantity * fillPrice);
        Quantity += fillQuantity;
        AvgEntryPrice = totalCost / Quantity;
    }

    public void Decrease(decimal fillQuantity)
    {
        if (fillQuantity <= 0)
            throw new ArgumentException("Fill quantity must be greater than zero.", nameof(fillQuantity));

        if (fillQuantity > Quantity)
            throw new InvalidOperationException("Cannot reduce position below zero.");

        Quantity -= fillQuantity;

        if (Quantity == 0)
            AvgEntryPrice = 0;
    }
}