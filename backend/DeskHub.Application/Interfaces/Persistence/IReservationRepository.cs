using DeskHub.Domain.Entities;

namespace DeskHub.Application.Interfaces.Persistence;

public interface IReservationRepository : ICrudRepository<Reservation>
{
}