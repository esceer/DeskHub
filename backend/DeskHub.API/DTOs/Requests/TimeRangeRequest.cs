using System.ComponentModel.DataAnnotations;

namespace DeskHub.API.DTOs.Requests;

public record TimeRangeRequest
{
    [Required]
    public DateTime? Start { get; init; }
    [Required]
    public DateTime? End { get; init; }
}
