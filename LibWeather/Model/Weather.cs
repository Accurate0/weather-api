using Newtonsoft.Json;
namespace LibWeather.Model
{
    public class WeatherData
    {
        public const int CurrentVersion = 1;
        public class Weather
        {
            public DateTime LocalTime { get; set; }
            public DateTime UTCTime { get; set; }
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
        public int? Version { get; set; }
        public Weather CurrentWeather { get; set; }
        public List<Weather> HistorialWeather { get; set; }
    }
}
