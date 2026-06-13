namespace Domain.DTO_s;

public sealed record TicketDto(
    Guid TicketId,
    string Title,
    string? Description,
    string Status,
    DateTime? CreatedAt,
    DateTime? ClosedAt,
    Guid UserId,
    string Username,
    Guid? CategoryId,
    string? CategoryName);