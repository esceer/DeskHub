using Microsoft.AspNetCore.Mvc;

namespace DeskHub.Backend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HealthController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Health()
    {
        return Ok();
    }
}
