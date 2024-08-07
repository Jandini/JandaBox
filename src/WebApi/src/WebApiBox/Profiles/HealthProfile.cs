﻿using AutoMapper;

namespace WebApiBox.Profiles;

public class HealthProfile : Profile
{
    public HealthProfile()
    {
        CreateMap<Services.ServiceInfo, Models.ServiceInfoDto>();
        CreateMap<Services.HealthInfo, Models.HealthInfoDto>();
    }
}
