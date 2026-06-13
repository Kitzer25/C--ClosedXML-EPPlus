using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Response.Commands;

public class AddResponseCommand : IRequest<string>
{
    public Guid TicketId { get; set; }
    public Guid ResponderId { get; set; }
    public string Message { get; set; } = null!;
}

internal sealed class AddResponseCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<AddResponseCommand, string>
{
    public async Task<string> Handle(AddResponseCommand request, CancellationToken ct)
    {
        Domain.Entities.Response response = new Domain.Entities.Response
        {
            ResponseId = Guid.NewGuid(),
            TicketId = request.TicketId,
            ResponderId = request.ResponderId,
            Message = request.Message,
            CreatedAt = DateTime.UtcNow
        };

        await unitOfWork.ResponseRepo.AddAsync(response, ct);

        return "Respuesta agregada al ticket exitosamente";
    }
}