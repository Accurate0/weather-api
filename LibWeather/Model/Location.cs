using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LibWeather.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Location
    {
        // default
        Unknown = 0,
        Perth,
        PerthAirport,
        RottnestIsland,
        MelbourneOlympicPark,
        Hobart,
        AdelaideWestTerrace,
        Brisbane,
        DarwinAirport,
        SydneyObservatoryHill,
        Canberra,
    }
}
