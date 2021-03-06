using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using LibWeather.Model;
using LibWeather.Utils;

namespace WeatherApi.Controllers;

public partial class ObservationController : ControllerBase
{
    public class HistoryParameters
    {
        [BindRequired]
        public string Location { get; set; }
        [BindRequired]
        public int Count { get; set; }
    }

    [HttpGet("History")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WeatherData.Weather>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetHistory([FromQuery] HistoryParameters parameters)
    {
        Location location = LocationUtil.GetLocationFromUserString(parameters.Location);
        if (location != Location.Unknown && parameters.Count > 0)
        {
            var weather = await _database.GetWeather(location);
            var response = weather.HistorialWeather
                            .OrderByDescending(w => w.UTCTime)
                            .Take(parameters.Count)
                            .ToList();

            return new OkObjectResult(response);
        }
        else
        {
            if (location == Location.Unknown)
            {
                var locationList = Enum.GetNames<Location>()
                                    .Where(x => x != "Unknown")
                                    .Aggregate((s, current) => $"{s}, {current}");
                return new BadRequestObjectResult(new
                {
                    message = $"Location must be one of {locationList}"
                });
            }

            if (parameters.Count <= 0)
            {
                return new BadRequestObjectResult(new
                {
                    message = $"Count must be greater than 0"
                });
            }

            // literally won't happen
            return new BadRequestResult();
        }
    }
}
