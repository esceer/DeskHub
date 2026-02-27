namespace DeskHub.API.DTOs.Requests;

public record CreateDeskRequest
{
    public required string Code { get; init; }
    public bool IsActive { get; init; } = true;
}
