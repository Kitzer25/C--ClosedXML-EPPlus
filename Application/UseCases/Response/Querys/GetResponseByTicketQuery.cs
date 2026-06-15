using Domain.DTO_s;
using Domain.Ports.Repositories;

namespace Application.UseCases.Response.Querys;

using MediatR;

public class GetResponsesByTicketQuery : IRequest<IEnumerable<ResponseDto>>
{
    public Guid TicketId { get; set; }
}

internal sealed class GetResponsesByTicketQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetResponsesByTicketQuery, IEnumerable<ResponseDto>>
{
    public async Task<IEnumerable<ResponseDto>> Handle(GetResponsesByTicketQuery request, CancellationToken ct)
    {
        var responses = await unitOfWork.ResponseRepo.GetByTicketIdAsync(request.TicketId, ct);

        var responseDtos = responses.Select(r => new ResponseDto(
            r.ResponseId,
            r.TicketId,
            r.ResponderId,
            r.Message,
            r.CreatedAt
        ));

        return responseDtos;
    }
}