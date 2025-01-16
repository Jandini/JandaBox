// Created with JandaBox 0.0.00.0.00.0.0 [GitVersion] http://github.com/Jandini/JandaBox
#if (basic)
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
#if (serilog)
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
#endif

using var serviceProvider = new ServiceCollection()
#if (serilog)    
    .AddLogging(builder => builder.AddSerilog(new LoggerConfiguration()
        .Enrich.WithMachineName()
        .WriteTo.Console(
            theme: AnsiConsoleTheme.Code,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] [{MachineName}] [{SourceContext}] {Message}{NewLine}{Exception}")
        .CreateLogger()))
#else
    .AddLogging(builder => builder.AddConsole())
#endif
    .AddTransient<Main>()
    .BuildServiceProvider();

try
{
#if (async)
    using var cancellationTokenSource = new CancellationTokenSource();

    Console.CancelKeyPress += (sender, eventArgs) =>
    {
        serviceProvider.GetRequiredService<ILogger<Program>>()
            .LogWarning("User break (Ctrl+C) detected. Shutting down gracefully...");
        
        cancellationTokenSource.Cancel();
        eventArgs.Cancel = true; 
    };

    await serviceProvider.GetRequiredService<Main>().RunAsync(cancellationTokenSource.Token);
#else
    serviceProvider.GetRequiredService<Main>().Run();
#endif    
}
catch (Exception ex)
{
    serviceProvider.GetService<ILogger<Program>>()?
        .LogCritical(ex, "Program failed.");
}
#else
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CommandLine;

#if (async)
await Parser.Default.ParseArguments<Options.Run>(args).WithParsedAsync(async (parameters) =>
#else
Parser.Default.ParseArguments<Options.Run>(args).WithParsed((parameters) =>
#endif    
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddEmbeddedJsonFile("appsettings.json")
        .Build();

    using var serviceProvider = new ServiceCollection()
        .AddConfiguration(config)
        .AddLogging(config)
        .AddServices()
        .BuildServiceProvider();

    serviceProvider.LogVersion<Program>();

    try
    {
#if (async)
        using var cancellationTokenSource = serviceProvider.GetCancellationTokenSource();
#endif
        var main = serviceProvider.GetRequiredService<Main>();

        switch (parameters)
        {
            case Options.Run options:
#if (async && settings)
                await main.RunAsync(options.Path, cancellationTokenSource.Token);
#elif (async)
                await main.RunAsync(cancellationTokenSource.Token);
#elif (settings)
                main.Run(options.Path);
#else
                main.Run();
#endif
                break;
        };
    }
    catch (Exception ex)
    {
        serviceProvider.GetService<ILogger<Program>>()?
            .LogCritical(ex, "Program failed.");
    }
});
#endif