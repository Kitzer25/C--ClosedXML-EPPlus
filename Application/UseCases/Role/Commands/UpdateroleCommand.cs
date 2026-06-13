using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Role.Commands;

public class UpdateRoleCommand : IRequest<string>
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = null!;
}
 
internal sealed class UpdateRoleCommandHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<UpdateRoleCommand, string>
{
    public async Task<string> Handle(UpdateRoleCommand request, CancellationToken ct)
    {
        var role = await unitOfWork.RoleRepo.GetByIdAsync(request.RoleId, ct)
                   ?? throw new KeyNotFoundException("El rol no existe");
 
        if (!string.Equals(role.RoleName, request.RoleName, StringComparison.OrdinalIgnoreCase)
            && await unitOfWork.RoleRepo.RoleNameExists(request.RoleName, ct))
        {
            throw new ApplicationException("El nombre del rol ya existe");
        }
 
        role.RoleName = request.RoleName;
 
        await unitOfWork.RoleRepo.UpdateAsync(request.RoleId, role, ct);
        await unitOfWork.SaveChangesAsync(ct);
 
        return "Rol actualizado exitosamente";
    }
}