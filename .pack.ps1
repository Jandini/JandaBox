$env:DOTNET_CLI_TELEMETRY_OPTOUT="1"

# Make sure dotnet git version is installed
if (!(Get-Command dotnet-gitversion -ErrorAction SilentlyContinue)) {
    Write-Warning "GitVersion.Tool is missing"
    Write-Host "Installing GitVersion.Tool"
    dotnet tool install --global GitVersion.Tool --version 5.*

    if ($LASTEXITCODE -ne 0) {
        Write-Warning "Installation failed with error $LASTEXITCODE."
    }
    else {
        Write-Warning "GitVersion.Tool installation complete"
    }
}

if (!(Get-Command nuget -ErrorAction SilentlyContinue)) {
    Write-Warning "Nuget.exe command is missing"
}


$gitVersion = $(dotnet gitversion /showvariable SemVer)

if ($LASTEXITCODE -ne 0) {
    $gitVersion = "0.1.0"
    Write-Warning "GitVersion failed. Packing JandaBox.$gitVersion"
}
else {
    Write-Host "Packing JandaBox.$gitVersion"
}


nuget pack .nuspec -OutputDirectory bin\Release -NoDefaultExcludes -Version ${gitVersion}

dotnet new uninstall JandaBox | Out-Null
dotnet new install bin/Release/JandaBox.${gitVersion}.nupkg
