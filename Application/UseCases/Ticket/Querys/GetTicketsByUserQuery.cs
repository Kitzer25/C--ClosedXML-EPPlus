using Domain.DTO_s;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Ticket.Querys;

public class GetTicketsByUserQuery : IRequest<IEnumerable<TicketDto>>
{
    public Guid UserId { get; set; }
}
 
internal sealed class GetTicketsByUserQueryHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<GetTicketsByUserQuery, IEnumerable<TicketDto>>
{
    public async Task<IEnumerable<TicketDto>> Handle(GetTicketsByUserQuery request, CancellationToken ct)
    {
        var tickets = await unitOfWork.TicketRepo.GetByUserIdAsync(request.UserId, ct);
 
        return tickets.Select(t => new TicketDto(
            t.TicketId,
            t.Title,
            t.Description,
            t.Status,
            t.CreatedAt,
            t.ClosedAt,
            t.UserId,
            t.User.Username,
            t.CategoryId,
            t.Category?.Name));
    }
}