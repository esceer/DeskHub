
using DeskHub.Application.Interfaces.Persistence;
using DeskHub.Domain.Entities;
using DeskHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DeskHub.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _db.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> CreateAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        var affected = await _db.Users
            .Where(u => u.Id == user.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(u => u.Name, user.Name)
                .SetProperty(u => u.Email, user.Email)
                .SetProperty(u => u.Role, user.Role));

        if (affected == 0)
            return null;

        return await GetByIdAsync(user.Id);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        await _db.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
    }
}