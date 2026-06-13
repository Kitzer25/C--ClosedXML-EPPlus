using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class RoleRepository :
    GRepositories<Role>,
    IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> RoleNameExists(string roleName, CancellationToken ct)
        => await _dbSet
            .AsNoTracking()
            .AnyAsync(r => r.RoleName == roleName, ct);
 
    public async Task<bool> HasUsersAssigned(Guid roleId, CancellationToken ct)
        => await _dbSet
            .AsNoTracking()
            .AnyAsync(ur => ur.RoleId == roleId, ct);
}
