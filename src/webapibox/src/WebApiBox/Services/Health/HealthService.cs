using System.Reflection;

namespace WebApiBox.Services.Health;


#if (nameOverride)
public class HealthService(IConfiguration configuration) : IHealthService
#else
public class HealthService : IHealthService
#endif
{
    private static DateTime _startedAt = DateTime.UtcNow;

    public async Task<HealthInfo> GetHealthInfoAsync()
    {
#if (nameOverride)
        var info = new HealthInfo
        {
            Service = new ServiceInfo()
            {
                Name = configuration.GetValue("APPLICATION_NAME", Assembly.GetExecutingAssembly().GetName().Name),
                Version = configuration.GetValue("APPLICATION_VERSION", Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion),
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
