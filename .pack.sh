#!/bin/bash

GIT_VERSION=$(dotnet gitversion /showvariable SemVer)
mono /usr/local/bin/nuget.exe pack .nuspec -OutputDirectory bin/Release -NoDefaultExcludes -Version $GIT_VERSION
dotnet new uninstall JandaBox > /dev/null
dotnet new install bin/Release/JandaBox.$GIT_VERSION.nupkg