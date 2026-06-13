using Domain.DTO_s;
using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface ICategoryRepository : IGRepositories<Category>
{
    Task<IEnumerable<Category>> GetAllWithTicketsAsync(CancellationToken ct);
}
