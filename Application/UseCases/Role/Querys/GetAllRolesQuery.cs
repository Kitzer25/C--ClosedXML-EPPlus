using Domain.DTO_s;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Role.Querys;

public class GetAllRolesQuery : IRequest<IEnumerable<RoleDto>>
{
}
 
internal sealed class GetAllRolesQueryHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
{
    public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken ct)
    {
        var roles = await unitOfWork.RoleRepo.GetAllAsync(ct);
        return roles.Select(r => new RoleDto(r.RoleId, r.RoleName));
    }
}