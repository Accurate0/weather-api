using Newtonsoft.Json;
namespace LibWeather.Model
{
    public class WeatherData
    {
        public class Weather
        {
            public DateTime Time { get; set; }
            public double AirTemperature { get; set; }
            public double ApparentTemperature { get; set; }
            public double Pressure { get; set; }
            public string WindDirection { get; set; }
            public double WindSpeedKmh { get; set; }
            public double DewPoint { get; set; }
            public int Humidity { get; set; }
        }
        [JsonProperty("id")]
        public Location Name { get; set; }
        public DateTime LastUpdate { get; set; }
        public Weather CurrentWeather { get; set; }
        public List<Weather> HistorialWeather { get; set; }
    }
}
