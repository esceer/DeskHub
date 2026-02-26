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
    public IActionResult Get()
    {
        var desks = _deskService.GetAll();
        return Ok(desks);
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var desk = _deskService.GetById(id);
        return Ok(desk);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Desk desk)
    {
        var createdDesk = _deskService.Create(desk);
        return CreatedAtAction(nameof(Post), new { id = createdDesk.Id }, createdDesk);
    }

    [HttpPut("{id}")]
    [IdExistsFilter]
    public IActionResult Put(Guid id, [FromBody] Desk desk)
    {
        if (desk.Id != id)
        {
            return BadRequest("ID in URL and body must match.");
        }

        var updatedDesk = _deskService.Update(desk);
        if (updatedDesk == null)
        {
            return NotFound();
        }
        return Ok(updatedDesk);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _deskService.DeleteById(id);
        return NoContent();
    }
}
