using DeskHub.API.DTOs.Responses;
using DeskHub.Domain.Entities;

namespace DeskHub.API.Mappings;

public static class UserMappings
{
    public static UserResponse ToResponse(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }
}
