namespace Domain.Entities;

public partial class AuditLog
{
    public Guid LogId { get; set; }

    public Guid? UserId { get; set; }

    public string Action { get; set; } = null!;

    public string? Entity { get; set; }

    public Guid? EntityId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
