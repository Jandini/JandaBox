using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
#if (nswag)
using ConsoleBox;
#endif

internal class Main(ILogger<Main> logger, IOptions<SettingsOptions> settings)
{
    public async Task RunAsync(string path, CancellationToken cancellationToken = default)
    {
        var dir = new DirectoryInfo(path);
        logger.LogInformation(settings.Value.MessageFormat, dir.Name, dir.GetFiles().Length);
        await Task.CompletedTask;
    }
}
