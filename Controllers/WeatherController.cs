using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WeatherApi.Model;
using AutoMapper;
using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("Weather")]
public partial class WeatherController : ControllerBase
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
}
