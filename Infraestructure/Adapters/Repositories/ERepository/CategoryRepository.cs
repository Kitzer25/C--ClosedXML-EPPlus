using Domain.DTO_s;
using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class CategoryRepository :
    GRepositories<Category>,
    ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Category>> GetAllWithTicketsAsync(CancellationToken ct)
        => await _dbSet
            .AsNoTracking()
            .Include(c => c.Tickets)
            .OrderBy(c => c.Name)
            .ToListAsync(ct);

    
}
