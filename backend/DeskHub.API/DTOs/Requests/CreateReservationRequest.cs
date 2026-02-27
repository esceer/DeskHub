namespace DeskHub.API.DTOs.Requests;

public record CreateReservationRequest
{
    public Guid UserId { get; init; }
    public Guid DeskId { get; init; }
    public DateTimeOffset Start { get; init; }
    public DateTimeOffset End { get; init; }
}
