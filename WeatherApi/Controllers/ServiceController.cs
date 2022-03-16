using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using LibWeather.Model;
using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("Service")]
public partial class ServiceController : ControllerBase
{
    private DatabaseService _database;
    public ServiceController(DatabaseService database)
    {
        _database = database;
    }

    public class LastUpdateResponse
    {
        public DateTime LastUpdate { get; set; }
    }

    [HttpGet("LastUpdate")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LastUpdateResponse))]
    public async Task<IActionResult> GetLastUpdate()
    {
        // all have the exact same last update, so use perth as default
        var weather = await _database.GetWeather(Location.Perth);
        return new OkObjectResult(new LastUpdateResponse { LastUpdate = weather.LastUpdate });
    }
}
