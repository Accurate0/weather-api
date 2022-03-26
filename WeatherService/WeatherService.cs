
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
    private static DateTime _now = DateTime.UtcNow;
    private static HttpClient _client = new HttpClient();
    private static MapperConfiguration _mapperConfig = new(cfg =>
   {
       cfg.AddProfile<WeatherDataMapper>();
       cfg.AddProfile<WeatherMapper>();
   });
    private static IMapper _mapper = new Mapper(_mapperConfig);

    public async Task UpdateLocation(ILambdaContext context, Container container, Location location, string url)
    {
        var logger = context.Logger;
        var response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var responseAsJson = await response.Content.ReadAsStringAsync();
        var weatherStationData = JsonConvert.DeserializeObject<WeatherStationData>(responseAsJson);
        var weather = _mapper.Map<WeatherData>(weatherStationData);

        weather.Version = WeatherData.CurrentVersion;
        weather.LastUpdate = _now;

        try
        {
            var tryGetWeather = await container.ReadItemAsync<WeatherData>(location.ToString(), PartitionKey.None);
            WeatherData weatherInDatabase = tryGetWeather.Resource;
            if (!weatherInDatabase.Version.HasValue || weatherInDatabase.Version < weather.Version)
            {
                logger.LogWarning($"{location.ToString()}: Database has version {weatherInDatabase.Version}, current is {weather.Version}");
                logger.LogWarning($"{location.ToString()}: Replacing data instead...");
                // Don't merge, replace instead
                weather.Version = WeatherData.CurrentVersion;
                await container.UpsertItemAsync<WeatherData>(weather);
            }
            else if (weatherInDatabase.Version > weather.Version)
            {
                logger.LogCritical($"{location.ToString()}: Database has version \"{weatherInDatabase.Version}\", current is \"{weather.Version}\"");
                logger.LogCritical($"{location.ToString()}: this makes no sense...");
                throw new InvalidDataException("Data is inconsistent, aborting...");
            }
            else
            {
                logger.LogInformation($"{location.ToString()}: Merging with current data...");
                weatherInDatabase.Merge(weather);

                // remove all older than 90 days
                var removed = weatherInDatabase.HistorialWeather.RemoveAll(w => (_now - w.UTCTime).TotalDays > 90);
                logger.LogInformation($"{location.ToString()}: Trimmed historical entries: {removed}");
                await container.UpsertItemAsync<WeatherData>(weatherInDatabase);
            }

        }
        catch (CosmosException ce)
        {
            logger.LogCritical(ce.StackTrace);
            await container.UpsertItemAsync<WeatherData>(weather);
        }
        logger.LogInformation($"{location.ToString()}: Added to database successfully");
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

        var cosmosClient = new CosmosClient(connectionString ?? CosmosDb.LocalConnectionString, cosmosClientOptions);
        Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(Constants.DatabaseName);
        Container container = await database.CreateContainerIfNotExistsAsync(Constants.ContainerName, Constants.PartitionKey);

        await Task.WhenAll(Constants.FetchLocationUrls.Select(kvp => UpdateLocation(context, container, kvp.Key, kvp.Value)));
    }
}
