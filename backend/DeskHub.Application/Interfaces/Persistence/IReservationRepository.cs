using DeskHub.Domain.Entities;

namespace DeskHub.Application.Interfaces.Persistence;

public interface IReservationRepository : ICrudRepository<Reservation>
{
    Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId);
}