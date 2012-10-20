# M-Files SDK

### Downloads

[Latest release build](http://mfiles.jubjubnest.net/guestAuth/repository/download/bt3/lastSuccessful/MFiles.SDK%20%7Bbuild.number%7D.zip) is the latest manually created build.  
[Latest continuous integration build](http://mfiles.jubjubnest.net/guestAuth/repository/download/bt2/lastSuccessful/MFiles.SDK%20%7Bbuild.number%7D.zip) is the latest build automatically created from the head of the repository.

The packages are built in [TeamCity](http://mfiles.jubjubnest.net/).

## MSBuild Tasks

MSBuild tasks for packaging M-Files applications. Currently there are two tasks.
One utility task for performing M-Files Client logout from MSBuild scripts and
one task for creating the M-Files Application zip packages.

## Visual Studio project type

Visual Studio project for M-Files applications includes a new project type for
Visual Studio for creating new M-Files applications. The project makes it easier
to use the MSBuild tasks for developing M-Files applications and supports the
basic Visual Studio features such as launching the application from the IDE and
using project references to share common code between projects.

![M-Files Application project](http://ssh.jubjubnest.net/~wace/code.png)

The project enables easy configuration of the M-Files Application package
properties through the familiar Visual Studio project settings.

![M-Files Application project settings](http://ssh.jubjubnest.net/~wace/project-properties.png)

It also makes it easy to debug the application. The Debug properties include
options for the document vault used in testing as well as launch options.
During the launch (F5) the project deploys the current application locally
for the configured vault and performs a vault relogin. This enables a fast
Hack-F5-Test-Hack cycle for M-Files Applications as well.

![M-Files Application debug settings](http://ssh.jubjubnest.net/~wace/debug-properties.png)

## Copyright

M-Files is copyright of M-Files Corporation

