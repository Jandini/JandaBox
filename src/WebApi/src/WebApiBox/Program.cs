// Created with JandaBox http://github.com/Jandini/JandaBox
using AutoMapper;
using Serilog;
#if (exceptionMiddleware)
using WebApiBox;
#endif
using WebApiBox.Services;
using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Read configuration from environment variables
builder.Configuration.AddEnvironmentVariables();

// Create serilog logger
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger()
    .ForContext<Program>();

#if (appSettings)
// Get application settings
var appSettings = builder.Configuration.Get<AppSettings>()!;
builder.Services.AddSingleton(appSettings);

// Log application name and version
var appName = appSettings.ApplicationName ?? builder.Environment.ApplicationName;
#else
// Log application name and version
var appName = builder.Environment.ApplicationName;
#endif
var appVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
logger.Information($"Starting {appName} {appVersion}");

// Use serilog for web hosting
builder.Host.UseSerilog(logger);

// Add services to the container
builder.Services.AddHealth();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add controllers
builder.Services.AddControllers()
    // Suppress ProblemDetails schema
    .ConfigureApiBehaviorOptions(o => o.SuppressMapClientErrors = true);

// Add http client
builder.Services.AddHttpClient();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Get rid of Dto postfix 
    options.CustomSchemaIds((type) => type.Name.EndsWith("Dto")
        ? type.Name[..^3]
        : type.Name);

    // Update application title on Swagger UI web page for v1
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = appName
    });
});

#if (windowsService)
// Add run as windows service
builder.Services.AddWindowsService();
#endif

var app = builder.Build();

//-:cnd:noEmit
#if (DEBUG)
// Assert mapper configuration 
app.Services.GetRequiredService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();

// Log all environment variables in DEBUG mode only
var variables = Environment.GetEnvironmentVariables();
foreach (var key in variables.Keys.Cast<string>().Order())
    logger.ForContext(typeof(Environment)).Debug($"{key}={variables[key]}");

#endif
//+:cnd:noEmit

// Configure the HTTP request pipeline
if (!app.Environment.IsProduction())
{
    logger.Warning("Adding swagger UI");
    app.UseSwagger();
    // Update html document title in swagger UI
    app.UseSwaggerUI(options => options.DocumentTitle = appName);
}

#if (exceptionMiddleware)
// Unhandled exception middleware
app.UseMiddleware<ExceptionMiddleware>();
#endif

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
