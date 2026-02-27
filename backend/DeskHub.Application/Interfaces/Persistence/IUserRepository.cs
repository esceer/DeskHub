using DeskHub.Domain.Entities;

namespace DeskHub.Application.Interfaces.Persistence;

public interface IUserRepository : ICrudRepository<User>
{
}