namespace Domain.DTO_s;

public sealed record TicketResponseDto(
    Guid ResponseId,
    Guid ResponderId,
    string ResponderUsername,
    string Message,
    DateTime? CreatedAt);