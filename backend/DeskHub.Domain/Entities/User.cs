using DeskHub.Domain.Enums;

namespace DeskHub.Domain.Entities;

public class User
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
}