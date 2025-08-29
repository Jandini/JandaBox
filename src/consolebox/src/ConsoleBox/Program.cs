// Created with JandaBox http://github.com/Jandini/JandaBox
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

try
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddApplicationSettings()
        .AddEnvironmentVariables()
        .Build();

    using var serviceProvider = new ServiceCollection()
        .AddConfiguration(config)
        .AddLogging(config)
        .AddServices()
        .BuildServiceProvider();

    serviceProvider.LogVersion<Program>();

    try
    {
        using var cancellationTokenSource = serviceProvider.GetCancellationTokenSource();
        var main = serviceProvider.GetRequiredService<Main>();

        await main.RunAsync(cancellationTokenSource.Token);
        return 0;
    }
    catch (Exception ex)
    {
        var logger = serviceProvider.GetService<ILogger<Program>>();
        logger.LogCritical(ex, "Program failed.");

        return -1;
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);

    return -1;
}    

