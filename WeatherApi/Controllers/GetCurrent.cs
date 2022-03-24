using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using LibWeather.Model;
using LibWeather.Utils;

namespace WeatherApi.Controllers;

public partial class ObservationController : ControllerBase
{
    public class CurrentParameters
    {
        [BindRequired]
        public string Location { get; set; }
    }

    [HttpGet("Current")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherData.Weather))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCurrent([FromQuery] CurrentParameters parameters)
    {
        Location location = LocationUtil.GetLocationFromUserString(parameters.Location);
        if (location != Location.Unknown)
        {
            var weather = await _database.GetWeather(location);
            var response = weather.CurrentWeather;
            return new OkObjectResult(response);
        }
        else
        {
            var locationList = Enum.GetNames<Location>()
                                .Where(x => x != "Unknown")
                                .Aggregate((s, current) => $"{s}, {current}");
            return new BadRequestObjectResult(new
            {
                message = $"Location must be one of {locationList}"
            });
        }
    }
}
