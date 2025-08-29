# Runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

# Add CA certificates if you need to access outside world to reach the nugets
# ADD https://your-certificate-location
# RUN update-ca-certificates

# Restore
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore
WORKDIR /src
# Copy only the project file(s) first for better caching
COPY ConsoleBox.csproj ./
RUN dotnet restore ConsoleBox.csproj

# Build (no restore)
FROM restore AS build
ARG BUILD_CONFIGURATION=Release
ARG BUILD_VERSION=1.0.0
# Now copy the rest of the sources
COPY . .
RUN dotnet build ConsoleBox.csproj -c $BUILD_CONFIGURATION --no-restore /p:DisableGitVersionTask=true /p:Version=$BUILD_VERSION

# Publish (no build)
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG BUILD_VERSION=1.0.0
RUN dotnet publish ConsoleBox.csproj -c $BUILD_CONFIGURATION --no-build -o /app/publish /p:UseAppHost=false /p:DisableGitVersionTask=true /p:Version=$BUILD_VERSION

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsoleBox.dll"]
