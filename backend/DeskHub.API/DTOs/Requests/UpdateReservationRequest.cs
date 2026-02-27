using DeskHub.Domain.Enums;

namespace DeskHub.API.DTOs.Requests
{
    public record UpdateReservationRequest
    {
        public DateTime Start { get; init; }
        public DateTime End { get; init; }
        public ReservationStatus Status { get; init; }
    }
}
