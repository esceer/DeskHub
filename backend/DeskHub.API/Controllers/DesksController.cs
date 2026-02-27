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
        return Ok(desks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var desk = await _deskService.GetByIdAsync(id);
        return Ok(desk);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Desk desk)
    {
        var createdDesk = await _deskService.CreateAsync(desk);
        return CreatedAtAction(nameof(Post), new { id = createdDesk.Id }, createdDesk);
    }

    [HttpPut("{id}")]
    [IdExistsFilter]
    public async Task<IActionResult> Put(Guid id, [FromBody] Desk desk)
    {
        if (desk.Id != id)
        {
            return BadRequest("ID in URL and body must match.");
        }

        var updatedDesk = await _deskService.UpdateAsync(desk);
        if (updatedDesk == null)
        {
            return NotFound();
        }
        return Ok(updatedDesk);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _deskService.DeleteByIdAsync(id);
        return NoContent();
    }
}
