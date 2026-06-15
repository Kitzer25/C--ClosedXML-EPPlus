using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IResponseRepository : IGRepositories<Response>
{
    Task<IEnumerable<Domain.Entities.Response>> GetByTicketIdAsync(Guid ticketId, CancellationToken ct);
}
