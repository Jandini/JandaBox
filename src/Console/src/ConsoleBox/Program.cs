// Created with JandaBox http://github.com/Jandini/JandaBox
#if (basic)
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
#if (serilog)
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
#endif

using var provider = new ServiceCollection()
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
   var cancellationTokenSource = new CancellationTokenSource();

    Console.CancelKeyPress += (sender, eventArgs) =>
    {
        provider.GetRequiredService<ILogger<Program>>()
            .LogWarning("Ctrl+C pressed. Shutting down gracefully...");
        
        cancellationTokenSource.Cancel();
        eventArgs.Cancel = true; 
    };

    await provider.GetRequiredService<Main>().Run(cancellationTokenSource.Token);
#else
    provider.GetRequiredService<Main>().Run();
#endif    
}
catch (Exception ex)
{
    provider.GetService<ILogger<Program>>()?
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

    using var provider = new ServiceCollection()
        .AddConfiguration(config)
        .AddLogging(config)
        .AddServices()
        .BuildServiceProvider();

    provider.LogVersion<Program>();

    try
    {
#if (async)
        var token = provider.GetCancellationToken();
#endif
        var main = provider.GetRequiredService<Main>();

        switch (parameters)
        {
            case Options.Run options:
#if (async && settings)
                await main.Run(options.Path, token);
#elif (async)
                await main.Run(token);
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
        provider.GetService<ILogger<Program>>()?
            .LogCritical(ex, "Program failed.");
    }
});
#endif