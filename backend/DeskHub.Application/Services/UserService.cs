using DeskHub.Application.Interfaces.Persistence;
using DeskHub.Application.Interfaces.Services;
using DeskHub.Domain.Entities;

namespace DeskHub.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository repository) => _userRepository = repository;

    public Task<IEnumerable<User>> GetAllAsync() => _userRepository.GetAllAsync();

    public Task<User?> GetByIdAsync(Guid id) => _userRepository.GetByIdAsync(id);

    public Task<User> CreateAsync(User user) => _userRepository.CreateAsync(user);

    public Task<User?> UpdateAsync(User user) => _userRepository.UpdateAsync(user);

    public Task DeleteByIdAsync(Guid id) => _userRepository.DeleteByIdAsync(id);
}