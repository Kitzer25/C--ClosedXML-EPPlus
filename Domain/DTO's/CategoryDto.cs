namespace Domain.DTO_s;

public sealed record CategoryDto(
    Guid CategoryId,
    string Name,
    string? Description,
    int TicketCount);