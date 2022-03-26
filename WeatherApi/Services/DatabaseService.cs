using Microsoft.Azure.Cosmos;

using LibWeather;
using LibWeather.Model;

namespace WeatherApi.Services
{
    public class DatabaseService
    {
        private ILogger<DatabaseService> _logger;
        private IConfiguration _config;
        private CosmosClient _client;
        private Database _database;
        private Container _container;

        public DatabaseService(IConfiguration configuration, ILogger<DatabaseService> logger)
        {
            _logger = logger;
            _config = configuration;
            CosmosClientOptions? cosmosClientOptions = null;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                cosmosClientOptions = new CosmosClientOptions()
                {
                    HttpClientFactory = () =>
                    {
                        HttpMessageHandler httpMessageHandler = new HttpClientHandler()
                        {
                            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                        };

                        return new HttpClient(httpMessageHandler);
                    },
                    ConnectionMode = ConnectionMode.Gateway
                };
            }

            _client = new CosmosClient(configuration.GetConnectionString("Database"), cosmosClientOptions);
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
            _container = await _database.CreateContainerIfNotExistsAsync(Constants.ContainerName, Constants.PartitionKey);
        }

        public Task Heartbeat()
        {
            return _client.ReadAccountAsync();
        }
    }
}
