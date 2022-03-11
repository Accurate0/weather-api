using Microsoft.AspNetCore.Mvc;
using WeatherApi.Model;

namespace WeatherApi.Controllers;

[ApiController]
[Route("Weather")]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private IConfiguration _configuration;

    public WeatherController(ILogger<WeatherController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet("Current")]
    public Weather Get()
    {
        // log sensitive information :D
        _logger.LogInformation(_configuration.GetConnectionString("Database"));
        _logger.LogInformation("request for current weather");
        return new Weather();
    }
}
