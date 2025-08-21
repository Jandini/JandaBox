#!/bin/bash

if ! command -v dotnet-gitversion &> /dev/null; then
  echo "GitVersion tool is not installed. Installing..."
  DOTNET_NOLOGO=1
  dotnet tool install --global GitVersion.Tool --version 5.*
else
  echo "GitVersion tool is already installed."
fi

# Get GitVersion
GIT_VERSION=$(dotnet gitversion /showvariable SemVer)
HEADER="Created with JandaBox"
REPLACEMENT="$HEADER $GIT_VERSION"

echo Updating version in Program.cs files.
find . -type f -name "Program.cs" -exec sed -i "s/$HEADER/$REPLACEMENT/g" {} +

# Pack the NuGet package using Mono
dotnet pack -p:Version=$GIT_VERSION

echo Reverting version change in Program.cs files.
find . -type f -name "Program.cs" -exec sed -i "s/$REPLACEMENT/$HEADER/g" {} +

# Uninstall and reinstall the NuGet package
dotnet new uninstall JandaBox > /dev/null
dotnet new install bin/Release/JandaBox.$GIT_VERSION.nupkg