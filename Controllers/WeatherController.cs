using Microsoft.AspNetCore.Mvc;
using WeatherApi.Model;

namespace WeatherApi.Controllers;

[ApiController]
[Route("Weather")]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;

    public WeatherController(ILogger<WeatherController> logger)
    {
        _logger = logger;
    }

    [HttpGet("Current")]
    public Weather Get()
    {
        _logger.LogInformation("request for current weather");
        return new Weather();
    }
}
