using AutoMapper;
using System.Globalization;

namespace WeatherApi.Model.Mapper
{
    public class WeatherDataMapper : Profile
    {
        private DateTime ParseDate(string datetime)
        {
            DateTime output;

            DateTime.TryParseExact(datetime, "yyyyMMddHHmmss", null, DateTimeStyles.None, out output);

            return output;
        }
        public WeatherDataMapper()
        {
            CreateMap<Datum, Weather.WeatherData>()
                .ForMember(d => d.AirTemp, s => s.MapFrom(x => x.AirTemp))
                .ForMember(d => d.Pressure, s => s.MapFrom(x => x.Press))
                .ForMember(d => d.Time, s => s.MapFrom(x => ParseDate(x.LocalDateTimeFull)));
        }
    }
}
