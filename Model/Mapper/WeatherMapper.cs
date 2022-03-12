using AutoMapper;

namespace WeatherApi.Model.Mapper
{
    public class WeatherMapper : Profile
    {
        public WeatherMapper()
        {
            CreateMap<WeatherStationData, Weather>()
                .ForMember(d => d.Name, s => s.MapFrom(x => x.Observations.Header.First().Name))
                .ForMember(d => d.Data, s => s.MapFrom(x => x.Observations.Data))
                .ForMember(d => d.LatestDataTime, s => s.MapFrom(x => x.Observations.Data.First().LocalDateTimeFull));
        }
    }
}
