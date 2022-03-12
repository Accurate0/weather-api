using WeatherApi.Model;

namespace WeatherApi.Utils
{
    public class LocationUtil
    {
        public static Location GetLocationFromString(string location)
        {
            switch (location)
            {
                case "Perth":
                    return Location.Perth;
                case "Perth Airport":
                    return Location.PerthAirport;
                default:
                    return Location.Unknown;
            }
        }
    }
}
