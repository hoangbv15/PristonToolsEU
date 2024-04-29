# PristonToolsEU - A Boss Timer Priston Tale EU
## With audio alarm!

This is a desktop application with the purpose of showing the time remaining until bosses spawn in the MMO game [Priston Tale EU](https://pristontale.eu/).

This app is written in _C# .NET_ using [MAUI](https://dotnet.microsoft.com/en-us/apps/maui), the very latest cross-platform application development framework from Microsoft.

## How to build locally

You will need a machine running either Windows or macOS.

### Using Visual Studio
The most straightforward way to build the app locally is to use Visual Studio. I have not tested this on macOS but the process should be rather similar. Simply download the installer, select MAUI when being prompted for development platforms to install.
After everything is installed, open the .sln file, select Debug or Release configuration, and click the Play button. Standard procedures.

### Using Visual Studio Code
This is a bit more involved. In addition to having VSCode installed, you also need
- The latest version of dotnet 8 from [here](https://dotnet.microsoft.com/en-us/download)
- [.NET MAUI extension package](https://devblogs.microsoft.com/visualstudio/announcing-the-dotnet-maui-extension-for-visual-studio-code/) for VSCode

With these installed, open the project's root folder in VSCode, and go to the Run and Debug tab, click Run and Debug.

### Using terminal
This is useful if you want to build a release package locally. However github workflow CI has been setup for this project so there's not much need to do this. 
If you still would like to know how, you will need:
- The latest version of dotnet 8 from [here](https://dotnet.microsoft.com/en-us/download)
Note that this will be automatically available on Windows if you installed Visual Studio with the MAUI development platform.

For the commands to run, refer to the [github workflow file](.github/workflows/ci-maui-dotnet.yml).
