using DeskHub.API.DTOs.Requests;
using DeskHub.API.DTOs.Responses;
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

        var response = users.Select(u => new UserResponse
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Role = u.Role
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        var response = new UserResponse
        {
            Id = id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
        return Ok(user);
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

        var response = new UserResponse
        {
            Id = createdUser.Id,
            Name = createdUser.Name,
            Email = createdUser.Email,
            Role = createdUser.Role
        };
        return CreatedAtAction(nameof(Post), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    [IdExistsFilter]
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

        var response = new UserResponse
        {
            Id = updatedUser.Id,
            Name = updatedUser.Name,
            Email = updatedUser.Email,
            Role = updatedUser.Role
        };
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _userService.DeleteByIdAsync(id);
        return NoContent();
    }
}
