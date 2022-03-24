using AutoMapper;

using LibWeather.Utils;

namespace LibWeather.Model.Mappers
{
    public class WeatherMapper : Profile
    {
        public WeatherMapper()
        {
            CreateMap<WeatherStationData, WeatherData>()
                .ForMember(d => d.Name, s => s.MapFrom(x => LocationUtil.GetLocationFromWmoId(x.Observations.Data.First().Wmo)))
                .ForMember(d => d.HistorialWeather, s => s.MapFrom(x => x.Observations.Data))
                .ForMember(d => d.CurrentWeather, s => s.MapFrom(x => x.Observations.Data.Find(d => d.SortOrder == 0)));
        }
    }
}
