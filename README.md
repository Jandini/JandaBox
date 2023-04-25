# JandaBox

[![.NET](https://github.com/Jandini/JandaBox/actions/workflows/build.yml/badge.svg)](https://github.com/Jandini/JandaBox/actions/workflows/build.yml)
[![NuGet](https://github.com/Jandini/JandaBox/actions/workflows/nuget.yml/badge.svg)](https://github.com/Jandini/JandaBox/actions/workflows/nuget.yml)

Out of the box .NET6 and .NET7 templates

- Console App with dependency injection, logging, configuration, [GitVersion](https://gitversion.net/docs/) and [CommandLineParser](https://github.com/commandlineparser/commandline).
- ASP.NET Core Web API with [Serilog](https://serilog.net/), [AutoMapper](https://automapper.org/) and [GitVersion](https://gitversion.net/docs/)
- NuGet ready Class Library with with GitHub Actions and [GitVersion](https://gitversion.net/docs/).

```
Template Name                  Short Name  Language  Tags
-----------------------------  ----------  --------  ----------------
JandaBox ASP.NET Core Web API  webapibox   [C#]      JandaBox/WebApi
JandaBox Console App           consolebox  [C#]      JandaBox/Console
JandaBox NuGet Class Library   nugetbox    [C#]      JandaBox/NuGet
```

![helloworld](https://user-images.githubusercontent.com/19593367/234417753-44bf29db-064c-4d8a-af26-91afee97e6ca.gif)

## Quick Start

Install template and create console applications or web services. 

### Install

To install JandaBox templates use `dotnet` command.

```bash
dotnet new install JandaBox
```

You are now ready to use the templates. 



### Console App

Create .NET6 console application with dependency injection, Serilog and configuration.

```sh
dotnet new consolebox -n HelloWorld
```

HelloWorld basic console app using command line interface

![jandabox](https://user-images.githubusercontent.com/19593367/234421169-106edabc-6288-4498-b6f0-252e8e96f62f.gif)




### Web API

Create .NET7 web API  from `webapibox` template.

```sh
dotnet new webapibox -n MyWebService
```





### NuGet Class Library

Create NuGet ready class library with GitHub Actions.

```sh
dotnet new nugetbox -n MyNuGet --actions --user Jandini
```

Create simple class library without NuGet configuration.

```sh
dotnet new nugetbox -n MyLibrary --nuget false
```

How to create and push NuGet package to registry ? 

Read more under "JandaBox NuGet Class Library" 



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

* `--single` Publish as single file, self contained, win-x64 console application.

* `--gitVersion` Add semantic versioning with GitVersion. The code created with this parameter can be only build from initialized git repository.  

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




## JandaBox NuGet Class Library

Create NuGet package and push to GitHub/NuGet.org package registry. 

* Create new GitHub repository.
* Create GitHub access tokens and/or API key in NuGet.org and save it as secrets in the new repository.
* Create new Project from the template and push it to GitHub. 

  ```sh
  dotnet new nugetbox -n MyNuGet --actions --user Jandini --gitVersion
  cd MyNuGet
  git init -b main
  git add .
  git commit -m "First commit"
  git remote add origin https://github.com/Jandini/MyNuGet.git
  git push -u origin main
  ```

At this point GitHub actions will create NuGet package and push it to GitHub private package registry. The package is created and pushed only for main branch. 

* A new tag will trigger NuGet pipeline which will create and push release package to GitHub or NuGet.org.  Use `--tagNugetOrg` parameter to push release package to nuget.org. Nuget.org API key is required in repos' secrets. 

Read more on how to use GitHub NuGet registry in GitHub actions here https://docs.github.com/en/actions/security-guides/automatic-token-authentication

### Step by step


* Create new GitHub repository
  - Go to https://github.com/new and create public or private repository.
  - Do not add any files at this stage.
  - Your repository URL should look like https://github.com/Jandini/MyNuGet.git where `Jandini` is going to be your user name.


*Following step can be skipped if you have safely stored your PAT which can be re-used.*

* Create Personal Access Token (PAT) with `write:packages` permissions. This will allow to push NuGet packages into GitHub package registry. 

  * Go to https://github.com/settings/tokens and from drop down "Generate new token" select "Generate new token (classic)" or go directly to https://github.com/settings/tokens/new

  * Set "Note" to anything you want. Usually it should reflect purpose of the token. 

  * Select checkbox "write:packages" to allow upload packages to GitHub Package Registry.

  * Click "Generate token"

  * Copy the new token to clipboard. You need to add it to repository secretes. Note: you will see the token only once.

    ​

*Following steps explains how create API key in https://www.nuget.org/. If you are planning to push your release packages to nuget.org then you must use `--tagNugetOrg` parameter when creating project from template and perform following steps.*

* Go to https://www.nuget.org/ and login to your account. 

* Under "API Keys" create new token or go directly to https://www.nuget.org/account/apikeys

  * Specify "Key Name"

  * You can specify pattern name for your NuGet package or use `*` to allow push for any package

  * Click "Create"

  * Copy the Key into clipboard

    ​

* Add NuGet API Key to Secrets in GitHub repository.

  * Go To "Actions secrets and variables" 

    * Open repository Setting 
    * In the left hand side, under Security select Secrets and variables | Actions

  * Click "New repository secret"

  * Paste your NuGet API Key into "Secret*" field 

  * Secret's "Name" must be set to  `NUGET_ORG_API_KEY`. This name is used in `nuget.yml` file.

  * Click "Add secret"

    ​

* Create new project from `nugetbox` template.

  * Give it a name `-n MyNuGet`
  * Add GitHub Actions `--actions`
  * Provide your GitHub user name `--user Jandini`
  * Add versioning with GitVersoin `--gitVersion`

  ```sh
  dotnet new nugetbox -n MyNuGet --actions --user Jandini --gitVersion
  ```

  ​

* Initialize local git repository and push it to GitHub

  ```sh
  cd MyNuGet
  git init -b main
  git add .
  git commit -m "First commit"
  git remote add origin https://github.com/Jandini/MyNuGet.git
  git push -u origin main
  ```



That's all. Your NuGet package will be waiting in GitHub registry ! 






##### Template options

```sh
-s, --sourceName <sourceName>  Type: string
                               Default: LibraryBox
-nu, --nuget                   Add nuget package properties to project file.
                               Type: bool
                               Default: true
-ta, --tagNugetOrg             Add GitHub action for pushing tagged package to NuGet.org registry.
                               Type: bool
                               Default: false
-li, --license                 Add MIT LICENSE file to the repository and nuget package.
                               Type: bool
                               Default: true
-au, --authors <authors>       Package authors.
                               Type: string
                               Default: PACKAGE_AUTHORS
-us, --user <user>             GitHub user name.
                               Type: string
                               Default: GITHUB_USER
-ac, --actions                 Add GitHub actions for building and pushing package to the registry.
                               Type: bool
                               Default: false
-g, --gitVersion               Add semantic versioning with GitVesion.
                               Type: bool
                               Default: false
```



- `--nuget`  Add properties to project file required to build and push NuGet package. Default value is `true`. Use `false` to create simple class library.
- `--user`  Specify GitHub user name to update links in project file properties and GitHub action badge links in README.md file. 
- `--actions` Add GitHub Actions pipeline files for building and pushing NuGet packages. Build pipeline creates NuGet package and push it to private GitHub packages only form `main` branch. NuGet pipeline creates NuGet package and push it to NuGet.org package registry.  Default value is `false`.
- `--tagNugetOrg` Add GitHub action file to push tagged NuGet packages to NuGet.org registry. By default tagged packages are pushed into of GitHub package registry. 
- `--gitVersion` Add semantic versioning with GitVersion. The code created with this parameter can be only build from initialized git repository.
- `--license` Add MIT license file to the NuGet package with authors provided in `--authors` parameter.
- `--authors` Add NuGet package authors. The authors will be written to package properties and license file.  




##### Template features

- Repository Layout
  - The `src` and `bin` folders 
  - Default `README.md` file 
  - Default `.gitignore` file
- NuGet configuration
  - Build NuGet package with only `dotnet pack` 
- GitHub Actions
  - Restore packages from private GitHub package registry. 
  - Pack library into NuGet package.
  - Push NuGet package created from main branch into NuGet package registry.
  - Push Tagged package into NuGet.org or GitHub package registry.
- Update links in project properties `RepositoryUrl`  `PackageProjectUrl`  and badge links in `README.md` files. 
- Add files into the NuGet package
  - README.md 
  - MIT LICENSE with given authors
  - Icon file




## Resources

* Box icon was downloaded from [Flaticon](https://www.flaticon.com/free-icon/open-box_869027?term=box&related_id=869027)


* https://learn.microsoft.com/en-us/dotnet/core/tools/custom-templates
* https://github.com/dotnet/templating/wiki
