using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("Heartbeat")]
public class HeartbeatController : ControllerBase
{
    private readonly ILogger<HeartbeatController> _logger;

    public HeartbeatController(ILogger<HeartbeatController> logger, IConfiguration configuration, Database database, IMapper mapper)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetHeartbeat()
    {
        return new NoContentResult();
    }
}
