#!/bin/bash

if ! command -v dotnet-gitversion &> /dev/null; then
  echo "GitVersion tool is not installed. Installing..."
  DOTNET_NOLOGO=1
  dotnet tool install --global GitVersion.Tool --version 5.*
else
  echo "GitVersion tool is already installed."
fi

# Check and install Mono if not present
if ! command -v mono &> /dev/null; then
  echo "Mono is not installed. Installing..."
  sudo apt update
  sudo apt install -y mono-devel
else
  echo "Mono is already installed."
fi

# Check and install NuGet if not present
if [ ! -f /usr/local/bin/nuget.exe ]; then
  echo "NuGet is not installed. Installing..."
  sudo wget -O /usr/local/bin/nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
else
  echo "NuGet is already installed."
fi

# Get GitVersion
GIT_VERSION=$(dotnet gitversion /showvariable SemVer)
HEADER="Created with JandaBox"
REPLACEMENT="$HEADER $GIT_VERSION"

echo Updating version in Program.cs files.
find . -type f -name "Program.cs" -exec sed -i "s/$HEADER/$REPLACEMENT/g" {} +

# Pack the NuGet package using Mono
mono /usr/local/bin/nuget.exe pack .nuspec -OutputDirectory bin/Release -NoDefaultExcludes -Version $GIT_VERSION

echo Reverting version change in Program.cs files.
find . -type f -name "Program.cs" -exec sed -i "s/$REPLACEMENT/$HEADER/g" {} +

# Uninstall and reinstall the NuGet package
dotnet new uninstall JandaBox > /dev/null
dotnet new install bin/Release/JandaBox.$GIT_VERSION.nupkg