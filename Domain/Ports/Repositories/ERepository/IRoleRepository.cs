using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IRoleRepository : IGRepositories<Role>
{
    Task<bool> RoleNameExists(string roleName, CancellationToken ct);
    Task<bool> HasUsersAssigned(Guid roleId, CancellationToken ct);
}
