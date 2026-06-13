using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.User.Commands;

public class DeleteUserCommand : IRequest<string>
{
    public Guid UserId { get; set; }
}
 
internal sealed class DeleteUserCommandHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<DeleteUserCommand, string>
{
    public async Task<string> Handle(DeleteUserCommand request, CancellationToken ct)
    {
        var user = await unitOfWork.UserRepo.GetByIdAsync(request.UserId, ct)
                   ?? throw new KeyNotFoundException("El usuario no existe");
 
        if (user.Tickets.Any())
            throw new ApplicationException("No se puede eliminar: el usuario tiene tickets asociados");
 
        await unitOfWork.UserRepo.DeleteAsync(user, ct);
 
        return "Usuario eliminado exitosamente";
    }
}