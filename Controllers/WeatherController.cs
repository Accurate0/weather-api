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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CurrentWeather))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromQuery] string city)
    {
        if (city.ToLower().Equals("perth"))
        {
            var weather = await _database.GetLatestWeather();
            return CreatedAtAction(nameof(Get), _mapper.Map<CurrentWeather>(weather));
        }
        else
        {
            return new BadRequestResult();
        }
    }
}
