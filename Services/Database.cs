using Microsoft.Azure.Cosmos;
using WeatherApi.Model;

namespace WeatherApi.Services
{
    public class Database
    {
        public readonly string DatabaseName = "WeatherDatabase";
        public readonly string ContainerName = "v1";
        private ILogger<Database> _logger;
        private IConfiguration _config;
        private CosmosClient _client;
        private Microsoft.Azure.Cosmos.Database _database;
        private Container _container;
        private Container _latestContainer;

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

        public async Task AddWeather(Weather item)
        {
            var resp = await _container.UpsertItemAsync<Weather>(item);
            _logger.LogInformation($"request charge: {resp.RequestCharge}");
        }

        public async Task<Weather> GetWeather(Location city)
        {
            var resp = await _container.ReadItemAsync<Weather>(city.ToString(), PartitionKey.None);
            _logger.LogInformation($"request charge: {resp.RequestCharge}");
            return resp;
        }

        private async Task CreateDatabaseAsync()
        {
            _database = await _client.CreateDatabaseIfNotExistsAsync(DatabaseName);
        }

        private async Task CreateContainerAsync()
        {
            _container = await _database.CreateContainerIfNotExistsAsync(ContainerName, "/Id");
        }
    }
}
