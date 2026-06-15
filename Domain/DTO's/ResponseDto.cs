namespace Domain.DTO_s;

public record ResponseDto(
    Guid ResponseId,
    Guid TicketId,
    Guid ResponderId,
    string Message,
    DateTime? CreatedAt
);