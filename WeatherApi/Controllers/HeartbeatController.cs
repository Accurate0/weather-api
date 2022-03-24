using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("Heartbeat")]
public class HeartbeatController : ControllerBase
{
    private DatabaseService _databaseService;
    public HeartbeatController(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetHeartbeat()
    {
        var t = _databaseService.Heartbeat();
        await t;

        // check cosmosdb connection
        if (t.IsCompletedSuccessfully)
        {
            return new NoContentResult();
        }
        else
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
