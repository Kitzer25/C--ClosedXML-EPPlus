namespace Domain.Entities;

public partial class Attachment
{
    public Guid AttachmentId { get; set; }

    public Guid? TicketId { get; set; }

    public string FileName { get; set; } = null!;

    public string FileUrl { get; set; } = null!;

    public DateTime? UploadedAt { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
