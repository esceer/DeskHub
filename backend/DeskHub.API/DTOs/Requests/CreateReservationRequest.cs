namespace DeskHub.API.DTOs.Requests;

public record CreateReservationRequest
{
    public Guid UserId { get; init; }
    public Guid DeskId { get; init; }
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
}
