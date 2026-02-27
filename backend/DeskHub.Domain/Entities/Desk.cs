namespace DeskHub.Domain.Entities;

public class Desk
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Code { get; set; }
    public bool IsActive { get; set; } = true;
}