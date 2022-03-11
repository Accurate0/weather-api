using Microsoft.Azure.Cosmos;
using WeatherApi.Model;

namespace WeatherApi.Services
{
    public class CosmosDb
    {
        private IConfiguration _config;
        private CosmosClient _client;
        private Database _database;
        private Container _container;

        public CosmosDb(IConfiguration configuration)
        {
            _config = configuration;
            _client = new CosmosClient(configuration.GetConnectionString("database"));
        }

        public async Task CreateDatabaseAsync()
        {
            _database = await _client.CreateDatabaseIfNotExistsAsync("WeatherDatabase");
        }

        public async Task CreateContainerAsync()
        {
            _container = await _database.CreateContainerIfNotExistsAsync("WeatherData", "/Id");
        }

        public async Task AddTestValue()
        {
            await _container.CreateItemAsync<Weather>(new Weather
            {
                Id = $"{new Random().NextInt64()}"
            });
        }
    }
}
