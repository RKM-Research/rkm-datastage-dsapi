# RKM.DataStage.DSAPI

A low-level C#/.NET wrapper for the native IBM DataStage DSAPI, developed by Richard K. Morris and maintained by **RKM Research Ltd**.

The library exposes native DataStage API operations to managed .NET applications. It requires a compatible IBM DataStage runtime; it does not replace a DataStage installation.

## Features

- Connect to the DataStage engine and discover projects and jobs
- Open and close projects and jobs
- Retrieve job status, parameter, stage and link information
- Run, reset, stop and wait for jobs
- Read and write job log information
- Use other supported DSAPI functions for environment, workload-management and repository operations

## Requirements

- **Windows x86 (32-bit) processes only.** The native IBM DataStage DSAPI library used by this wrapper is 32-bit.
- **.NET Framework 4.8** or **.NET 10 for Windows** (`net10.0-windows`)
- Compatible IBM 32-bit `vmdsapi.dll` and its native dependencies, installed and available at runtime
- An accessible IBM DataStage environment and appropriate credentials

**This repository and the NuGet package do not include IBM software, native DLLs or DataStage licences.**

## Install

NuGet package: [RKM.DataStage.DSAPI](https://www.nuget.org/packages/RKM.DataStage.DSAPI/)

For .NET 10, ensure the consuming application runs as an **x86** process. A 64-bit application cannot load the 32-bit IBM DSAPI library.

## Build

Open `RKM.DataStage.DSAPI.slnx` in Visual Studio with the .NET 10 SDK and .NET Framework 4.8 targeting pack installed. Build the library for Windows x86.

The project includes NuGet metadata for packaging the managed library. The icon file is expected at `assets/nuget-icon.png` when packing from source.

## Licence

The managed wrapper is released under the [MIT License](LICENSE).

IBM and DataStage are trademarks of their respective owners. This is an independent project and is not endorsed by IBM.

## Support

Maintained by **RKM Research Ltd**. Visit [rkmresearch.com](https://rkmresearch.com) or open an issue in this repository.
