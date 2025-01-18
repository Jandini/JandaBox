# JandaBox

[![Build](https://github.com/Jandini/JandaBox/actions/workflows/build.yml/badge.svg)](https://github.com/Jandini/JandaBox/actions/workflows/build.yml)
[![NuGet](https://github.com/Jandini/JandaBox/actions/workflows/nuget.yml/badge.svg)](https://github.com/Jandini/JandaBox/actions/workflows/nuget.yml)
[![NuGet Version](http://img.shields.io/nuget/v/JandaBox.svg?style=flat&label=Version)](https://www.nuget.org/packages/JandaBox/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/JandaBox.svg?style=flat&label=Downloads)](https://www.nuget.org/packages/Serilog.Enrichers.ClassName)


Out of the box .NET8 templates

- Console App with dependency injection, logging, configuration, [GitVersion](https://gitversion.net/docs/) and [CommandLineParser](https://github.com/commandlineparser/commandline).
- ASP.NET Core Web API with [Serilog](https://serilog.net/), [AutoMapper](https://automapper.org/) and [GitVersion](https://gitversion.net/docs/)
- NuGet ready Class Library with with GitHub Actions and [GitVersion](https://gitversion.net/docs/).
- Service files: a service interface, an implementation, and an extension class.

```
Template Name                            Short Name  Language  Tags            
---------------------------------------  ----------  --------  ----------------
JandaBox ASP.NET Core Web API            webapibox   [C#]      JandaBox/WebApi 
JandaBox ASP.NET Core Web API Project    webapiprj   [C#]      JandaBox/WebApi 
JandaBox Console App                     consolebox  [C#]      JandaBox/Console
JandaBox Console App Project             consoleprj  [C#]      JandaBox/Console
JandaBox NuGet Class Library             nugetbox    [C#]      JandaBox/NuGet  
JandaBox Service Classes and Extensions  servicebox  [C#]      JandaBox/Service
```


Learn more about templates in the [wiki pages](https://github.com/Jandini/JandaBox/wiki).



## Quick Start

To install JandaBox templates use `dotnet` command.

```bash
dotnet new install JandaBox
```

You are now ready to use the templates from command line or from Visual Studio.

![helloworld](https://user-images.githubusercontent.com/19593367/234417753-44bf29db-064c-4d8a-af26-91afee97e6ca.gif)



To upgarde JandaBox templates to the latest version use following commands: 

```
dotnet new uninstall JandaBox
dotnet new install JandaBox
```


### Console Application

Create .NET8 console application with dependency injection, Serilog and configuration.

```sh
dotnet new consolebox -n HelloWorld
```

HelloWorld basic console app using command line interface

![jandabox](https://user-images.githubusercontent.com/19593367/234421169-106edabc-6288-4498-b6f0-252e8e96f62f.gif)


### Console Application Project

Create .NET8 console application project with dependency injection, Serilog and configuration. 

```sh
dotnet new consoleprj -n HelloWorld
```

This is project only, without solution files etc. 


### Web API

Create .NET8 web API from `webapibox` template.

```sh
dotnet new webapibox -n MyWebService
```

The template provides solution file and the API project.

### Web API Project

Create .NET8 web API project only from `webapiprj` template.

```sh
dotnet new webapiprj -n MyWebService
```

This template provides project folder only.


### Service Files

Add new service interface, implementation and extsions.

```sh
dotnet new servicebox -n DemoService --nameSpace MyApp.Services --logger
```





## Add a new service tutorial

This tutorial demonstrates how to add simple service to the console application created with JandaBox.

Create demo console application.

```sh
C:\Demo>dotnet new consolebox
The template "JandaBox Console App" was created successfully.
```

Go to the source folder and create Services directory.

```
C:\Demo>cd src\Demo
C:\Demo\src\Demo>mkdir Services
C:\Demo\src\Demo>cd Services
C:\Demo\src\Demo\Services
```

Add demo service

```sh
dotnet new servicebox -n DemoService --nameSpace Demo.Services --logger
The template "JandaBox Service Classes and Extensions" was created successfully.
```

The template provides following files: 

###### IDemoService.cs

```c#
namespace Demo.Services
{
    public interface IDemoService
    {

    }
}
```


###### DemoService.cs

```c#
using Microsoft.Extensions.Logging;

namespace Demo.Services
{
    internal class DemoService : IDemoService
    {
        private readonly ILogger<DemoService> _logger;

        public DemoService(ILogger<DemoService> logger) 
        {
            _logger = logger;
        }
    }
}
```

###### DemoServiceExtensions.cs

```c#
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Services
{
    public static class DemoServiceExtensions
    {
        public static IServiceCollection AddDemoService(this IServiceCollection services)
        {
            return services.AddTransient<IDemoService, DemoService>();
        }
    }
}
```

You can do the same from the Visual Studio.

![jandabox](https://github.com/Jandini/JandaBox/assets/19593367/55e1f23a-3574-4156-92df-2ba62f6e61b5)



## Class Library into NuGet tutorial

This tutorial demonstrates how to create a new library, push it to GitHub, and publish it on NuGet.org using the **JandaBox nugetbox** template. Additionally, this tutorial provides explanations on why and how to create PAT tokens and NuGet.org tokens.

Here's what we aim to accomplish:

1. The library will be named **AnyoneDrive** and will be available under the **MIT license**.
2. The package's author is **Matt Janda**.
3. Semantic versioning, as provided by **GitVersion**, will be used for versioning the library.
4. The package will be published on GitHub under the **Jandini** owner.
5. Automatic building and publishing of the package will be handled by **GitHub actions**.
6. The package will be published to a **private GitHub packages registry** for each branch and commit. This approach allows you to create NuGet packages and test them without the need to publish them to the public NuGet.org.
7. The package will be published to **Nuget.org** only after a tag is created.

*Please note that you should replace **Jandini** and **AnyoneDrive** with your GitHub owner and your repository name, respectively.*



Let's get started!

* Create a new EMPTY repository in GitHub. 

  * Head over to [GitHub's repository creation page](https://github.com/new).
  * Create a new repository, making sure **not** to add a `README.md`, `.gitignore`, or license file.
  * Your repository will have a home at `https://github.com/Jandini/AnyoneDrive.git`.

* Open your command line or terminal and run the following command to create a new project:

  ```shell
  dotnet new nugetbox -n AnyoneDrive --nuget --tagNugetOrg --license --authors "Matt Janda" --user Jandini --actions --gitVersion
  ```

  This command will create a new project named "AnyoneDrive". Once created, navigate to the folder and list its contents:

  ```cmd
  cd AnyoneDrive
  dir
  ```

  The directory structure should resemble the following:

  ```cmd
  06/10/2023  22:56    <DIR>          .github
  06/10/2023  22:56    <DIR>          src
  06/10/2023  22:56             6,001 .gitignore
  06/10/2023  22:56               241 GitVersion.yml
  06/10/2023  22:56             6,643 icon.png
  06/10/2023  22:56             1,067 LICENSE
  06/10/2023  22:56               476 README.md
  ```

* Add the project to GitHub repository. 

  Initialize a local Git repository and simultaneously create a new branch:

  ```shell
  git init -b main
  ```

  Add the remote origin to the local repository:

  ```shell
  git remote add origin https://github.com/Jandini/AnyoneDrive.git
  ```

  Stage all the files before creating the first commit:

  ```shell
  git add .
  ```

  Create the initial commit:

  ```c#
  git commit -m "First commit"
  ```

  Push the new branch to the remote GitHub repository, using `-u` to set the upstream branch that doesn't exist yet in the remote repository:

  ```shell
  git push -u origin main
  ```



You've successfully created a new project and made it available on GitHub. To view your repository, visit https://github.com/Jandini/AnyoneDrive 

![image-20231006231234635](https://github.com/Jandini/JandaBox/assets/19593367/a65c23d7-9ff5-45d4-842b-7304fc6eb0c9)


The GitHub Actions have started executing the "**Build**" action, which is currently **failing** as expected. To investigate further, go to GitHub Actions by navigating to https://github.com/Jandini/AnyoneDrive/actions and check the build details to understand why it's failing.

The build is failing at the "Setup" step, and you'll see the following error message:

```shell
Error: The NUGET_AUTH_TOKEN environment variable was not provided. In this step, add the following: 
env:
  NUGET_AUTH_TOKEN: ${{secrets.GITHUB_TOKEN}}
```

The `NUGET_AUTH_TOKEN` is an environment variable used in the context of the NuGet package manager, which is a package manager for .NET development. NuGet is used to manage and distribute libraries, frameworks, and tools for .NET applications.  

This error occurs because the Setup step is attempting to configure `dotnet` to have access (`source-url`) to your private GitHub NuGet registry. You can find this configuration in your GitHub Actions workflow file at https://github.com/Jandini/AnyoneDrive/blob/main/.github/workflows/build.yml#L28:

```yml
- name: Setup
  uses: actions/setup-dotnet@v1
  with:
    dotnet-version: 7.0.x
    source-url: https://nuget.pkg.github.com/${{ github.repository_owner }}/index.json
```

To resolve this issue, you should NOT follow the error's recommendation and use `NUGET_AUTH_TOKEN: ${{secrets.GITHUB_TOKEN}}`. The `GITHUB_TOKEN` has read access only to your private GitHub NuGet registry, which will enable the Setup stage to proceed successfully.

However, the "Push" step will fail due to insufficient permissions with the provided GITHUB_TOKEN.

```yaml
- name: Push
  # Push packages into private github repository 
  if: github.ref == 'refs/heads/main'
  run: dotnet nuget push "../bin/Release/*.nupkg" -k ${{ secrets.PACKAGE_REGISTRY_TOKEN }} -s https://nuget.pkg.github.com/${{ github.repository_owner }}/index.json --skip-duplicate
```

In the workflow file (build.yml), the definition of `NUGET_AUTH_TOKEN` assigns the `PACKAGE_REGISTRY_TOKEN` secret string as follows:

``` yaml
env:
    NUGET_AUTH_TOKEN: ${{ secrets.PACKAGE_REGISTRY_TOKEN }}
```

The build is currently failing because your repository does not have the secret string that contains the Personal Access Token (PAT) required for successful execution.



Now, let's create a Personal Access Token (PAT) and add it to the secrets of your GitHub repository:

1. **Generate a PAT:**

   - Go to your **User Profile** > **Settings** > **Developer settings** > **Personal access tokens**. Alternatively, you can follow this link: [GitHub Token Settings](https://github.com/settings/tokens).
   - Click on **Generate new token (classic)** and authenticate your user.
   - In the **Note** field, enter a name for your token (it can be anything you prefer).
   - Set the **Expiration** time according to your preference.
   - Select the following scopes:
     - **write:packages** - Allows you to upload packages to GitHub Package Registry.
     - **read:packages** - Enables downloading packages from GitHub Package Registry.
   - Click **Generate Token**.

   Be sure to **copy** the generated token and keep it in a secure place. This token can be used in multiple repositories.

2. **Add the Token to Repository Secrets:**

   - Navigate to your **AnyoneDrive** repository on GitHub.
   - Go to **Settings** > **Secrets and variables** > **Actions**. You can access it directly via this link: [Repository Secrets](https://github.com/Jandini/AnyoneDrive/settings/secrets/actions).
   - Click on **New repository secret**.
   - Set the **Name** to `PACKAGE_REGISTRY_TOKEN`, matching what's declared in your `build.yml`.
   - Paste the PAT token you obtained earlier into the **Secret** text box.

Now, the secret `PACKAGE_REGISTRY_TOKEN` is securely stored within your GitHub repository. Please note that you can update an existing secret if your PAT token expires. However, you won't be able to view the current value for security reasons.

Return to GitHub Actions and re-run any previously failed jobs. You should now have a successful build, and the first NuGet package will be available in your GitHub private NuGet registry.

![image-20231007001333416](https://github.com/Jandini/JandaBox/assets/19593367/192e8ae9-b94e-4085-8b34-b79896b5f620)







## Resources

- Box icon was downloaded from [Flaticon](https://www.flaticon.com/free-icon/open-box_869027?term=box&related_id=869027)
- https://learn.microsoft.com/en-us/dotnet/core/tools/custom-templates
- https://github.com/dotnet/templating/wiki
