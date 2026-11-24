# Crystal Defender

A Robotron (Smash TV)-style top-down shooter, originally written for Xbox using XNA and sold through Xbox Indie Games. The current Windows edition uses .NET 10 and MonoGame.

## Build and run on Windows

1. Install the .NET 10 SDK.
2. Open `MonoGameWinDeskApp.sln` in Visual Studio or run `dotnet build MonoGameWinDeskApp\MonoGameWinDeskApp.csproj` from the repository root.
3. Run the `MonoGameWinDeskApp` project.

The game uses MonoGame's DesktopGL backend and portable MonoGame APIs. Platform-specific launch, packaging, and store integration remain isolated from the game code to make later platform work easier; this does not itself make an Xbox build.

## Microsoft Store MSIX packaging

The solution contains a separate `CrystalDefender.Package` Windows Application Packaging Project under `Packaging`. To build a package, install Visual Studio's **Universal Windows Platform development** workload (including MSIX/Desktop Bridge support), open the solution in Visual Studio, and build the packaging project in x64 configuration. The project is excluded from the solution's default build so that regular .NET CLI builds do not require the Visual Studio packaging workload.

The package reference publishes the game self-contained for x64 so end users do not need to install the .NET runtime separately. Before submission, reserve the app in Partner Center and use Visual Studio's **Associate App with the Store** workflow to replace the placeholder package identity and publisher with the values assigned to your Store listing. Configure package signing for local testing or Store submission as prompted; do not commit signing certificates or private keys. Review the Store listing artwork and submission details in Partner Center before publishing.
