using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Ticket.Commands;

public class DeleteTicketCommand : IRequest<string>
{
    public Guid TicketId { get; set; }
}

internal sealed class DeleteTicketCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteTicketCommand, string>
{
    public async Task<string> Handle(DeleteTicketCommand request, CancellationToken ct)
    {
        var ticket = await unitOfWork.TicketRepo.GetByIdAsync(request.TicketId, ct);

        if (ticket == null)
        {
            throw new ApplicationException("El ticket no existe");
        }

        await unitOfWork.TicketRepo.DeleteAsync(ticket, ct);

        return "Ticket eliminado exitosamente";
    }
}