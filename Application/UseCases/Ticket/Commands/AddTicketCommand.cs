using Domain.Entities;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Ticket.Commands;

public class AddTicketCommand : IRequest<string>
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public ListiTicket Status { get; set; } = ListiTicket.abierto; // Estado por defecto
    public Guid? CategoryId { get; set; }
}

internal sealed class AddTicketCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<AddTicketCommand, string>
{
    public async Task<string> Handle(AddTicketCommand request, CancellationToken ct)
    {
        Domain.Entities.Ticket ticket = new Domain.Entities.Ticket
        {
            TicketId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Status = request.Status.ToString(),
            CreatedAt = DateTime.SpecifyKind(
                DateTime.UtcNow,
                DateTimeKind.Unspecified
            )
        };

        if (request.CategoryId.HasValue)
        {
            ticket.CategoryId = request.CategoryId.Value;
        }

        await unitOfWork.TicketRepo.AddAsync(ticket, ct);

        return "Ticket creado exitosamente";
    }
}