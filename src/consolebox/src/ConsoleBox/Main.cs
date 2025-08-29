using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

internal class Main(ILogger<Main> logger, IOptions<ProgramOptions> options)
{
    private readonly ProgramOptions _options = options.Value;

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {      
        logger.LogInformation("{0}", _options.HelloWorld);
        await Task.Delay(0, cancellationToken);
    }
}
