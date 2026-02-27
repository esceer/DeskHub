using DeskHub.API.DTOs.Requests;
using DeskHub.API.DTOs.Responses;
using DeskHub.Application.Interfaces.Services;
using DeskHub.Backend.Api.Filters;
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
    public async Task<IActionResult> Get()
    {
        var reservations = await _reservationService.GetAllAsync();

        var response = reservations.Select(r => new ReservationResponse
        {
            Id = r.Id,
            UserId = r.UserId,
            DeskId = r.DeskId,
            Start = r.Time.Start,
            End = r.Time.End,
            Status = r.Status
        });
        return Ok(reservations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var reservation = await _reservationService.GetByIdAsync(id);

        if (reservation is null)
            return NotFound();

        var response = new ReservationResponse
        {
            Id = reservation.Id,
            UserId = reservation.UserId,
            DeskId = reservation.DeskId,
            Start = reservation.Time.Start,
            End = reservation.Time.End,
            Status = reservation.Status
        };
        return Ok(reservation);
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

        var reponse = new ReservationResponse
        {
            Id = reservation.Id,
            UserId = reservation.UserId,
            DeskId = reservation.DeskId,
            Start = reservation.Time.Start,
            End = reservation.Time.End,
            Status = reservation.Status
        };
        return CreatedAtAction(nameof(Post), new { id = createdReservation.Id }, createdReservation);
    }

    [HttpPut("{id}")]
    [IdExistsFilter]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateReservationRequest dto)
    {
        var reservation = new Reservation
        {
            Id = id,
            Time = new TimeRange(dto.Start, dto.End),
            Status = dto.Status
        };

        if (reservation.Id != id)
            return BadRequest("ID in URL and body must match.");

        var updated = await _reservationService.UpdateAsync(reservation);
        if (updated is null)
            return NotFound();

        var response = new ReservationResponse
        {
            Id = updated.Id,
            UserId = updated.UserId,
            DeskId = updated.DeskId,
            Start = updated.Time.Start,
            End = updated.Time.End,
            Status = updated.Status
        };
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _reservationService.DeleteByIdAsync(id);
        return NoContent();
    }
}
