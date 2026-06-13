using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Response.Commands;

public class DeleteResponseCommand : IRequest<string>
{
    public Guid ResponseId { get; set; }
}

internal sealed class DeleteResponseCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteResponseCommand, string>
{
    public async Task<string> Handle(DeleteResponseCommand request, CancellationToken ct)
    {
        var response = await unitOfWork.ResponseRepo.GetByIdAsync(request.ResponseId, ct);

        if (response == null)
        {
            throw new ApplicationException("La respuesta no existe o ya fue removida");
        }

        await unitOfWork.ResponseRepo.DeleteAsync(response, ct);

        return "Respuesta eliminada";
    }
}