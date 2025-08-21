using Microsoft.Extensions.Logging;

internal class Main(ILogger<Main> logger)
{

#if (async)
    public async Task<int> RunAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Hello, World!");
        await Task.CompletedTask;
        return 0;
    }
#else
    public int Run()
    {
        logger.LogInformation("Hello, World!");
        return 0;
    }
#endif
}
