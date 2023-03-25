using System.Reflection;

namespace WebApiBox.Services
{
    public class HealthService : IHealthService
    {
        public async Task<HealthInfo> GetHealthInfoAsync()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            var info = new HealthInfo
            {
                Service = new WebApiHealthInfo()
                {
                    Name = assembly.GetName().Name,
                    Version = version,
                }
            };

            return await Task.FromResult(info);
        }
    }
}
