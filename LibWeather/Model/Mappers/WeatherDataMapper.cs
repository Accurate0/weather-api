using AutoMapper;
using LibWeather.Utils;

namespace LibWeather.Model.Mappers
{
    public class WeatherDataMapper : Profile
    {
        public WeatherDataMapper()
        {
            CreateMap<Datum, WeatherData.Weather>()
                .ForMember(d => d.AirTemperature, s => s.MapFrom(x => x.AirTemp))
                .ForMember(d => d.ApparentTemperature, s => s.MapFrom(x => x.ApparentT))
                .ForMember(d => d.Humidity, s => s.MapFrom(x => x.RelHum))
                .ForMember(d => d.DewPoint, s => s.MapFrom(x => x.Dewpt))
                .ForMember(d => d.Pressure, s => s.MapFrom(x => x.Press))
                .ForMember(d => d.Time, s => s.MapFrom(x => DateTimeUtils.ParseDate(x.LocalDateTimeFull)))
                .ForMember(d => d.WindDirection, s => s.MapFrom(x => x.WindDir))
                .ForMember(d => d.WindSpeedKmh, s => s.MapFrom(x => x.WindSpdKmh));
        }
    }
}
