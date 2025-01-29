using System.Reflection;

namespace WebApiBox.Services;

public class HealthService : IHealthService
{
#if (nameOverride)
    private readonly IConfiguration _configuration;

    public HealthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

#endif
    private static DateTime _startedAt = DateTime.UtcNow;

    public async Task<HealthInfo> GetHealthInfoAsync()
    {
#if (nameOverride)
        var info = new HealthInfo
        {
            Service = new ServiceInfo()
            {
                Name = _configuration.GetValue("APPLICATION_NAME", Assembly.GetExecutingAssembly().GetName().Name),
                Version = _configuration.GetValue("APPLICATION_VERSION", Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion),
                StartedOn = _startedAt,
                UpTime = new TimeSpan(DateTime.UtcNow.Ticks - _startedAt.Ticks)
            }
        };
#else
        var info = new HealthInfo
        {
            Service = new ServiceInfo()
            {
                Name = Assembly.GetExecutingAssembly().GetName().Name,
                Version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion,
                StartedOn = _startedAt,
                UpTime = new TimeSpan(DateTime.UtcNow.Ticks - _startedAt.Ticks)
            }
        };
#endif

        return await Task.FromResult(info);
    }
}
