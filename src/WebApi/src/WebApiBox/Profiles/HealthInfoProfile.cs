using AutoMapper;

namespace WebApiBox.Profiles
{
    public class HealthProfile : Profile
    {
        public HealthProfile()
        {
            CreateMap<Services.HealthDetails, Models.HealthDetailsDto>();
            CreateMap<Services.HealthInfo, Models.HealthInfoDto>();
        }
    }
}
