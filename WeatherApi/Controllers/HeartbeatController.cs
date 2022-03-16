using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("Heartbeat")]
public class HeartbeatController : ControllerBase
{
    public HeartbeatController()
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetHeartbeat()
    {
        return new NoContentResult();
    }
}
