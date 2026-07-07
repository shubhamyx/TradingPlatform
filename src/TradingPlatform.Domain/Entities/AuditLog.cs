namespace TradingPlatform.Domain.Entities;

public class AuditLog
{
    public Guid Id { get; private set; }
    public Guid? UserId { get; private set; } // nullable: system actions may have no user
    public string Action { get; private set; } = default!;
    public string EntityName { get; private set; } = default!;
    public Guid EntityId { get; private set; }
    public string? Details { get; private set; }
    public DateTime Timestamp { get; private set; }

    private AuditLog() { } // for EF Core

    public AuditLog(Guid? userId, string action, string entityName, Guid entityId, string? details = null)
    {
        if (string.IsNullOrWhiteSpace(action))
            throw new ArgumentException("Action cannot be empty.", nameof(action));

        if (string.IsNullOrWhiteSpace(entityName))
            throw new ArgumentException("Entity name cannot be empty.", nameof(entityName));

        Id = Guid.NewGuid();
        UserId = userId;
        Action = action;
        EntityName = entityName;
        EntityId = entityId;
        Details = details;
        Timestamp = DateTime.UtcNow;
    }
}