using Domain.Ports.Repositories.ERepository;

namespace Domain.Ports.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IAttachmentRepository AttachmentRepo { get; }
    public IAuditLogRepository AuditLogRepo { get; }
    public ICategoryRepository CategoryRepo { get; }
    public IInternalNoteRepository InternalNoteRepo { get; }
    public IResponseRepository ResponseRepo { get; }
    public IRoleRepository RoleRepo { get; }
    public ITicketRepository TicketRepo { get; }
    public IUserRepository UserRepo { get; }
    public IUserRoleRepository UserRoleRepo { get; }
    
    public IGRepositories<T> Repositories<T>() where T : class;
    
    Task<int> SaveChangesAsync(CancellationToken ct);
}