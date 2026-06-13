using Domain.Ports.Repositories.RoleRepository;
using Domain.Entities;

namespace Infrastructure.Adapters.Repositories.RoleRepository;

public class RoleRepository :
    GRepositories<Role>,
    IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    {
    }
}
