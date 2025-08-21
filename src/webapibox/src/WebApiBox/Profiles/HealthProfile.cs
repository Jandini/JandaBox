using AutoMapper;

namespace WebApiBox.Profiles;

public class HealthProfile : Profile
{
    public HealthProfile()
    {
        CreateMap<Services.Health.ServiceInfo, Models.ServiceInfoDto>();
        CreateMap<Services.Health.HealthInfo, Models.HealthInfoDto>();
    }
}
