using DeskHub.Domain.Enums;

namespace DeskHub.API.DTOs.Responses;

public record ReservationResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid DeskId { get; init; }
    public required string UserName { get; init; }
    public required string DeskCode { get; init; }
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    public ReservationStatus Status { get; init; }
}