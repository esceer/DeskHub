using DeskHub.Application.Interfaces.Persistence;
using DeskHub.Application.Interfaces.Services;
using DeskHub.Domain.Entities;

namespace DeskHub.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository repository)
    {
        _reservationRepository = repository;
    }

    public Task<IEnumerable<Reservation>> GetAllAsync() => _reservationRepository.GetAllAsync();

    public Task<Reservation?> GetByIdAsync(Guid id) => _reservationRepository.GetByIdAsync(id);

    public Task<Reservation> CreateAsync(Reservation reservation) => _reservationRepository.CreateAsync(reservation);

    public Task<Reservation?> UpdateAsync(Reservation reservation) => _reservationRepository.UpdateAsync(reservation);

    public Task DeleteByIdAsync(Guid id) => _reservationRepository.DeleteByIdAsync(id);
}