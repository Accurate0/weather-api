using LibWeather.Model;

namespace LibWeather.Extensions
{
    public static class WeatherExtension
    {
        public static WeatherData Merge(this WeatherData w1, WeatherData w2)
        {
            if (!w1.Name.Equals(w2.Name))
            {
                throw new ArgumentException();
            }

            w1.HistorialWeather = w1.HistorialWeather.UnionBy(w2.HistorialWeather, d => d.Time).ToList();
            w1.CurrentWeather = DateTime.Compare(w1.CurrentWeather.Time, w2.CurrentWeather.Time) >= 0
                                ? w1.CurrentWeather
                                : w2.CurrentWeather;

            w1.LastUpdate = DateTime.Compare(w1.LastUpdate, w2.LastUpdate) >= 0
                    ? w1.LastUpdate
                    : w2.LastUpdate;

            return w1;
        }
    }
}
