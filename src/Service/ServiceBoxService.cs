#if (logger)
using Microsoft.Extensions.Logging;

#endif
namespace NAMESPACE_NAME;

#if (primary)
#if (logger)
internal class ServiceBoxService(ILogger<ServiceBoxService> logger) : IServiceBoxService
{

}
#else
internal class ServiceBoxService : IServiceBoxService
{
    
}
#endif
#else
internal class ServiceBoxService : IServiceBoxService
{
#if (logger)
    private readonly ILogger<ServiceBoxService> _logger;

    public ServiceBoxService(ILogger<ServiceBoxService> logger) 
    {
        _logger = logger;
    }
#else

#endif
}
#endif