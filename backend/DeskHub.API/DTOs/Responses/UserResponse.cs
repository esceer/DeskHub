using DeskHub.Domain.Enums;

namespace DeskHub.API.DTOs.Responses;

public record UserResponse
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Email { get; init; }
    public Role Role { get; init; }
}