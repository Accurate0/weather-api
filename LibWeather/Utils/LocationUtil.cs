using LibWeather.Model;

namespace LibWeather.Utils
{
    public class LocationUtil
    {
        public static Dictionary<int, Location> WmoLocationMap = new()
        {
            [94608] = Location.Perth,
            [94610] = Location.PerthAirport,
            [94602] = Location.RottnestIsland,
            [95936] = Location.MelbourneOlympicPark,
            [94970] = Location.Hobart,
            [94648] = Location.AdelaideWestTerrace,
            [94576] = Location.Brisbane,
            [94120] = Location.DarwinAirport,
            [94768] = Location.SydneyObservatoryHill,
            [94926] = Location.Canberra,
        };
        public static Location GetLocationFromWmoId(int wmo)
        {
            Location location;
            WmoLocationMap.TryGetValue(wmo, out location);
            return location;
        }


        public static Location GetLocationFromUserString(string location)
        {
            Location locationEnum;
            var result = Enum.TryParse<Location>(location, true, out locationEnum);
            return result ? locationEnum : ResolveAlias(location);
        }

        // Alias weirdly named stations
        public static Dictionary<string, Location> AliasMap = new()
        {
            ["melbourne"] = Location.MelbourneOlympicPark,
            ["adelaide"] = Location.AdelaideWestTerrace,
            ["darwin"] = Location.DarwinAirport,
            ["sydney"] = Location.SydneyObservatoryHill,
            ["adelaide"] = Location.SydneyObservatoryHill,
        };

        private static Location ResolveAlias(string name)
        {
            Location location;
            AliasMap.TryGetValue(name.ToLower(), out location);
            return location;
        }
    }
}
