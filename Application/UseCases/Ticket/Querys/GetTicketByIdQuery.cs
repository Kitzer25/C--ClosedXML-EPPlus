using Domain.DTO_s;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Ticket.Querys;

public class GetTicketByIdQuery : IRequest<TicketDetailDto>
{
    public Guid TicketId { get; set; }
}
 
internal sealed class GetTicketByIdQueryHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<GetTicketByIdQuery, TicketDetailDto>
{
    public async Task<TicketDetailDto> Handle(GetTicketByIdQuery request, CancellationToken ct)
    {
        var ticket = await unitOfWork.TicketRepo.GetByIdWithDetailsAsync(request.TicketId, ct)
                     ?? throw new KeyNotFoundException("El ticket no existe");
 
        return new TicketDetailDto(
            ticket.TicketId,
            ticket.Title,
            ticket.Description,
            ticket.Status,
            ticket.CreatedAt,
            ticket.ClosedAt,
            ticket.UserId,
            ticket.User.Username,
            ticket.CategoryId,
            ticket.Category?.Name,
            ticket.Responses
                .OrderBy(r => r.CreatedAt)
                .Select(r => new TicketResponseDto(
                    r.ResponseId,
                    r.ResponderId,
                    r.Responder.Username,
                    r.Message,
                    r.CreatedAt))
                .ToList());
    }
}