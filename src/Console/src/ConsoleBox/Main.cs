﻿#if (basic)
using Microsoft.Extensions.Logging;

internal class Main
{
    private readonly ILogger<Main> _logger;

    public Main(ILogger<Main> logger)
    {
        _logger = logger;
    }

#if (async)
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hello, World!");
        await Task.Delay(1000, cancellationToken); 
    }
#else
    public void Run()
    {
        _logger.LogInformation("Hello, World!");
    }
#endif
}
#else
#if (settings)
using Microsoft.Extensions.Configuration;
#endif
using Microsoft.Extensions.Logging;

internal class Main
{
    private readonly ILogger<Main> _logger;
#if (settings)
    private readonly IConfiguration _config;
#endif
#if (settings)
    public Main(ILogger<Main> logger, IConfiguration config)
#else
    public Main(ILogger<Main> logger)
#endif
    {
        _logger = logger;
#if (settings)
        _config = config;
#endif
    }
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
        _logger.LogInformation(_config.Bind<Settings>("ConsoleBox").Message, dir.Name, dir.GetFiles().Length);
#else
        _logger.LogInformation("Hello World!");
#endif
#if (async)
        await Task.Delay(1000, cancellationToken); 
#endif
    }
}
#endif