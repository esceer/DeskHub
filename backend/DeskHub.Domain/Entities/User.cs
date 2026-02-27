using DeskHub.Domain.Enums;

namespace DeskHub.Domain.Entities;

public class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Email { get; set; }
    public Role Role { get; set; } = Role.User;
}