using System.Configuration;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using LibWeather;
using Microsoft.Extensions.Configuration;

namespace WeatherService
{
    public class WeatherService
    {
        [FunctionName("WeatherService")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            log.LogCritical(config.GetConnectionString("Database"));
        }
    }
}
