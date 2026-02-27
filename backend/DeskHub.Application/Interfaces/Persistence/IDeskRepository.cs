using DeskHub.Domain.Entities;
using DeskHub.Domain.ValueObjects;

namespace DeskHub.Application.Interfaces.Persistence;

public interface IDeskRepository : ICrudRepository<Desk>
{
    Task<IEnumerable<Desk>> GetAvailableAsync(TimeRange range);
}