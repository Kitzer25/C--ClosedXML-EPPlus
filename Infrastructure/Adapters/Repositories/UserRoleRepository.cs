using Domain.Ports.Repositories.UserRoleRepository;
using Domain.Entities;

namespace Infrastructure.Adapters.Repositories.UserRoleRepository;

public class UserRoleRepository :
    GRepositories<UserRole>,
    IUserRoleRepository
{
    public UserRoleRepository(AppDbContext context) : base(context)
    {
    }
}
