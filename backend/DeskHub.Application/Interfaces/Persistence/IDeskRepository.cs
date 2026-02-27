using DeskHub.Domain.Entities;

namespace DeskHub.Application.Interfaces.Persistence;

public interface IDeskRepository
{
    Task<IEnumerable<Desk>> GetAllAsync();
    Task<Desk?> GetByIdAsync(Guid id);
    Task<Desk> CreateAsync(Desk desk);
    Task<Desk?> UpdateAsync(Desk desk);
    Task DeleteByIdAsync(Guid id);
}