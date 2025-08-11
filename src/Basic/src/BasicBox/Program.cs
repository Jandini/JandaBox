// Created with JandaBox http://github.com/Jandini/JandaBox
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
#if (serilog)
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
#endif

try 
{
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
            if (!cancellationTokenSource.IsCancellationRequested) {
                serviceProvider.GetRequiredService<ILogger<Program>>()
                    .LogWarning("User break (Ctrl+C) detected. Shutting down gracefully...");
            
                cancellationTokenSource.Cancel();
                eventArgs.Cancel = true; 
            }
        };

        await serviceProvider.GetRequiredService<Main>().RunAsync(cancellationTokenSource.Token);
    #else
        serviceProvider.GetRequiredService<Main>().Run();
    #endif
        return 0;
    }
    catch (Exception ex)
    {
        serviceProvider.GetService<ILogger<Program>>()?
            .LogCritical(ex, "Program failed.");

        return -1;
    }
}
catch (Exception ex) 
{
    Console.WriteLine(ex.Message);
    return -1;
}
