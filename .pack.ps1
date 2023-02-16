.init.ps1

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
