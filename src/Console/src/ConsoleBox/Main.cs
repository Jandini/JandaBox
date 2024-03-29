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
    public async Task Run()
    {
        _logger.LogInformation("Hello, World!");
        await Task.CompletedTask; 
    }
#else
    public void Run()
    {
        _logger.LogInformation("Hello, World!");
    }
#endif
}
#else
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

internal class Main
{
    private readonly ILogger<Main> _logger;
    private readonly IConfiguration _config;

    public Main(ILogger<Main> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }
#if (async)
    public async Task Run(string path)
#else
    public void Run(string path)
#endif
    {
        var dir = new DirectoryInfo(path);
        _logger.LogInformation(_config.Bind<Settings>("ConsoleBox").Message, dir.Name, dir.GetFiles().Length);
#if (async)
        await Task.CompletedTask;
#endif
    }
}
#endif