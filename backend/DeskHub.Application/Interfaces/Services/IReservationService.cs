using DeskHub.Domain.Entities;

namespace DeskHub.Application.Interfaces.Services;

public interface IReservationService : ICrudService<Reservation>
{
    Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId);
    Task<Reservation?> CancelAsync(Guid id);
}