using Domain.Ports.Repositories;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Adapters.Repositories.ERepository;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IDictionary<Type, object> _repositories;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
        
        //Repositories
        AttachmentRepo = new AttachmentRepository(_context);
        AuditLogRepo = new AuditLogRepository(_context);
        CategoryRepo = new CategoryRepository(_context);
        InternalNoteRepo = new InternalNoteRepository(_context);
        ResponseRepo = new ResponseRepository(_context);
        RoleRepo = new RoleRepository(_context);
        TicketRepo = new TicketRepository(_context);
        UserRepo = new UserRepository(_context);
        UserRoleRepo = new UserRoleRepository(_context);
    }
    
    //Repositorios 
    public IAttachmentRepository AttachmentRepo { get; }
    public IAuditLogRepository AuditLogRepo { get; }
    public ICategoryRepository CategoryRepo { get; }
    public IInternalNoteRepository InternalNoteRepo { get; }
    public IResponseRepository ResponseRepo { get; }
    public IRoleRepository RoleRepo { get; }
    public ITicketRepository TicketRepo { get; }
    public IUserRepository UserRepo { get; }
    public IUserRoleRepository UserRoleRepo { get; }

    //Funciones
    public IGRepositories<T> Repositories<T>() where T : class
    {
        var type = typeof(T);

        if (_repositories.TryGetValue(type, out var repositories))
        {
            return (IGRepositories<T>)repositories;
        }
        
        var repositoryInstance = new GRepositories<T>(_context);
        
        _repositories.Add(type, repositoryInstance);
        
        return repositoryInstance;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}