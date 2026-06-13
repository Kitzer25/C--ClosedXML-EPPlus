using Domain.Ports.Repositories.AttachmentRepository;
using Domain.Entities;

namespace Infrastructure.Adapters.Repositories.AttachmentRepository;

public class AttachmentRepository :
    GRepositories<Attachment>,
    IAttachmentRepository
{
    public AttachmentRepository(AppDbContext context) : base(context)
    {
    }
}
