#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG BUILD_VERSION=1.0.0
WORKDIR /src
COPY ["WebApiBox/WebApiBox.csproj", "WebApiBox/"]
RUN dotnet restore "./WebApiBox/WebApiBox.csproj"
COPY . .
WORKDIR "/src/WebApiBox"
RUN dotnet build "./WebApiBox.csproj" -c $BUILD_CONFIGURATION -o /app/build /p:DisableGitVersionTask=true /p:Version=$BUILD_VERSION

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApiBox.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false /p:DisableGitVersionTask=true /p:Version=$BUILD_VERSION

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiBox.dll"]