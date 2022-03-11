using Microsoft.AspNetCore.Mvc;
using WeatherApi.Model;
using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("Weather")]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private IConfiguration _configuration;
    private CosmosDb _cosmosDb;

    public WeatherController(ILogger<WeatherController> logger, IConfiguration configuration, CosmosDb cosmosDb)
    {
        _logger = logger;
        _configuration = configuration;
        _cosmosDb = cosmosDb;
    }

    [HttpGet("Current")]
    public async Task<Weather> Get()
    {
        // log sensitive information :D
        System.Diagnostics.Trace.TraceInformation(_configuration.GetConnectionString("Database"));
        _logger.LogInformation("request for current weather");

        await _cosmosDb.CreateDatabaseAsync();
        await _cosmosDb.CreateContainerAsync();
        await _cosmosDb.AddTestValue();

        return new Weather();
    }
}
