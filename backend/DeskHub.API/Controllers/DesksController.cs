using DeskHub.API.DTOs.Requests;
using DeskHub.API.DTOs.Responses;
using DeskHub.Application.Interfaces.Services;
using DeskHub.Backend.Api.Filters;
using DeskHub.Domain.Entities;
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

        var response = desks.Select(d => new DeskResponse
        {
            Id = d.Id,
            Code = d.Code,
            IsActive = d.IsActive
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var desk = await _deskService.GetByIdAsync(id);

        if (desk is null)
            return NotFound();

        var response = new DeskResponse
        {
            Id = desk.Id,
            Code = desk.Code,
            IsActive = desk.IsActive
        };
        return Ok(response);
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

        var response = new DeskResponse
        {
            Id = createdDesk.Id,
            Code = createdDesk.Code,
            IsActive = createdDesk.IsActive
        };
        return CreatedAtAction(nameof(Post), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    [IdExistsFilter]
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

        var response = new DeskResponse
        {
            Id = updatedDesk.Id,
            Code = updatedDesk.Code,
            IsActive = updatedDesk.IsActive
        };
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _deskService.DeleteByIdAsync(id);
        return NoContent();
    }
}
