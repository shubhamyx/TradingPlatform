using TradingPlatform.Domain.Enums;

namespace TradingPlatform.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid InstrumentId { get; private set; }
    public OrderSide Side { get; private set; }
    public OrderType Type { get; private set; }
    public decimal Quantity { get; private set; }

    public decimal RemainingQuantity { get; private set; }
    public decimal? Price { get; private set; }
    public OrderStatus Status { get; private set; }
    public TimeInForce TimeInForce { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Order() { } // for EF Core

    public Order(Guid userId, Guid instrumentId, OrderSide side, OrderType type,
                 decimal quantity, decimal? price, TimeInForce timeInForce)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        if (type == OrderType.Limit && price is null)
            throw new ArgumentException("Limit orders require a price.", nameof(price));

        if (type == OrderType.Market && price is not null)
            throw new ArgumentException("Market orders must not have a price.", nameof(price));

        Id = Guid.NewGuid();
        UserId = userId;
        InstrumentId = instrumentId;
        Side = side;
        Type = type;
        Quantity = quantity;
        RemainingQuantity = quantity;
        Price = price;
        TimeInForce = timeInForce;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void ApplyFill(decimal fillQuantity)
    {
        if (fillQuantity <= 0)
            throw new ArgumentException("Fill quantity must be greater than zero.", nameof(fillQuantity));

        if (fillQuantity > RemainingQuantity)
            throw new InvalidOperationException("Fill quantity exceeds remaining order quantity.");

        if (Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Cannot fill a cancelled order.");

        RemainingQuantity -= fillQuantity;

        Status = RemainingQuantity == 0
            ? OrderStatus.Filled
            : OrderStatus.PartiallyFilled;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Filled)
            throw new InvalidOperationException("Cannot cancel a fully filled order.");

        Status = OrderStatus.Cancelled;
    }
}