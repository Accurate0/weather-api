using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("Observations")]
public partial class ObservationController : ControllerBase
{
    private DatabaseService _database;

    public ObservationController(DatabaseService database)
    {
        _database = database;
    }
}
