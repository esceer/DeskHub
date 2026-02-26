using DeskHub.Domain.Entities;

namespace DeskHub.Application.Interfaces.Persistence;

public interface IDeskRepository
{
    IEnumerable<Desk> GetAll();
    Desk? GetById(Guid id);
    Desk Create(Desk desk);
    Desk Update(Desk desk);
    void DeleteById(Guid id);
}