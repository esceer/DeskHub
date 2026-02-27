using DeskHub.API.DTOs.Requests;
using DeskHub.API.Mappings;
using DeskHub.Application.Interfaces.Services;
using DeskHub.Domain.Entities;
using DeskHub.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace DeskHub.Backend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService service)
    {
        _reservationService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Guid? userId)
    {
        var reservations = userId.HasValue
            ? await _reservationService.GetByUserIdAsync(userId.Value)
            : await _reservationService.GetAllAsync();

        return Ok(reservations.Select(ReservationMappings.ToResponse));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var reservation = await _reservationService.GetByIdAsync(id);

        if (reservation is null)
            return NotFound();

        return Ok(reservation.ToResponse());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateReservationRequest dto)
    {
        var reservation = new Reservation
        {
            UserId = dto.UserId,
            DeskId = dto.DeskId,
            Time = new TimeRange(dto.Start, dto.End)
        };

        var createdReservation = await _reservationService.CreateAsync(reservation);

        return CreatedAtAction(nameof(Post), new { id = createdReservation.Id }, createdReservation.ToResponse());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateReservationRequest dto)
    {
        var reservation = new Reservation
        {
            Id = id,
            Time = new TimeRange(dto.Start, dto.End),
            Status = dto.Status
        };

        var updated = await _reservationService.UpdateAsync(reservation);
        if (updated is null)
            return NotFound();

        return Ok(updated.ToResponse());
    }

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var updated = await _reservationService.CancelAsync(id);
        if (updated is null)
            return NotFound();

        return Ok(updated.ToResponse());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _reservationService.DeleteByIdAsync(id);
        return NoContent();
    }
}
