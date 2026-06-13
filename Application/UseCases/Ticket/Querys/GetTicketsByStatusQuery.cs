using Domain.DTO_s;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Ticket.Querys;

public class GetTicketsByStatusQuery : IRequest<IEnumerable<TicketDto>>
{
    public string Status { get; set; } = null!;
}
 
internal sealed class GetTicketsByStatusQueryHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<GetTicketsByStatusQuery, IEnumerable<TicketDto>>
{
    public async Task<IEnumerable<TicketDto>> Handle(GetTicketsByStatusQuery request, CancellationToken ct)
    {
        var tickets = await unitOfWork.TicketRepo.GetByStatusAsync(request.Status, ct);
 
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