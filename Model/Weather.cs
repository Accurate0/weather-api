namespace WeatherApi.Model
{
    public class Weather
    {
        public string Test { get; set; } = "Test";
        [Newtonsoft.Json.JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
