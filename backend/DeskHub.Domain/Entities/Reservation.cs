using DeskHub.Domain.Enums;
using DeskHub.Domain.ValueObjects;

namespace DeskHub.Domain.Entities;

public class Reservation
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid UserId { get; init; }
    public required Guid DeskId { get; init; }
    public User User { get; private set; } = null!;
    public Desk Desk { get; private set; } = null!;
    public required TimeRange Time { get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Active;
}