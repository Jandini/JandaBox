// Created with JandaBox http://github.com/Jandini/JandaBox
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
    try
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddApplicationSettings()
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
#if (async && options)
                    await main.RunAsync(options.Path, cancellationTokenSource.Token);
#elif (async)
                    await main.RunAsync(cancellationTokenSource.Token);
#elif (options)
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
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }    
});
#endif