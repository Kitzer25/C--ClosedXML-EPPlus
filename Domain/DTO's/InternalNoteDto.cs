namespace Domain.DTO_s;

public sealed record InternalNoteDto(
    Guid NoteId,
    Guid TicketId,
    Guid AuthorId,
    string AuthorUsername,
    string Note,
    DateTime? CreatedAt);