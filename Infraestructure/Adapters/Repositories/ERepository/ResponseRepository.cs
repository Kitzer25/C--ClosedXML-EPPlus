using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class ResponseRepository :
    GRepositories<Response>,
    IResponseRepository
{
    public ResponseRepository(AppDbContext context) : base(context)
    {
    }


    public async Task<IEnumerable<Domain.Entities.Response>> GetByTicketIdAsync(Guid ticketId, CancellationToken ct)
    {
        return await _dbSet
            .Where(r => r.TicketId == ticketId)
            .AsNoTracking() // Excelente para consultas de solo lectura (mejora el rendimiento)
            .ToListAsync(ct);
    }
}
