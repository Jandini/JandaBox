using System.Reflection;

namespace WebApiBox.Services
{
    public class HealthService : IHealthService
    {
#if (appSettings)
        private readonly AppSettings _settings;

        public HealthService(AppSettings settings)
        {
            _settings = settings;
        }

#endif
        public async Task<HealthInfo> GetHealthInfoAsync()
        {
#if (appSettings)
            var info = new HealthInfo
            {
                Service = new WebApiHealthInfo()
                {
                    Name = _settings.ApplicationName ?? Assembly.GetExecutingAssembly().GetName().Name,
                    Version = _settings.ApplicationVersion ?? Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                }
            };
#else
            var info = new HealthInfo
            {
                Service = new WebApiHealthInfo()
                {
                    Name = Assembly.GetExecutingAssembly().GetName().Name,
                    Version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                }
            };
#endif

            return await Task.FromResult(info);
        }
    }
}
