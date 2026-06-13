namespace Domain.Entities;

public partial class InternalNote
{
    public Guid NoteId { get; set; }

    public Guid TicketId { get; set; }

    public Guid AuthorId { get; set; }

    public string Note { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
