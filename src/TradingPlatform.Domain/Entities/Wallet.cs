namespace TradingPlatform.Domain.Entities;

public class Wallet
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public decimal Balance { get; private set; }
    public string Currency { get; private set; } = "USD";
    public byte[] RowVersion { get; private set; } = default!; // concurrency token

    private Wallet() { } // for EF Core

    public Wallet(Guid userId, decimal startingBalance, string currency = "USD")
    {
        if (startingBalance < 0)
            throw new ArgumentException("Starting balance cannot be negative.", nameof(startingBalance));

        Id = Guid.NewGuid();
        UserId = userId;
        Balance = startingBalance;
        Currency = currency;
    }

    public void Debit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Debit amount must be greater than zero.", nameof(amount));

        if (amount > Balance)
            throw new InvalidOperationException("Insufficient balance for this debit.");

        Balance -= amount;
    }

    public void Credit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Credit amount must be greater than zero.", nameof(amount));

        Balance += amount;
    }
}