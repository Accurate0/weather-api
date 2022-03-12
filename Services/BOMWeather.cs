using WeatherApi.Model;
using Newtonsoft.Json;
using AutoMapper;

namespace WeatherApi.Services
{
    public class BOMWeather : BackgroundService
    {
        private readonly string PerthWeatherStationUrl = "http://reg.bom.gov.au/fwo/IDW60901/IDW60901.94608.json";
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
                var resp = await _client.GetAsync(PerthWeatherStationUrl);
                resp.EnsureSuccessStatusCode();

                var json = await resp.Content.ReadAsStringAsync();
                var weatherStationData = JsonConvert.DeserializeObject<WeatherStationData>(json);
                var weather = _mapper.Map<Weather>(weatherStationData);

                await _database.AddWeather(weather);

                _logger.LogInformation("Added to database successfully");
                // 15 minutes
                await Task.Delay(900000, cancellationToken);
            }
        }
    }
}
