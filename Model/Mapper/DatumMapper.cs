using AutoMapper;
using WeatherApi.Utils;

namespace WeatherApi.Model.Mapper
{
    public class DatumMapper : Profile
    {
        public DatumMapper()
        {
            CreateMap<Datum, CurrentWeather>()
                .ForMember(d => d.Temperature, s => s.MapFrom(x => x.AirTemp))
                .ForMember(d => d.Pressure, s => s.MapFrom(x => x.Press))
                .ForMember(d => d.Time, s => s.MapFrom(x => DateTimeUtils.ParseDate(x.LocalDateTimeFull)));
        }
    }
}
