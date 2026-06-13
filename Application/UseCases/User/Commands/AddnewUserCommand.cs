using Domain.Ports.Repositories;
using Domain.Ports.Services.Security;
using MediatR;

namespace Application.UseCases.User.Commands;

public class AddNewUserCommand : IRequest<string>
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
 
internal sealed class AddNewUserCommandHandler(
    IUnitOfWork unitOfWork,
    IPasswordHash passwordHash) :
    IRequestHandler<AddNewUserCommand, string>
{
    public async Task<string> Handle(AddNewUserCommand request, CancellationToken ct)
    {
        bool userExist = await unitOfWork.UserRepo.UserExists(request.Username, ct);
        bool emailExist = await unitOfWork.UserRepo.ExistEmail(request.Email, ct);
 
        if (userExist || emailExist)
        {
            throw new ApplicationException(
                userExist && emailExist
                    ? "Username y Email ya existen"
                    : userExist
                        ? "Username ya existe"
                        : "Email ya existe");
        }
 
        var user = new Domain.Entities.User
        {
            UserId = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash.Hash(request.Password),
            CreatedAt = DateTime.UtcNow
        };
 
        await unitOfWork.UserRepo.AddAsync(user, ct);
        await unitOfWork.SaveChangesAsync(ct);
 
        return "Usuario generado exitosamente";
    }
}