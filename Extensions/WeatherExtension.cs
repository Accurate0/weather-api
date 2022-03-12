using WeatherApi.Model;

namespace WeatherApi.Extensions
{
    public static class WeatherExtension
    {
        public static Weather Merge(this Weather w1, Weather w2)
        {
            if (!w1.Name.Equals(w2.Name))
            {
                throw new ArgumentException();
            }

            return new Weather
            {
                Name = w1.Name,
                Data = w1.Data.UnionBy(w2.Data, d => d.Time).ToList(),
                CurrentWeather = DateTime.Compare(w1.CurrentWeather.Time, w2.CurrentWeather.Time) >= 0
                                ? w1.CurrentWeather
                                : w2.CurrentWeather
            };
        }
    }
}
