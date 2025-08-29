using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Serilog;
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

    internal static IConfigurationBuilder AddApplicationSettings(this IConfigurationBuilder builder)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        return builder
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.json", optional: true);
    }

    internal static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<ProgramOptions>(configuration.GetSection("ConsoleBox"))
            .AddSingleton(configuration);            
    }

    internal static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddLogging(builder => builder
            .AddSerilog(new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger(), dispose: true));
    }

    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            // Add services here
            .AddTransient<Main>();
    }
}
