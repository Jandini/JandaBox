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
