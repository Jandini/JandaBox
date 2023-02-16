using AutoMapper;

namespace WebApiBox.Profiles
{
    public class HealthProfile : Profile
    {
        public HealthProfile()
        {
            CreateMap<Services.HealthInfo, Models.HealthInfoDto>();
        }
    }
}
