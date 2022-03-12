using AutoMapper;
using System.Globalization;

namespace WeatherApi.Model.Mapper
{
    public class CurrentWeatherMapper : Profile
    {
        public CurrentWeatherMapper()
        {
            CreateMap<Weather, CurrentWeather>()
                .ForMember(d => d.Temperature, s => s.MapFrom(x => x.Data.First().AirTemp))
                .ForMember(d => d.Pressure, s => s.MapFrom(x => x.Data.First().Pressure))
                .ForMember(d => d.Time, s => s.MapFrom(x => x.Data.First().Time));
        }
    }
}
