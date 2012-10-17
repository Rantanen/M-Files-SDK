# Building M-Files SDK

## Dependencies

There are some third party components that are required for building the
projects inclkuded in M-Files SDK. When possible these dependencies are
included in the `/Libs` folder in the repository.

The dependencies listed below are external dependencies that are not included
in the repository and instead must be installed separately.

### MFiles.SDK.Setup

* WiX Toolset
  http://wixtoolset.org

### MFiles.SDK.Tasks

* M-Files API
  <http://www.m-files.com>

### MFiles.SDK.VisualStudio.Application

* Visual Studio 2012 SDK  
  <http://msdn.microsoft.com/en-us/library/bb166441.aspx>  
  ((Download link))[http://www.microsoft.com/en-us/download/details.aspx?id=30668]
* M-Files API
  <http://www.m-files.com>

## Build instructions

Once the dependencies are installed the build itself should be quite normal.
The SDK can be built by building the `MFiles.SDK.sln` solution at the root of
the repository.

The release package is built by building the `release.proj` in this directory.
The release build accepts `Version` property in the a.b.c.d version format to
alter the Assembly and Extension versioning.

__Note:__ Visual Studio SDK doesn't build that well under 64-bit msbuild. Make
sure you use 32-bit msbuild to build the SDK.
