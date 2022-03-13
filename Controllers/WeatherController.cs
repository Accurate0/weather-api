using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("Observations")]
public partial class ObservationController : ControllerBase
{
    private readonly ILogger<ObservationController> _logger;
    private IConfiguration _configuration;
    private Database _database;
    private IMapper _mapper;

    public ObservationController(ILogger<ObservationController> logger, IConfiguration configuration, Database database, IMapper mapper)
    {
        _logger = logger;
        _configuration = configuration;
        _database = database;
        _mapper = mapper;
    }
}
