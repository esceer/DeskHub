using DeskHub.Domain.Enums;

namespace DeskHub.API.DTOs.Responses;

public record ReservationResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid DeskId { get; init; }
    public DateTimeOffset Start { get; init; }
    public DateTimeOffset End { get; init; }
    public ReservationStatus Status { get; init; }
}