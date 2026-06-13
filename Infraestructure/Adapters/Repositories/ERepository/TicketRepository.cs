using Domain.DTO_s;
using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class TicketRepository :
    GRepositories<Ticket>,
    ITicketRepository
{
    public TicketRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync(CancellationToken ct)
    {
        return await _dbSet
            .Include(t => t.User)
            .Include(t => t.Category)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists(Guid ticketId, CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .AnyAsync(i => i.TicketId == ticketId, ct);

    }

    public async Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId, CancellationToken ct)
        => await _dbSet
            .AsNoTracking()
            .Include(t => t.User)
            .Include(t => t.Category)
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(ct);

    public async Task<IEnumerable<Ticket>> GetByStatusAsync(string status, CancellationToken ct)
        => await _dbSet
            .AsNoTracking()
            .Include(t => t.User)
            .Include(t => t.Category)
            .Where(t => t.Status == status)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(ct);

    public async Task<Ticket?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct)
        => await _dbSet
            .AsNoTracking()
            .Include(t => t.User)
            .Include(t => t.Category)
            .Include(t => t.Responses)
            .ThenInclude(r => r.Responder)
            .FirstOrDefaultAsync(t => t.TicketId == id, ct);

    public async Task<IEnumerable<UsuarioTopDto>> GetTopUsersAsync(int topCount, CancellationToken ct)
    {
        return await _context.Tickets
            .AsNoTracking()
            .GroupBy(t => t.UserId)
            .Select(g => new 
            {
                Id = g.Key,
                Total = g.Count() // EF Core traduce esto a COUNT(*) sin problemas
            })
            .OrderByDescending(x => x.Total)
            .Take(topCount)
            .Select(x => new UsuarioTopDto(x.Id, x.Total)) // Mapeo final a tu Record
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<CategoriaTopDto>> GetTopCategoriesAsync(int topCount, CancellationToken ct)
    {
        return await _context.Tickets
            .AsNoTracking()
            .GroupBy(t => t.CategoryId)
            .Select(g => new 
            {
                Id = g.Key,
                Total = g.Count()
            })
            .OrderByDescending(x => x.Total)
            .Take(topCount)
            .Select(x => new CategoriaTopDto(x.Id, x.Total)) // Mapeo final a tu Record
            .ToListAsync(ct);
    }
}
