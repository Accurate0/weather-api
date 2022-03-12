using System.Globalization;

namespace WeatherApi.Utils
{
    public class DateTimeUtils
    {
        public static DateTime ParseDate(string datetime)
        {
            DateTime output;

            DateTime.TryParseExact(datetime, "yyyyMMddHHmmss", null, DateTimeStyles.None, out output);

            return output;
        }
    }
}
