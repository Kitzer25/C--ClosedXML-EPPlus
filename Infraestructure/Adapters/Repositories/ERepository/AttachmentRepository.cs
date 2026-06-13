using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class AttachmentRepository :
    GRepositories<Attachment>,
    IAttachmentRepository
{
    public AttachmentRepository(AppDbContext context) : base(context)
    {
    }
}
