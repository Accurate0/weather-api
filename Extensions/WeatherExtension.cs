using WeatherApi.Model;

namespace WeatherApi.Extensions
{
    public static class WeatherExtension
    {
        public static WeatherData Merge(this WeatherData w1, WeatherData w2)
        {
            if (!w1.Name.Equals(w2.Name))
            {
                throw new ArgumentException();
            }

            return new WeatherData
            {
                Name = w1.Name,
                HistorialWeather = w1.HistorialWeather.UnionBy(w2.HistorialWeather, d => d.Time).ToList(),
                CurrentWeather = DateTime.Compare(w1.CurrentWeather.Time, w2.CurrentWeather.Time) >= 0
                                ? w1.CurrentWeather
                                : w2.CurrentWeather
            };
        }
    }
}