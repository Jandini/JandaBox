using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.Reflection;
#if (serilog)
using Serilog;
#endif

internal static class Extensions
{
    internal static void LogVersion<T>(this IServiceProvider provider) => provider
        .GetRequiredService<ILogger<T>>()
        .LogInformation($"ConsoleBox {Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion}");

    internal static CancellationTokenSource GetCancellationTokenSource(this IServiceProvider provider)
    {
        var cancellationTokenSource = new CancellationTokenSource();

        Console.CancelKeyPress += (sender, eventArgs) =>
        {
            if (!cancellationTokenSource.IsCancellationRequested)
            {
                provider.GetRequiredService<ILogger<Program>>()
                    .LogWarning("User break (Ctrl+C) detected. Shutting down gracefully...");

                cancellationTokenSource.Cancel();
                eventArgs.Cancel = true;
            }
        };

        return cancellationTokenSource;
    }

    internal static IConfigurationBuilder AddEmbeddedJsonFile(this IConfigurationBuilder builder, string name)
    {
        return builder
            .AddJsonStream(new EmbeddedFileProvider(Assembly.GetExecutingAssembly()).GetFileInfo(name).CreateReadStream())
            .AddJsonFile(name, true);
    }

    internal static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
#if (settings)
        return services.AddSingleton(configuration)
            .Configure<Settings>(settings => configuration.GetRequiredSection("ConsoleBox").Bind(settings));
#else
        return services.AddSingleton(configuration);
#endif
    }

    internal static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddLogging(builder => builder
#if (serilog)
            .AddSerilog(new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger(), dispose: true));
#else
            .AddConsole());
#endif
    }

    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            // Add services here
            .AddTransient<Main>();
    }
}
