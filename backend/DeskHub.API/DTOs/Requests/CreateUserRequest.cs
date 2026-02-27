using DeskHub.Domain.Enums;

namespace DeskHub.API.DTOs.Requests;

public record CreateUserRequest
{
    public required string Name { get; init; }
    public string? Email { get; init; }
    public Role Role { get; init; } = Role.User;
}
