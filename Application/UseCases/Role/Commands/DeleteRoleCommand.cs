using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Role.Commands;

public class DeleteRoleCommand : IRequest<string>
{
    public Guid RoleId { get; set; }
}
 
internal sealed class DeleteRoleCommandHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<DeleteRoleCommand, string>
{
    public async Task<string> Handle(DeleteRoleCommand request, CancellationToken ct)
    {
        var role = await unitOfWork.RoleRepo.GetByIdAsync(request.RoleId, ct)
                   ?? throw new KeyNotFoundException("El rol no existe");
 
        if (await unitOfWork.RoleRepo.HasUsersAssigned(request.RoleId, ct))
            throw new ApplicationException("No se puede eliminar: el rol tiene usuarios asignados");
 
        await unitOfWork.RoleRepo.DeleteAsync(role, ct);
        await unitOfWork.SaveChangesAsync(ct);
 
        return "Rol eliminado exitosamente";
    }
}