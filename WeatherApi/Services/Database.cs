using Microsoft.Azure.Cosmos;

using LibWeather;
using LibWeather.Model;

namespace WeatherApi.Services
{
    public class Database
    {
        private ILogger<Database> _logger;
        private IConfiguration _config;
        private CosmosClient _client;
        private Microsoft.Azure.Cosmos.Database _database;
        private Container _container;

        public Database(IConfiguration configuration, ILogger<Database> logger)
        {
            _logger = logger;
            _config = configuration;
            _client = new CosmosClient(configuration.GetConnectionString("Database"));
        }

        public async Task InitAsync()
        {
            await CreateDatabaseAsync();
            await CreateContainerAsync();
        }

        public async Task AddWeather(WeatherData item)
        {
            var resp = await _container.UpsertItemAsync<WeatherData>(item);
            _logger.LogInformation($"request charge: {resp.RequestCharge}");
        }

        public async Task<WeatherData> GetWeather(Location location)
        {
            var resp = await _container.ReadItemAsync<WeatherData>(location.ToString(), PartitionKey.None);
            _logger.LogInformation($"request charge: {resp.RequestCharge}");
            return resp;
        }

        private async Task CreateDatabaseAsync()
        {
            _database = await _client.CreateDatabaseIfNotExistsAsync(Constants.DatabaseName);
        }

        private async Task CreateContainerAsync()
        {
            _container = await _database.CreateContainerIfNotExistsAsync(Constants.ContainerName, "/Id");
        }
    }
}
