using Microsoft.Extensions.DependencyInjection;

namespace NAMESPACE_NAME;

public static class ServiceBoxServiceExtensions
{
    public static IServiceCollection AddServiceBoxService(this IServiceCollection services)
    {
        return services.AddTransient<IServiceBoxService, ServiceBoxService>();
    }
}