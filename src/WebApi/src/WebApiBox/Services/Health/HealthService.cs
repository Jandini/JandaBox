﻿using System.Reflection;

namespace WebApiBox.Services;

public class HealthService : IHealthService
{
#if (appName)
    private readonly IConfiguration _configuration;

    public HealthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

#endif
    public async Task<HealthInfo> GetHealthInfoAsync()
    {
#if (appName)
        var info = new HealthInfo
        {
            Service = new ServiceInfo()
            {
                Name = _configuration.GetValue("APPLICATION_NAME", Assembly.GetExecutingAssembly().GetName().Name),
                Version = _configuration.GetValue("APPLICATION_VERSION", Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion)
            }
        };
#else
        var info = new HealthInfo
        {
            Service = new ServiceInfo()
            {
                Name = Assembly.GetExecutingAssembly().GetName().Name,
                Version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
            }
        };
#endif

        return await Task.FromResult(info);
    }
}
