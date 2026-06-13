using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class UserRepository :
    GRepositories<User>,
    IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<User>> GetAllWithRolesAsync(CancellationToken ct)
        => await _dbSet
            .AsNoTracking()
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .OrderBy(u => u.Username)
            .ToListAsync(ct);

    public async Task<bool> UserExists(string username, CancellationToken ct)
        => await _dbSet
            .AsNoTracking()
            .AnyAsync(u => u.Username == username, ct);
 
    public async Task<bool> ExistEmail(string email, CancellationToken ct)
        => await _dbSet
            .AsNoTracking()
            .AnyAsync(u => u.Email == email, ct);
}
