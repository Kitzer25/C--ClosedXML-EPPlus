using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class InternalNoteRepository :
    GRepositories<InternalNote>,
    IInternalNoteRepository
{
    public InternalNoteRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<InternalNote>> GetByTicketIdAsync(Guid ticketId, CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .Include(n => n.Author)
            .Where(n => n.TicketId == ticketId)
            .OrderBy(n => n.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists(Guid ticketId, CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .AnyAsync(i => i.TicketId == ticketId, ct); 
    }
    
}
