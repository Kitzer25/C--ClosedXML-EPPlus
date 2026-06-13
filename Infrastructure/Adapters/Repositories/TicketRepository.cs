using Domain.Ports.Repositories.TicketRepository;
using Domain.Entities;

namespace Infrastructure.Adapters.Repositories.TicketRepository;

public class TicketRepository :
    GRepositories<Ticket>,
    ITicketRepository
{
    public TicketRepository(AppDbContext context) : base(context)
    {
    }
}
