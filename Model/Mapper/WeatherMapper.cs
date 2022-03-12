using AutoMapper;
using WeatherApi.Utils;

namespace WeatherApi.Model.Mapper
{
    public class WeatherMapper : Profile
    {
        public WeatherMapper()
        {
            CreateMap<WeatherStationData, Weather>()
                .ForMember(d => d.Name, s => s.MapFrom(x => LocationUtil.GetLocationFromString(x.Observations.Header.First().Name)))
                .ForMember(d => d.Data, s => s.MapFrom(x => x.Observations.Data))
                .ForMember(d => d.CurrentWeather, s => s.MapFrom(x => x.Observations.Data.Find(d => d.SortOrder == 0)));
        }
    }
}
