using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IInternalNoteRepository : IGRepositories<InternalNote>
{
    Task<IEnumerable<InternalNote>> GetByTicketIdAsync(Guid ticketId, CancellationToken ct);
    Task<bool> Exists(Guid ticketId, CancellationToken ct);
}
