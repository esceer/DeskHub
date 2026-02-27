
using DeskHub.Application.Interfaces.Persistence;
using DeskHub.Domain.Entities;
using DeskHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DeskHub.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _db;

    public ReservationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _db.Reservations
            .AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.Desk)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId)
    {
        return await _db.Reservations
            .AsNoTracking()
            .Where(r => r.UserId == userId)
            .Include(r => r.User)
            .Include(r => r.Desk)
            .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(Guid id)
    {
        return await _db.Reservations
            .AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.Desk)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Reservation> CreateAsync(Reservation reservation)
    {
        _db.Reservations.Add(reservation);
        await _db.SaveChangesAsync();
        return await _db.Reservations
            .Include(x => x.User)
            .Include(x => x.Desk)
            .FirstAsync(x => x.Id == reservation.Id);
    }

    // Tracked update due to owned type TimeRange
    public async Task<Reservation?> UpdateAsync(Reservation reservation)
    {
        var existing = await _db.Reservations
            .FirstOrDefaultAsync(r => r.Id == reservation.Id);

        if (existing is null)
            return null;

        existing.Time = reservation.Time;
        existing.Status = reservation.Status;

        await _db.SaveChangesAsync();

        return await _db.Reservations
            .AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.Desk)
            .FirstOrDefaultAsync(r => r.Id == reservation.Id);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        await _db.Reservations
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();
    }
}