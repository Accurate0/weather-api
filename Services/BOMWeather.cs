using WeatherApi.Extensions;
using WeatherApi.Model;
using Newtonsoft.Json;
using AutoMapper;

namespace WeatherApi.Services
{
    public class BOMWeather : BackgroundService
    {
        private readonly Dictionary<Location, string> FetchLocationUrls = new()
        {
            [Location.Perth] = "http://reg.bom.gov.au/fwo/IDW60901/IDW60901.94608.json",
            [Location.PerthAirport] = "http://reg.bom.gov.au/fwo/IDW60901/IDW60901.94610.json",
            [Location.RottnestIsland] = "http://reg.bom.gov.au/fwo/IDW60901/IDW60901.94602.json",
        };
        private readonly ILogger<BOMWeather> _logger;
        private readonly Database _database;
        private readonly IMapper _mapper;
        private static readonly HttpClient _client = new HttpClient();

        public BOMWeather(ILogger<BOMWeather> logger, Database database, IMapper mapper)
        {
            _logger = logger;
            _database = database;
            _mapper = mapper;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var kvp in FetchLocationUrls)
                {
                    var resp = await _client.GetAsync(kvp.Value);
                    resp.EnsureSuccessStatusCode();

                    var json = await resp.Content.ReadAsStringAsync();
                    var weatherStationData = JsonConvert.DeserializeObject<WeatherStationData>(json);
                    var weather = _mapper.Map<WeatherData>(weatherStationData);

                    try
                    {
                        var tryGetWeather = await _database.GetWeather(kvp.Key);
                        var weatherInDatabase = tryGetWeather;
                        weatherInDatabase.Merge(weather);

                        await _database.AddWeather(weatherInDatabase);
                    }
                    catch (Microsoft.Azure.Cosmos.CosmosException)
                    {
                        await _database.AddWeather(weather);
                    }

                    _logger.LogInformation($"{kvp.Key.ToString()}: Added to database successfully");
                }
                // 15 minutes
                await Task.Delay(900000, cancellationToken);
            }
        }
    }
}
