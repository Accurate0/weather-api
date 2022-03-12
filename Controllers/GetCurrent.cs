using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WeatherApi.Model;

namespace WeatherApi.Controllers;

public partial class WeatherController : ControllerBase
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
        Location locationEnum;
        var result = Enum.TryParse<Location>(parameters.Location, true, out locationEnum);
        if (result)
        {
            var weather = await _database.GetWeather(locationEnum);
            var response = weather.CurrentWeather;
            return new OkObjectResult(response);
        }
        else
        {
            return new BadRequestResult();
        }
    }
}
