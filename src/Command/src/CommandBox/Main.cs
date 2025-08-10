#if (basic)
using Microsoft.Extensions.Logging;

internal class Main(ILogger<Main> logger)
{

#if (async)
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Hello, World!");
        await Task.CompletedTask;
    }
#else
    public void Run()
    {
        logger.LogInformation("Hello, World!");
    }
#endif
}
#else
#if (settings)
using Microsoft.Extensions.Options;
#endif
using Microsoft.Extensions.Logging;
#if (nswag)
using CommandBox;
#endif

#if (settings)
internal class Main(ILogger<Main> logger, IOptions<Settings> settings)
#elif (settings && nswag)
internal class Main(ILogger<Main> logger, IOptions<Settings> settings, IAPI_CLIENT client)
#elif (nswag)
internal class Main(ILogger<Main> logger, IAPI_CLIENT client) 
#else
internal class Main(ILogger<Main> logger) 
#endif
{
#if (async && settings)
    public async Task RunAsync(string path, CancellationToken cancellationToken = default)
#elif (async)
    public async Task RunAsync(CancellationToken cancellationToken = default)
#elif (settings)
    public void Run(string path)
#else
    public void Run()
#endif
    {
#if (settings)
        var dir = new DirectoryInfo(path);
        logger.LogInformation(settings.Value.Message, dir.Name, dir.GetFiles().Length);
#else
        logger.LogInformation("Hello World!");
#endif
#if (async)
        await Task.CompletedTask;
#endif
    }
}
#endif