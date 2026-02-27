using DeskHub.API.DTOs.Requests;
using DeskHub.API.Mappings;
using DeskHub.Application.Interfaces.Services;
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

        return Ok(users.Select(UserMappings.ToResponse));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        return Ok(user.ToResponse());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserRequest dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Role = dto.Role
        };

        var createdUser = await _userService.CreateAsync(user);

        return CreatedAtAction(nameof(Post), new { id = createdUser.Id }, createdUser.ToResponse());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateUserRequest dto)
    {
        var user = new User
        {
            Id = id,
            Name = dto.Name,
            Email = dto.Email,
            Role = dto.Role
        };

        var updatedUser = await _userService.UpdateAsync(user);
        if (updatedUser == null)
            return NotFound();

        return Ok(updatedUser.ToResponse());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _userService.DeleteByIdAsync(id);
        return NoContent();
    }
}
