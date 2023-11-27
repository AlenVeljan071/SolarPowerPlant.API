using AutoMapper;
using SolarPowerPlant.API.Requests.Account;
using SolarPowerPlant.API.Requests.PowerPlant;
using SolarPowerPlant.Core.Entities;
using SolarPowerPlant.Core.Enums;
using SolarPowerPlant.Core.Responses.PowerPlant;
using SolarPowerPlant.Core.Responses.TimeSeries;

namespace SolarPowerPlant.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region USER

            CreateMap<SignUserAccountRequest, User>();
            CreateMap<LoginRequest, User>();
            #endregion

            #region SOLAR POVER PLANT

            CreateMap<SolarPowerPlantRequest, Core.Entities.SolarPowerPlant>();
            CreateMap<UpdatePowerPlantRequest, Core.Entities.SolarPowerPlant>();
            CreateMap<Core.Entities.SolarPowerPlant, SolarPowerPlantResponse>();
            CreateMap<Core.Entities.TimeseriesProduction, TimeSeriesResponse>()
                 .ForMember(x => x.TimeSerieType, o => o.MapFrom(x => EnumExtensions.GetValues<TimeSerieType>().First(d => d.Id == (int)x.TimeSerieType))); ;
            #endregion
        }
    }
}
