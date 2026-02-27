namespace DeskHub.Application.Interfaces.Services;

public interface ICrudService<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T> CreateAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task DeleteByIdAsync(Guid id);
}