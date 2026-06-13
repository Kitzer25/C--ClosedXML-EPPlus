namespace Domain.Ports.Repositories;

public interface IGRepositories<T>
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct);
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(T entity, CancellationToken ct);
    Task UpdateAsync(Guid id, T entity, CancellationToken ct);
    Task<bool> DeleteAsync(T entity, CancellationToken ct);
}