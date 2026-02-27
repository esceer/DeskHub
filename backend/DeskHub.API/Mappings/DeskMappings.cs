using DeskHub.API.DTOs.Responses;
using DeskHub.Domain.Entities;

namespace DeskHub.API.Mappings;

public static class DeskMappings
{
    public static DeskResponse ToResponse(this Desk desk)
    {
        return new DeskResponse
        {
            Id = desk.Id,
            Code = desk.Code,
            IsActive = desk.IsActive
        };
    }
}
