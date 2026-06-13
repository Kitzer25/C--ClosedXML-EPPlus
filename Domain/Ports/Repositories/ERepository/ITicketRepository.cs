using Domain.DTO_s;
using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface ITicketRepository : IGRepositories<Ticket>
{
    Task<IEnumerable<Ticket>> GetAllTicketsAsync(CancellationToken ct);
    Task<bool> Exists(Guid ticketId, CancellationToken ct);
    Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId, CancellationToken ct);
    Task<IEnumerable<Ticket>> GetByStatusAsync(string status, CancellationToken ct);
    Task<Ticket?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct);
  
    Task<IEnumerable<UsuarioTopDto>> GetTopUsersAsync(int topCount, CancellationToken ct);
    Task<IEnumerable<CategoriaTopDto>> GetTopCategoriesAsync(int topCount, CancellationToken ct);
}


