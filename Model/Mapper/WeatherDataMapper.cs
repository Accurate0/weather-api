using AutoMapper;
using WeatherApi.Utils;

namespace WeatherApi.Model.Mapper
{
    public class WeatherDataMapper : Profile
    {
        public WeatherDataMapper()
        {
            CreateMap<Datum, Weather.WeatherData>()
                .ForMember(d => d.AirTemp, s => s.MapFrom(x => x.AirTemp))
                .ForMember(d => d.Pressure, s => s.MapFrom(x => x.Press))
                .ForMember(d => d.Time, s => s.MapFrom(x => DateTimeUtils.ParseDate(x.LocalDateTimeFull)));
        }
    }
}
