using DeskHub.API.DTOs.Requests;
using DeskHub.API.Mappings;
using DeskHub.Application.Interfaces.Services;
using DeskHub.Domain.Entities;
using DeskHub.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace DeskHub.Backend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DesksController : ControllerBase
{
    private readonly IDeskService _deskService;


    public DesksController(IDeskService service)
    {
        _deskService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var desks = await _deskService.GetAllAsync();

        return Ok(desks.Select(DeskMappings.ToResponse));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var desk = await _deskService.GetByIdAsync(id);

        if (desk is null)
            return NotFound();

        return Ok(desk.ToResponse());
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable([FromQuery] TimeRangeRequest dto)
    {
        if (dto.Start >= dto.End)
            return BadRequest("Invalid time range.");

        var range = new TimeRange(dto.Start!.Value, dto.End!.Value);

        var desks = await _deskService.GetAvailableAsync(range);

        return Ok(desks.Select(DeskMappings.ToResponse));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDeskRequest dto)
    {
        var desk = new Desk
        {
            Code = dto.Code,
            IsActive = dto.IsActive,
        };

        var createdDesk = await _deskService.CreateAsync(desk);

        return CreatedAtAction(nameof(Post), new { id = createdDesk.Id }, createdDesk.ToResponse());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateDeskRequest dto)
    {
        var desk = new Desk
        {
            Id = id,
            Code = dto.Code,
            IsActive = dto.IsActive,
        };

        var updatedDesk = await _deskService.UpdateAsync(desk);
        if (updatedDesk == null)
            return NotFound();

        return Ok(updatedDesk.ToResponse());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _deskService.DeleteByIdAsync(id);
        return NoContent();
    }
}
