using Microsoft.AspNetCore.Mvc;
using WeatherApi.Model;
using AutoMapper;
using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("weather")]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private IConfiguration _configuration;
    private Database _database;
    private IMapper _mapper;

    public WeatherController(ILogger<WeatherController> logger, IConfiguration configuration, Database database, IMapper mapper)
    {
        _logger = logger;
        _configuration = configuration;
        _database = database;
        _mapper = mapper;
    }

    [HttpGet("current")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherData.Weather))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromQuery] string location)
    {
        Location locationEnum;
        var result = Enum.TryParse<Location>(location, true, out locationEnum);

        if (result)
        {
            var weather = await _database.GetWeather(locationEnum);
            return new OkObjectResult(weather.CurrentWeather);
        }
        else
        {
            return new BadRequestResult();
        }
    }
}
