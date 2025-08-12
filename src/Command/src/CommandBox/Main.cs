#if (options)
using Microsoft.Extensions.Options;
#endif
using Microsoft.Extensions.Logging;
#if (nswag)
using CommandBox;
#endif

#if (options)
internal class Main(ILogger<Main> logger, IOptions<SettingsOptions> settings)
#elif (options && nswag)
internal class Main(ILogger<Main> logger, IOptions<SettingsOptions> settings, IAPI_CLIENT client)
#elif (nswag)
internal class Main(ILogger<Main> logger, IAPI_CLIENT client) 
#else
internal class Main(ILogger<Main> logger) 
#endif
{
#if (async && options)
    public async Task RunAsync(string path, CancellationToken cancellationToken = default)
#elif (async)
    public async Task RunAsync(CancellationToken cancellationToken = default)
#elif (options)
    public void Run(string path)
#else
    public void Run()
#endif
    {
#if (options)
        var dir = new DirectoryInfo(path);
        logger.LogInformation(settings.Value.MessageFormat, dir.Name, dir.GetFiles().Length);
#else
        logger.LogInformation("Hello World!");
#endif
#if (async)
        await Task.CompletedTask;
#endif
    }
}
