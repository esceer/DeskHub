using DeskHub.API.DTOs.Responses;
using DeskHub.Domain.Entities;

namespace DeskHub.API.Mappings;

public static class ReservationMappings
{
    public static ReservationResponse ToResponse(this Reservation reservation)
    {
        return new ReservationResponse
        {
            Id = reservation.Id,
            UserId = reservation.UserId,
            UserName = reservation.User.Name,
            DeskId = reservation.DeskId,
            DeskCode = reservation.Desk.Code,
            Start = reservation.Time.Start,
            End = reservation.Time.End,
            Status = reservation.Status
        };
    }
}
