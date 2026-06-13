using Domain.DTO_s;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Ticket.Querys;

public class GetAllTicketsQuery : IRequest<IEnumerable<TicketDto>>
{
}
 
internal sealed class GetAllTicketsQueryHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<GetAllTicketsQuery, IEnumerable<TicketDto>>
{
    public async Task<IEnumerable<TicketDto>> Handle(GetAllTicketsQuery request, CancellationToken ct)
    {
        var tickets = await unitOfWork.TicketRepo.GetAllTicketsAsync(ct);
        return tickets.Select(MapToDto);
    }
 
    private static TicketDto MapToDto(Domain.Entities.Ticket t) => new(
        t.TicketId,
        t.Title,
        t.Description,
        t.Status,
        t.CreatedAt,
        t.ClosedAt,
        t.UserId,
        t.User.Username,
        t.CategoryId,
        t.Category?.Name);
}