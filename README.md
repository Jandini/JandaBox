# JandaBox

[![.NET](https://github.com/Jandini/JandaBox/actions/workflows/build.yml/badge.svg)](https://github.com/Jandini/JandaBox/actions/workflows/build.yml)
[![NuGet](https://github.com/Jandini/JandaBox/actions/workflows/nuget.yml/badge.svg)](https://github.com/Jandini/JandaBox/actions/workflows/nuget.yml)

Out of the box .NET6 and .NET7 templates

- Console App template with dependency injection, logging, configuration, [GitVersion](https://gitversion.net/docs/) and [CommandLineParser](https://github.com/commandlineparser/commandline).
- ASP.NET Core Web API with [Serilog](https://serilog.net/), [AutoMapper](https://automapper.org/). 
- GitHub Actions Workflows provides docker, NuGet and simple build/test workflows.  

```
Template Name                  Short Name  Language  Tags
-----------------------------  ----------  --------  ----------------
JandaBox ASP.NET Core Web API  webapibox   [C#]      JandaBox/WebApi
JandaBox Console App           consolebox  [C#]      JandaBox/Console
```



## Quick Start

Install template and create console applications or web services. 

### Install

To install JandaBox templates use `dotnet` command. It will automatically download NuGet package from https://www.nuget.org/packages/JandaBox

```bash
dotnet new install JandaBox
```

or in earlier versions 
```bash
dotnet new -i JandaBox
```



### Console App

Create default .NET6 console application from `consolebox` template.

```sh
dotnet new consolebox -n MyApp
```

The name parameter `-n` is optional.  

```sh
dotnet new consolebox
```



### Web API

Create .NET7 web API  from `webapibox` template.

```sh
dotnet new webapibox -n MyWebService
```



## JandaBox Console App

ConsoleBox .NET template provides solution for console application with dependency injection, logging, and configuration. Default logger is [Serilog](https://serilog.net). Use `--serilog false` parameter to switch to Microsoft console logger.



##### Template Options

```sh
  -s, --sourceName <sourceName>  Type: string
                                 Default: ConsoleBox
  -b, --basic                    Create basic console application.
                                 Type: bool
                                 Default: false
  -se, --serilog                 Use Serilog logger.
                                 Type: bool
                                 Default: true
  -g, --gitVersion               Provide semantic versioning with GitVesion.
                                 Type: bool
                                 Default: false
  -as, --async                   Run main with async await.
                                 Type: bool
                                 Default: false
  -si, --single                  Publish as single file, self contained, win-x64 console application.
                                 Type: bool
                                 Default: true
```



* `--basic`  Create basic console application with minimal amount startup code. Default value is `false`.

* `--serilog`  Use Serilog. Default value is `true`. 

* `--async` Create asynchronous code.  Default value is `false`.

* `--single` PPublish as single file, self contained, win-x64 console application.

* `--gitVersion` Add semantic versioning with GitVersion. The code created with `--git` parameter can be only build from initialized git repository.  

  ```sh
  dotnet new consolebox -n MyApp --git
  cd MyApp
  git init -b main
  git add .
  git commit -m "First commit"
  dotnet build src
  ```

  

##### Template features 

- Repository Layout
  - The `src` and `bin` folders 
  - Default `README.md` file 
  - Default `.gitignore` file
  - Default `launchSettings.json` file
- Dependency Injection
  - Main service with logging
  - Dispose service provider 
- Logging
  - `Serilog` or `Microsoft` log providers  
  - Serilog environment enrichers
  - Unhandled exceptions logging
- Configuration
  - Embedded `appsettings.json` file
  - Override embedded `appsettings.json` with a file
  - Configuration binding
- Semantic Versioning
  - `GitVersion.MsBuild` package
  - Configuration `GitVersion.yml` file
- Command Line Parser
  - Verbs and options parser
- Asynchronous code
  - Run Main with `async` and `await`
- Release build without debug symbols
  - Conditional project parameters for `Release` configuration to suppress debug symbols 




### Basic console application

Create basic console application with Microsoft console logger

```sh
dotnet new consolebox -n Basic --basic --serilog false	
```

or console application with Serilog.

```sh
dotnet new consolebox -n Basic --simple
```



### Default console application

Create console application with Serilog to console and file. 

```sh
dotnet new consolebox -n MyApp
```

You can create console application with Microsoft console logger only.

```sh
dotnet new consolebox -n MyApp --serilog false
```



### Help

For more information about **ConsoleBox** template run 

```
dotnet new consolebox -h  
```





## JandaBox ASP.NET Core Web API

Create web API service with [Serilog](https://serilog.net/), [AutoMapper](https://automapper.org/) and simple Health endpoint.

```sh
dotnet new webapibox -n MyWebService
```



##### Template options

```sh
  -s, --sourceName <sourceName>  Type: string
                                 Default: WebApiBox
  -g, --gitVersion               Provide semantic versioning with GitVesion.
                                 Type: bool
                                 Default: false
  -op, --openApi                 Add NuGet packages for OpenApi code generator.
                                 Type: bool
                                 Default: false
  -w, --windowsService           Add run as windows service.
                                 Type: bool
                                 Default: false
  -e, --exceptionMiddleware      Add global exception handler middleware.
                                 Type: bool
                                 Default: true
  -ap, --appName                 Add application name and version override option through appsettings or environment variables.
                                 Type: bool
                                 Default: false
  -el, --elasticLog              Add Elasticsearch Serilog sink and configuration.
                                 Type: bool
                                 Default: false
```



##### Template features

- Repository Layout

  - The `src` and `bin` folders 
  - Default `README.md` file 
  - Default `.gitignore` file
  - Default `launchSettings.json` file
- Semantic Versioning
  - Optional parameter `--gitVersion` provides `GitVersion.MsBuild` package and `GitVersion.yml` configuration file.
- Simple Health endpoint
  - Name and version of the service
- AutoMapper

  - DTO profiles and mapping
- Logging

  - `Serilog` for web hosting
  - Serilog environment enrichers like computer name
- Configuration

  - `appsettings.json` file
  - Override settings through environment variables
- Application Name and Version Override
  - Optional parameter `--appOverride` provides application name and version override through `appsettings.json`.

- Run as Windows Service

  - Optional parameter `--windowsService` add windows service start up.
- Open API

  - Optional parameter `--openApi`  add latest packages for OpenApi code generator.  
- Start up

  - Remove "Dto" postfix from DTOs class names for Swagger 
  - Log all environment variables in `DEBUG` build
  - Validate AutoMapper profiles in `DEBUG` build
  - Log web service name and version.
  - Swagger website title is set to assembly name.
- Release build without debug symbols
  - Conditional project parameters for `Release` configuration to suppress debug symbols. 
- Unhandled exception handler through middleware
  - Optional parameter `--exceptionMiddleware` provides unhandled exception middleware. 




## JandaBox Actions 

> Todo

Provides GitHub actions templates. 

````
dotnet new actionbox --build
````

###### Template Options

- `--build`  
- `--nuget`  
- `--docker`






## Useful Extensions

A few useful extensions that not necessarily have to belong to the template. 

#### Add file logger

This extension creates an extra log file in given location. You might need to log all the actions related to processing data in selected folder. 

```c#
internal static void AddFileLogger(this ILoggerFactory factory, FileInfo logFile)
{        
    factory
        .AddSerilog(new LoggerConfiguration()
        .Enrich.WithMachineName()
        .WriteTo.File(
            logFile.FullName, 
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] [{MachineName}] [{SourceContext}] {Message}{NewLine}{Exception}")
        .CreateLogger(), dispose: true);
}
```





## Resources

* Box icon was downloaded from [Flaticon](https://www.flaticon.com/free-icon/open-box_869027?term=box&related_id=869027).


* https://learn.microsoft.com/en-us/dotnet/core/tools/custom-templates
* https://github.com/dotnet/templating/wiki




## TODOs

* Add JandaBox Actions
* Switch between .NET6 and .NET7 for JandaBox Console App
* Add JandaBox Service to provide interface, class and extensions classes for a given service 
* Add github nuget ready JandaBox ClassLib solution