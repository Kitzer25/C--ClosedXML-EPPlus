namespace Domain.DTO_s;

public sealed record TicketDetailDto(
    Guid TicketId,
    string Title,
    string? Description,
    string Status,
    DateTime? CreatedAt,
    DateTime? ClosedAt,
    Guid UserId,
    string Username,
    Guid? CategoryId,
    string? CategoryName,
    IReadOnlyList<TicketResponseDto> Responses);