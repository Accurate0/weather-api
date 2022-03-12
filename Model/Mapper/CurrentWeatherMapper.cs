using AutoMapper;
using System.Globalization;

namespace WeatherApi.Model.Mapper
{
    public class CurrentWeatherMapper : Profile
    {
        public CurrentWeatherMapper()
        {
            CreateMap<Weather, CurrentWeather>()
                .ForMember(d => d.Temperature, s => s.MapFrom(x => x.CurrentWeather.Temperature))
                .ForMember(d => d.Pressure, s => s.MapFrom(x => x.CurrentWeather.Pressure))
                .ForMember(d => d.Time, s => s.MapFrom(x => x.CurrentWeather.Time));
        }
    }
}
