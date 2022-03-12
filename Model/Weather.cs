using Newtonsoft.Json;
using WeatherApi.Services;
namespace WeatherApi.Model
{
    public partial class Weather
    {
        public class WeatherData
        {
            public DateTime Time { get; set; }
            public double AirTemp { get; set; }
            public double Pressure { get; set; }
        }
        [JsonProperty("id")]
        public Location Name { get; set; }
        public List<WeatherData> Data { get; set; }
        public CurrentWeather CurrentWeather { get; set; }
    }
}
