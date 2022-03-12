using Newtonsoft.Json;
using WeatherApi.Services;
namespace WeatherApi.Model
{
    public class Weather
    {
        public class WeatherData
        {
            public DateTime Time { get; set; }
            public double AirTemp { get; set; }
            public double Pressure { get; set; }
        }
        public string Name { get; set; }
        public List<WeatherData> Data { get; set; }
        [JsonProperty("id")]
        public string LatestDataTime { get; set; }
    }
}
