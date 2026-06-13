using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IUserRepository : IGRepositories<User>
{
    Task<IEnumerable<User>> GetAllWithRolesAsync(CancellationToken ct);
    Task<bool> UserExists(string username, CancellationToken ct);
    Task<bool> ExistEmail(string email, CancellationToken ct);
}
