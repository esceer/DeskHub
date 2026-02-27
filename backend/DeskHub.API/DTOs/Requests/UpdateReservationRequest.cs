using DeskHub.Domain.Enums;

namespace DeskHub.API.DTOs.Requests
{
    public record UpdateReservationRequest
    {
        public DateTimeOffset Start { get; init; }
        public DateTimeOffset End { get; init; }
        public ReservationStatus Status { get; init; }
    }
}
