using DeskHub.Domain.Entities;
using DeskHub.Domain.ValueObjects;

namespace DeskHub.Application.Interfaces.Services;

public interface IDeskService : ICrudService<Desk>
{
    Task<IEnumerable<Desk>> GetAvailableAsync(TimeRange range);
}