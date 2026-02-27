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

    public Task<IEnumerable<Desk>> GetAllAsync() => _deskRepository.GetAllAsync();

    public Task<Desk?> GetByIdAsync(Guid id) => _deskRepository.GetByIdAsync(id);

    public Task<Desk> CreateAsync(Desk desk) => _deskRepository.CreateAsync(desk);

    public Task<Desk?> UpdateAsync(Desk desk) => _deskRepository.UpdateAsync(desk);

    public Task DeleteByIdAsync(Guid id) => _deskRepository.DeleteByIdAsync(id);
}