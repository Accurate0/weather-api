using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using AutoMapper;

using LibWeather;
using LibWeather.Extensions;
using LibWeather.Model;
using LibWeather.Model.Mappers;

using Newtonsoft.Json;

namespace WeatherService
{
    public class WeatherService
    {
        [FunctionName("WeatherService")]
        public async Task Run([TimerTrigger("0 15/30 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var client = new HttpClient();
            var database = new CosmosClient(config.GetConnectionString("Database"));
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
                log.LogInformation($"{kvp.Key.ToString()}: Added to database successfully");
            }
        }
    }
}
