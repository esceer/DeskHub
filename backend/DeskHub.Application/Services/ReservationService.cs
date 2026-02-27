using DeskHub.Application.Interfaces.Persistence;
using DeskHub.Application.Interfaces.Services;
using DeskHub.Domain.Entities;
using DeskHub.Domain.Enums;

namespace DeskHub.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IDeskRepository _deskRepository;

    public ReservationService(IReservationRepository reservationRepository, IDeskRepository deskRepository)
    {
        _reservationRepository = reservationRepository;
        _deskRepository = deskRepository;
    }

    public Task<IEnumerable<Reservation>> GetAllAsync() => _reservationRepository.GetAllAsync();

    public Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId) => _reservationRepository.GetByUserIdAsync(userId);

    public Task<Reservation?> GetByIdAsync(Guid id) => _reservationRepository.GetByIdAsync(id);

    public async Task<Reservation> CreateAsync(Reservation reservation)
    {
        var desk = await _deskRepository.GetByIdAsync(reservation.DeskId) ?? throw new InvalidOperationException("Desk does not exist.");
        if (!desk.IsActive)
            throw new InvalidOperationException("Cannot create a reservation for an inactive desk.");

        return await _reservationRepository.CreateAsync(reservation);
    }

    public Task<Reservation?> UpdateAsync(Reservation reservation) => _reservationRepository.UpdateAsync(reservation);

    public Task DeleteByIdAsync(Guid id) => _reservationRepository.DeleteByIdAsync(id);

    public async Task<Reservation?> CancelAsync(Guid id)
    {
        var reservation = await GetByIdAsync(id);
        if (reservation is null)
            return null;

        reservation.Status = ReservationStatus.Cancelled;

        return await UpdateAsync(reservation);
    }
}