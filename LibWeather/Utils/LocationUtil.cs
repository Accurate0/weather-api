using LibWeather.Model;

namespace LibWeather.Utils
{
    public class LocationUtil
    {
        public static Location GetLocationFromWmoId(int wmo)
        {
            switch (wmo)
            {
                case 94608:
                    return Location.Perth;
                case 94610:
                    return Location.PerthAirport;
                case 94602:
                    return Location.RottnestIsland;
                case 95936:
                    return Location.MelbourneOlympicPark;
                case 94970:
                    return Location.Hobart;
                case 94648:
                    return Location.AdelaideWestTerrace;
                case 94576:
                    return Location.Brisbane;
                case 94120:
                    return Location.DarwinAirport;
                case 94768:
                    return Location.SydneyObservatoryHill;
                case 94926:
                    return Location.Canberra;
                default:
                    return Location.Unknown;
            }
        }


        public static Location GetLocationFromUserString(string location)
        {
            Location locationEnum;
            var result = Enum.TryParse<Location>(location, true, out locationEnum);
            if (result)
            {
                return locationEnum;
            }
            else
            {
                return ResolveAlias(location);
            }
        }

        private static Location ResolveAlias(string name)
        {
            switch (name.ToLower())
            {
                case "melbourne":
                    return Location.MelbourneOlympicPark;

                case "adelaide":
                    return Location.AdelaideWestTerrace;

                case "darwin":
                    return Location.DarwinAirport;

                case "sydney":
                    return Location.SydneyObservatoryHill;

                default:
                    return Location.Unknown;
            }
        }
    }
}
