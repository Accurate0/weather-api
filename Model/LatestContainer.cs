using Newtonsoft.Json;

namespace WeatherApi.Model
{
    public class LatestContainer
    {
        public static readonly string LatestId = "Latest";
        [JsonProperty("id")]
        public string Id { get; } = LatestId;
        public string WeatherId { get; set; }
    }
}
