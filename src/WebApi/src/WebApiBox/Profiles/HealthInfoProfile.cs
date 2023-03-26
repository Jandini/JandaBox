using AutoMapper;

namespace WebApiBox.Profiles
{
    public class HealthProfile : Profile
    {
        public HealthProfile()
        {
            CreateMap<Services.ServiceDetails, Models.ServiceHealthDto>();
            CreateMap<Services.HealthInfo, Models.HealthInfoDto>();
        }
    }
}
