using Domain.Ports.Repositories.UserRepository;
using Domain.Entities;

namespace Infrastructure.Adapters.Repositories.UserRepository;

public class UserRepository :
    GRepositories<User>,
    IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}
