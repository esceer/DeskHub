using DeskHub.Application.Interfaces.Persistence;
using DeskHub.Application.Interfaces.Services;
using DeskHub.Domain.Entities;

namespace DeskHub.Application.Services;

public class DeskService : IDeskService
{
    private readonly IDeskRepository _deskRepository;

    public DeskService(IDeskRepository deskRepository)
    {
        _deskRepository = deskRepository;
    }

    public Desk Create(Desk desk)
    {
        return _deskRepository.Create(desk);
    }

    public void DeleteById(Guid id)
    {
        _deskRepository.DeleteById(id);
    }

    public IEnumerable<Desk> GetAll()
    {
        return _deskRepository.GetAll();
    }

    public Desk? GetById(Guid id)
    {
        return _deskRepository.GetById(id);
    }

    public Desk Update(Desk desk)
    {
        return _deskRepository.Update(desk);
    }
}