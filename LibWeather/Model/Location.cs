using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LibWeather.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Location
    {
        Unknown,
        Perth,
        PerthAirport,
        RottnestIsland
    }
}
