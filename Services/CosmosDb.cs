using Microsoft.Azure.Cosmos;
using WeatherApi.Model;
using System.Configuration;

namespace WeatherApi.Services
{
    public class CosmosDb
    {
        private CosmosClient _client;
        private Database _database;
        private Container _container;

        public CosmosDb()
        {
            _client = new CosmosClient(System.Configuration.ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
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
            await _container.CreateItemAsync<Weather>(new Weather());
        }
    }
}
