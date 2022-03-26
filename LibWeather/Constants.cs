using LibWeather.Model;

namespace LibWeather
{
    public static class Constants
    {
        public const string DatabaseName = "WeatherDatabase";
        public const string ContainerName = "WeatherContainer";

        public const string PartitionKey = "/Id";

        public static Dictionary<Location, string> FetchLocationUrls = new()
        {
            [Location.Perth] = "http://reg.bom.gov.au/fwo/IDW60901/IDW60901.94608.json",
            [Location.PerthAirport] = "http://reg.bom.gov.au/fwo/IDW60901/IDW60901.94610.json",
            [Location.RottnestIsland] = "http://reg.bom.gov.au/fwo/IDW60901/IDW60901.94602.json",
            [Location.MelbourneOlympicPark] = "http://reg.bom.gov.au/fwo/IDV60901/IDV60901.95936.json",
            [Location.Hobart] = "http://reg.bom.gov.au/fwo/IDT60901/IDT60901.94970.json",
            [Location.AdelaideWestTerrace] = "http://reg.bom.gov.au/fwo/IDS60901/IDS60901.94648.json",
            [Location.Brisbane] = "http://reg.bom.gov.au/fwo/IDQ60901/IDQ60901.94576.json",
            [Location.DarwinAirport] = "http://reg.bom.gov.au/fwo/IDD60901/IDD60901.94120.json",
            [Location.SydneyObservatoryHill] = "http://reg.bom.gov.au/fwo/IDN60901/IDN60901.94768.json",
            [Location.Canberra] = "http://reg.bom.gov.au/fwo/IDN60903/IDN60903.94926.json",
        };
    }
}
