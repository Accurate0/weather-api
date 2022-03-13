using System.Configuration;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using LibWeather;

namespace BackgroundService
{
    public class BackgroundService
    {
        [FunctionName("BackgroundService")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            var database = new CosmosClient(connectionString);
            var container = database.GetContainer(Constants.DatabaseName, Constants.ContainerName);
        }
    }
}
