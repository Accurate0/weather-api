using System.Net.Http;

using Microsoft.Azure.Cosmos;

using Amazon.Lambda.Core;

using AutoMapper;

using LibWeather;
using LibWeather.Extensions;
using LibWeather.Model;
using LibWeather.Model.Mappers;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace WeatherService2;

public class Function
{

    public async Task FunctionHandler(ILambdaContext context)
    {
        var connectionString = Environment.GetEnvironmentVariable("cosmosdb_connection_string");

        var client = new HttpClient();
        var database = new CosmosClient(connectionString);
        var container = database.GetContainer(Constants.DatabaseName, Constants.ContainerName);


        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<WeatherDataMapper>();
            cfg.AddProfile<WeatherMapper>();
        });

        IMapper mapper = new Mapper(mapperConfig);

        foreach (var kvp in Constants.FetchLocationUrls)
        {
            var resp = await client.GetAsync(kvp.Value);
            resp.EnsureSuccessStatusCode();

            var json = await resp.Content.ReadAsStringAsync();
            var weatherStationData = JsonConvert.DeserializeObject<WeatherStationData>(json);
            var weather = mapper.Map<WeatherData>(weatherStationData);

            try
            {
                var tryGetWeather = await container.ReadItemAsync<WeatherData>(kvp.Key.ToString(), PartitionKey.None);
                WeatherData weatherInDatabase = tryGetWeather.Resource;
                weatherInDatabase.Merge(weather);

                await container.UpsertItemAsync<WeatherData>(weatherInDatabase);
            }
            catch (CosmosException)
            {
                await container.UpsertItemAsync<WeatherData>(weather);
            }
            context.Logger.LogInformation($"{kvp.Key.ToString()}: Added to database successfully");
        }
    }
}
