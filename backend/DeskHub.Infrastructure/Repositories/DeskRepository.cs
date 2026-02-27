
using DeskHub.Application.Interfaces.Persistence;
using DeskHub.Domain.Entities;
using DeskHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DeskHub.Infrastructure.Repositories;

public class DeskRepository : IDeskRepository
{
    private readonly ApplicationDbContext _db;

    public DeskRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Desk>> GetAllAsync()
    {
        return await _db.Desks
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Desk?> GetByIdAsync(Guid id)
    {
        return await _db.Desks
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Desk> CreateAsync(Desk desk)
    {
        _db.Desks.Add(desk);
        await _db.SaveChangesAsync();
        return desk;
    }

    public async Task<Desk?> UpdateAsync(Desk desk)
    {
        var affected = await _db.Desks
            .Where(d => d.Id == desk.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(d => d.Code, desk.Code)
                .SetProperty(d => d.IsActive, desk.IsActive));

        if (affected == 0)
            return null;

        return await GetByIdAsync(desk.Id);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        await _db.Desks
            .Where(d => d.Id == id)
            .ExecuteDeleteAsync();
    }
}