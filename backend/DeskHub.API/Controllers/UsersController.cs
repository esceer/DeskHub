using DeskHub.Application.Interfaces.Services;
using DeskHub.Backend.Api.Filters;
using DeskHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DeskHub.Backend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService service)
    {
        _userService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user)
    {
        var createdUser = await _userService.CreateAsync(user);
        return CreatedAtAction(nameof(Post), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    [IdExistsFilter]
    public async Task<IActionResult> Put(Guid id, [FromBody] User user)
    {
        if (user.Id != id)
        {
            return BadRequest("ID in URL and body must match.");
        }

        var updatedUser = await _userService.UpdateAsync(user);
        if (updatedUser == null)
        {
            return NotFound();
        }
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _userService.DeleteByIdAsync(id);
        return NoContent();
    }
}
