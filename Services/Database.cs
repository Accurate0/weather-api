using Microsoft.Azure.Cosmos;
using WeatherApi.Model;

namespace WeatherApi.Services
{
    public class Database
    {
        public readonly string DatabaseName = "WeatherDatabase";
        public readonly string ContainerName = "v1";
        public readonly string LatestContainerName = "Latest";
        private IConfiguration _config;
        private CosmosClient _client;
        private Microsoft.Azure.Cosmos.Database _database;
        private Container _container;
        private Container _latestContainer;

        public Database(IConfiguration configuration)
        {
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
            await _latestContainer.UpsertItemAsync<LatestContainer>(new LatestContainer
            {
                WeatherId = item.LatestDataTime,
            });

            await _container.UpsertItemAsync<Weather>(item);
        }

        public async Task<Weather> GetLatestWeather()
        {
            var latest = await _latestContainer.ReadItemAsync<LatestContainer>(LatestContainer.LatestId, PartitionKey.None);
            return await _container.ReadItemAsync<Weather>(latest.Resource.WeatherId, PartitionKey.None);
        }

        private async Task CreateDatabaseAsync()
        {
            _database = await _client.CreateDatabaseIfNotExistsAsync(DatabaseName);
        }

        private async Task CreateContainerAsync()
        {
            _container = await _database.CreateContainerIfNotExistsAsync(ContainerName, "/Id");
            _latestContainer = await _database.CreateContainerIfNotExistsAsync(LatestContainerName, "/Id");
        }
    }
}
