namespace DeskHub.API.DTOs.Responses;

public record DeskResponse
{
    public Guid Id { get; init; }
    public required string Code { get; init; }
    public required bool IsActive { get; init; }
}