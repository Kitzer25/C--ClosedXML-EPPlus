using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Role.Commands;

public class AddNewRoleCommand : IRequest<string>
{
    public string RoleName { get; set; } = null!;
}
 
internal sealed class AddNewRoleCommandHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<AddNewRoleCommand, string>
{
    public async Task<string> Handle(AddNewRoleCommand request, CancellationToken ct)
    {
        if (await unitOfWork.RoleRepo.RoleNameExists(request.RoleName, ct))
            throw new ApplicationException("El rol ya existe");
 
        var role = new Domain.Entities.Role
        {
            RoleId = Guid.NewGuid(),
            RoleName = request.RoleName
        };
 
        await unitOfWork.RoleRepo.AddAsync(role, ct);
        await unitOfWork.SaveChangesAsync(ct);
 
        return "Rol generado exitosamente";
    }
}