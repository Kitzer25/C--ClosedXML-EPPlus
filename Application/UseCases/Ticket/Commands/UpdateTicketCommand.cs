using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Ticket.Commands;

public class UpdateTicketCommand : IRequest<string>
{
    public Guid TicketId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = null!;
    public Guid? CategoryId { get; set; }
    public DateTime? ClosedAt { get; set; }
}

internal sealed class UpdateTicketCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateTicketCommand, string>
{
    public async Task<string> Handle(UpdateTicketCommand request, CancellationToken ct)
    {
        var ticket = await unitOfWork.TicketRepo.GetByIdAsync(request.TicketId, ct);

        if (ticket == null)
        {
            throw new ApplicationException("El ticket no existe");
        }

        ticket.Title = request.Title;
        ticket.Description = request.Description;
        ticket.Status = request.Status;
        ticket.CategoryId = request.CategoryId;
        
        if (request.Status.Equals("Closed", StringComparison.OrdinalIgnoreCase))
        {
            ticket.ClosedAt = request.ClosedAt ?? DateTime.UtcNow;
        }

        await unitOfWork.TicketRepo.UpdateAsync(request.TicketId, ticket, ct);

        return "Ticket actualizado exitosamente";
    }
}