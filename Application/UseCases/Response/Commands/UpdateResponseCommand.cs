using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Response.Commands;

public class UpdateResponseCommand : IRequest<string>
{
    public Guid ResponseId { get; set; }
    public string Message { get; set; } = null!;
}

internal sealed class UpdateResponseCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateResponseCommand, string>
{
    public async Task<string> Handle(UpdateResponseCommand request, CancellationToken ct)
    {
        var response = await unitOfWork.ResponseRepo.GetByIdAsync(request.ResponseId, ct);

        if (response == null)
        {
            throw new ApplicationException("La respuesta no existe");
        }

        response.Message = request.Message;

        await unitOfWork.ResponseRepo.UpdateAsync(request.ResponseId, response, ct);

        return "Respuesta modificada exitosamente";
    }
}