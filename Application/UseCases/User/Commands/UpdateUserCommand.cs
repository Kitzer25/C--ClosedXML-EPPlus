using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.User.Commands;

public class UpdateUserCommand : IRequest<string>
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string? Email { get; set; }
}
 
internal sealed class UpdateUserCommandHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<UpdateUserCommand, string>
{
    public async Task<string> Handle(UpdateUserCommand request, CancellationToken ct)
    {
        var user = await unitOfWork.UserRepo.GetByIdAsync(request.UserId, ct)
                   ?? throw new KeyNotFoundException("El usuario no existe");
 
        // Solo validar duplicados si los valores cambiaron
        if (!string.Equals(user.Username, request.Username, StringComparison.OrdinalIgnoreCase)
            && await unitOfWork.UserRepo.UserExists(request.Username, ct))
        {
            throw new ApplicationException("Username ya existe");
        }
 
        if (request.Email is not null
            && !string.Equals(user.Email, request.Email, StringComparison.OrdinalIgnoreCase)
            && await unitOfWork.UserRepo.ExistEmail(request.Email, ct))
        {
            throw new ApplicationException("Email ya existe");
        }
 
        user.Username = request.Username;
        user.Email = request.Email;
 
        await unitOfWork.UserRepo.UpdateAsync(request.UserId,user, ct);
 
        return "Usuario actualizado exitosamente";
    }
}