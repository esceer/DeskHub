using DeskHub.Application.Interfaces.Persistence;
using DeskHub.Application.Interfaces.Services;
using DeskHub.Domain.Entities;
using DeskHub.Domain.ValueObjects;

namespace DeskHub.Application.Services;

public class DeskService : IDeskService
{
    private readonly IDeskRepository _deskRepository;

    public DeskService(IDeskRepository repository) => _deskRepository = repository;

    public Task<IEnumerable<Desk>> GetAllAsync() => _deskRepository.GetAllAsync();

    public Task<Desk?> GetByIdAsync(Guid id) => _deskRepository.GetByIdAsync(id);

    public Task<IEnumerable<Desk>> GetAvailableAsync(TimeRange range) => _deskRepository.GetAvailableAsync(range);

    public Task<Desk> CreateAsync(Desk desk) => _deskRepository.CreateAsync(desk);

    public Task<Desk?> UpdateAsync(Desk desk) => _deskRepository.UpdateAsync(desk);

    public Task DeleteByIdAsync(Guid id) => _deskRepository.DeleteByIdAsync(id);
}