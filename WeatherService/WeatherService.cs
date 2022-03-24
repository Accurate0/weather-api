
using Microsoft.Azure.Cosmos;

using Amazon.Lambda.Core;

using AutoMapper;

using LibWeather;
using LibWeather.Extensions;
using LibWeather.Model;
using LibWeather.Model.Mappers;

using Newtonsoft.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace WeatherService;

public class WeatherService
{
    public async Task UpdateLocation(Container container, Location location, string url, IMapper mapper, ILambdaContext context, HttpClient client, DateTime now)
    {
        var resp = await client.GetAsync(url);
        resp.EnsureSuccessStatusCode();

        var json = await resp.Content.ReadAsStringAsync();
        var weatherStationData = JsonConvert.DeserializeObject<WeatherStationData>(json);
        var weather = mapper.Map<WeatherData>(weatherStationData);

        weather.Version = WeatherData.CurrentVersion;
        weather.LastUpdate = now;

        try
        {
            var tryGetWeather = await container.ReadItemAsync<WeatherData>(location.ToString(), PartitionKey.None);
            WeatherData weatherInDatabase = tryGetWeather.Resource;
            if (!weatherInDatabase.Version.HasValue || weatherInDatabase.Version < weather.Version)
            {
                context.Logger.LogWarning($"{location.ToString()}: Database has version {weatherInDatabase.Version}, current is {weather.Version}");
                context.Logger.LogWarning($"{location.ToString()}: Replacing data instead...");
                // Don't merge, replace instead
                weather.Version = WeatherData.CurrentVersion;
                await container.UpsertItemAsync<WeatherData>(weather);
            }
            else if (weatherInDatabase.Version > weather.Version)
            {
                context.Logger.LogCritical($"{location.ToString()}: Database has version \"{weatherInDatabase.Version}\", current is \"{weather.Version}\"");
                context.Logger.LogCritical($"{location.ToString()}: this makes no sense...");
                throw new InvalidDataException("Data is inconsistent, aborting...");
            }
            else
            {
                context.Logger.LogInformation($"{location.ToString()}: Merging with current data...");
                weatherInDatabase.Merge(weather);
                await container.UpsertItemAsync<WeatherData>(weatherInDatabase);
            }

        }
        catch (CosmosException)
        {
            await container.UpsertItemAsync<WeatherData>(weather);
        }
        context.Logger.LogInformation($"{location.ToString()}: Added to database successfully");
    }
    public async Task Run(ILambdaContext context)
    {
        var connectionString = Environment.GetEnvironmentVariable("cosmosdb_connection_string");
        CosmosClientOptions? cosmosClientOptions = null;
        if (connectionString == null)
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

        var client = new HttpClient();
        var cosmosClient = new CosmosClient(connectionString ?? CosmosDb.LocalConnectionString, cosmosClientOptions);
        Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(Constants.DatabaseName);
        Container container = await database.CreateContainerIfNotExistsAsync(Constants.ContainerName, "/Id");

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<WeatherDataMapper>();
            cfg.AddProfile<WeatherMapper>();
        });

        IMapper mapper = new Mapper(mapperConfig);
        var now = DateTime.UtcNow;

        await Task.WhenAll(Constants.FetchLocationUrls.Select(kvp => UpdateLocation(container, kvp.Key, kvp.Value, mapper, context, client, now)));
    }
}
