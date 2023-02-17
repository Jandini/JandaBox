# JandaBox

[![.NET](https://github.com/Jandini/JandaBox/actions/workflows/build.yml/badge.svg)](https://github.com/Jandini/JandaBox/actions/workflows/build.yml)
[![NuGet](https://github.com/Jandini/JandaBox/actions/workflows/nuget.yml/badge.svg)](https://github.com/Jandini/JandaBox/actions/workflows/nuget.yml)

Out of the box .NET6/7 templates

- Console App template with dependency injection, logging and configuration.
- ASP.NET Core Web API with Serilog, AutoMapper and simple health endpoint. 

```
Template Name                  Short Name  Language  Tags
-----------------------------  ----------  --------  ----------------
JandaBox ASP.NET Core Web API  webapibox   [C#]      JandaBox/WebApi
JandaBox Console App           consolebox  [C#]      JandaBox/Console
```



## Install

To install JandaBox templates use `dotnet` command. It will automatically download NuGet package from https://www.nuget.org/packages/JandaBox

```bash
dotnet new install JandaBox
```

or in earlier versions 
```bash
dotnet new -i JandaBox
```



## Quick Start

Create default .NET6 console application from `consolebox` template.

```sh
dotnet new consolebox -n MyApp
```

The name parameter `-n` is optional.  

```sh
dotnet new consolebox
```









## JandaBox Console App

ConsoleBox .NET template provides startup solution for console application with dependency injection, logging, and configuration. Default logger is [Serilog](https://serilog.net). Use `--serilog false` parameter to switch to Microsoft console logger.



###### Options

* `--basic`  Create basic console application with minimal amount startup code. Default value is `false`.

* `--serilog`  Use Serilog. Default value is `true`. 

* `--async` Create asynchronous code.  Default value is `false`.

* `--git` Add versioning with GitVersion. The code created with `--git` parameter can be only build from initialized git repository.  

  ```sh
  dotnet new consolebox -n MyApp --git
  cd MyApp
  git init -b main
  git add .
  git commit -m "First commit"
  dotnet build src
  ```

  ​

### Features

- Repository Layout
  - The `src` and `bin` folders 
  - Default `README.md` file 
  - Default `.gitignore` file
  - Default `launchSettings.json` file​
- Dependency Injection
  - Main service with logging
  - Dispose service provider 
- Logging
  - `Serilog`  or `Microsoft` log providers  
  - Serilog environment enrichers
  - Unhandled exceptions logging
- Configuration
  - Embedded `appsettings.json` file
  - Override embedded `appsettings.json` with a file
  - Configuration binding
- Semantic Versioning
  - GitVersion.MsBuild package
  - Configuration `GitVersion.yml` file
- Command line parser
  - Verbs and options parser
- Asynchronous code




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

...








## JandaBox Actions 

Provides GitHub actions templates. 

````
dotnet new actionbox --build
````

###### Options

- `--build`  
- `--nuget`  
- `--docker`






## Useful Extensions



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

Box icon was downloaded from [Flaticon](https://www.flaticon.com/free-icon/open-box_869027?term=box&related_id=869027).


