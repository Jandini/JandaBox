using AutoMapper;

namespace WebApiBox.Profiles
{
    public class HealthProfile : Profile
    {
        public HealthProfile()
        {
            CreateMap<Services.WebApiHealthInfo, Models.WebApiHealthInfoDto>();
            CreateMap<Services.HealthInfo, Models.HealthInfoDto>();
        }
    }
}
