#if (logger)
using Microsoft.Extensions.Logging;

#endif
namespace NAMESPACE_NAME
{
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
}