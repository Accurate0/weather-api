using LibWeather.Model;

namespace LibWeather
{
    public static class Constants
    {
        public const string DatabaseName = "WeatherDatabase";
        public const string ContainerName = "v1";

        public static Dictionary<Location, string> FetchLocationUrls = new()
        {
            [Location.Perth] = "http://reg.bom.gov.au/fwo/IDW60901/IDW60901.94608.json",
            [Location.PerthAirport] = "http://reg.bom.gov.au/fwo/IDW60901/IDW60901.94610.json",
            [Location.RottnestIsland] = "http://reg.bom.gov.au/fwo/IDW60901/IDW60901.94602.json",
        };
    }
}
