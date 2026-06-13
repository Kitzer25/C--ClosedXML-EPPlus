using Domain.Ports.Repositories.InternalNoteRepository;
using Domain.Entities;

namespace Infrastructure.Adapters.Repositories.InternalNoteRepository;

public class InternalNoteRepository :
    GRepositories<InternalNote>,
    IInternalNoteRepository
{
    public InternalNoteRepository(AppDbContext context) : base(context)
    {
    }
}
