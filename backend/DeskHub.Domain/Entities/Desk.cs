namespace DeskHub.Domain.Entities;

public class Desk
{
    public required Guid Id { get; init; }
    public string Code { get; init; }
    public bool IsActive { get; set; }
}