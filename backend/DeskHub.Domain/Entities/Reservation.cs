using DeskHub.Domain.Enums;
using DeskHub.Domain.ValueObjects;

namespace DeskHub.Domain.Entities;

public class Reservation
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required Guid DeskId { get; init; }

    public required TimeRange Time { get; init; }
    public required ReservationStatus Status { get; set; }
}