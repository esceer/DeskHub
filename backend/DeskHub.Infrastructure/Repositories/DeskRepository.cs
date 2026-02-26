
using DeskHub.Application.Interfaces.Persistence;
using DeskHub.Domain.Entities;
using DeskHub.Infrastructure.Data;

namespace DeskHub.Infrastructure.Repositories;

public class DeskRepository : IDeskRepository
{
    private readonly ApplicationDbContext _db;

    public DeskRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public Desk Create(Desk desk)
    {
        var createdDesk = _db.Desks.Add(desk);
        _db.SaveChanges();
        return desk;
    }

    public void DeleteById(Guid id)
    {
        _db.Desks.Remove(new Desk { Id = id });
        _db.SaveChanges();
    }

    public IEnumerable<Desk> GetAll()
    {
        return [.. _db.Desks];
    }

    public Desk? GetById(Guid id)
    {
        return _db.Desks.Find(id);
    }

    public Desk Update(Desk desk)
    {
        var existingDesk = GetById(desk.Id);
        if (existingDesk == null)
        {
            throw new InvalidOperationException($"Desk with ID {desk.Id} does not exist.");
        }

        existingDesk.IsActive = desk.IsActive;
        _db.SaveChanges();

        return existingDesk;
    }
}