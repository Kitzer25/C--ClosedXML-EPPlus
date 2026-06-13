using Domain.DTO_s;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Role.Querys;

public class GetRoleByIdQuery : IRequest<RoleDto>
{
    public Guid RoleId { get; set; }
}
 
internal sealed class GetRoleByIdQueryHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken ct)
    {
        var role = await unitOfWork.RoleRepo.GetByIdAsync(request.RoleId, ct)
                   ?? throw new KeyNotFoundException("El rol no existe");
 
        return new RoleDto(role.RoleId, role.RoleName);
    }
}