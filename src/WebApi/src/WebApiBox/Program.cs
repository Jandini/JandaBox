// Created with JandaBox http://github.com/Jandini/JandaBox
using AutoMapper;
using Serilog;
#if (exceptionMiddleware || elasticLog)
using WebApiBox;
#endif
using WebApiBox.Services.Health;
using System.Reflection;
using Microsoft.OpenApi.Models;
#if (elasticLog)
using Serilog.Sinks.Elasticsearch;
using System.Text.RegularExpressions;
#endif

// Create web application builder
var builder = WebApplication.CreateBuilder(args);

// Read configuration from environment variables
builder.Configuration.AddEnvironmentVariables();

#if (nameOverride)
// Log application name and version
var appName = builder.Configuration.GetValue("APPLICATION_NAME", builder.Environment.ApplicationName);
var appVersion = builder.Configuration.GetValue("APPLICATION_VERSION", Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion);
#else
// Log application name and version
var appName = builder.Environment.ApplicationName;
var appVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
#endif

#if (elasticLog)
// Create elasticserach logging options
var elasticOptions = new ElasticsearchSinkOptions(builder.Configuration.GetValue<Uri>("ELASTICSEARCH_URI"))
{
    // Ensure index name meet the following criteria https://www.elastic.co/guide/en/elasticsearch/reference/current/indices-create-index.html
    IndexFormat = Regex.Replace($"{appName}-logs-{builder.Environment.EnvironmentName}-{DateTime.UtcNow:yyyy-MM}".ToLower(), "[\\\\/\\*\\?\"<>\\|#., ]", "-"),
    AutoRegisterTemplate = true,
    // Set environemnt variable ELASTICSEARCH_DEBUG=true do debug elasticsearch logging
    ModifyConnectionSettings = !builder.Configuration.GetValue("ELASTICSEARCH_DEBUG", false) ? null : config => config.OnRequestCompleted(d => Console.WriteLine(d.DebugInformation))
};

// Elasticsearch index name must not be longer than 255 characters
if (elasticOptions.IndexFormat.Length > 255)
    throw new Exception("Elasticsearch index name exceeds 255 characters.");
#endif

// Create serilog logger
var logger = new LoggerConfiguration()
#if (elasticLog)
    .WriteTo.Elasticsearch(elasticOptions)
    .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
    .Enrich.WithProperty("ApplicationName", appName!)
    .Enrich.WithProperty("ApplicationVersion", appVersion!)
#endif
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger()
    .ForContext<Program>();

logger.Information($"Starting {appName} {appVersion}");
#if (elasticLog)
logger.Debug($"Logging [{string.Join(",", elasticOptions.ConnectionPool.Nodes.Select(a => a.Uri))}] {elasticOptions.IndexFormat}");

if (!elasticOptions.ConnectionPool.Nodes.Any())
    logger.Warning("Elasticsearch Uri is not configured");
#endif

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
        Title = appName,
        Version = "1.0"
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
logger.ForContext(typeof(Environment)).Debug(string.Join(Environment.NewLine, variables.Keys.Cast<string>().Order().Select(key => $"{key}={variables[key]}")));
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
