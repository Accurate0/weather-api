using LibWeather.Extensions;
using LibWeather.Model;
using WeatherApi.Services;
using Newtonsoft.Json;
using AutoMapper;

namespace WeatherApi.Services
{
    public class BOMObservations : BackgroundService
    {
        private readonly ILogger<BOMObservations> _logger;
        private readonly Database _database;
        private readonly IMapper _mapper;
        private static readonly HttpClient _client = new HttpClient();

        public BOMObservations(ILogger<BOMObservations> logger, Database database, IMapper mapper)
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
