using Domain.Ports.Repositories.AuditLogRepository;
using Domain.Entities;

namespace Infrastructure.Adapters.Repositories.AuditLogRepository;

public class AuditLogRepository :
    GRepositories<AuditLog>,
    IAuditLogRepository
{
    public AuditLogRepository(AppDbContext context) : base(context)
    {
    }
}
