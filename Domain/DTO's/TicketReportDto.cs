namespace Domain.DTO_s;

using System;

using System;

public class TicketReportDto(
    Guid ticketId,
    Guid userId,
    string title,
    string? description,
    string status,
    DateTime? createdAt,
    DateTime? closedAt,
    Guid? categoryId)
{
    public Guid TicketId { get; } = ticketId;
    public Guid UserId { get; } = userId;
    public string Title { get; } = title;
    public string? Description { get; } = description;
    public string Status { get; } = status;
    public DateTime? CreatedAt { get; } = createdAt;
    public DateTime? ClosedAt { get; } = closedAt;
    public Guid? CategoryId { get; } = categoryId;
}