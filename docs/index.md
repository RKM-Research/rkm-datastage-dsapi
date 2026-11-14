# RKM.DataStage.DSAPI API reference

> **Validation status:** Examples are awaiting validation against a DataStage environment.

**RKM.DataStage.DSAPI** is a low-level C#/.NET wrapper for the native DSAPI used by IBM InfoSphere DataStage (DataStage Classic).

This reference is generated from the public .NET library and its XML documentation comments. Use the **API Reference** navigation to browse the `RKM.DataStage` namespace and the `DSAPI` class, methods, enumerations and data structures.

## Compatibility

- **.NET Framework 4.8** and **.NET 10 for Windows**
- **Windows x86 (32-bit process)** because the underlying IBM DSAPI native DLL is 32-bit
- Requires a compatible, licensed IBM DataStage native runtime including `vmdsapi.dll`
- An accessible DataStage environment and appropriate connection credentials

The documentation is generated from the library's .NET 10 target. Both frameworks expose the same public wrapper API.

## Getting started

- [Connect and list jobs](guides/getting-started.md)
- [Read job information](guides/reading-data.md)
- [Marshal data and manage memory](guides/marshalling.md)
- [Pass job parameter values](guides/parameters.md)
- [Public DSAPI source and installation instructions](https://github.com/RKM-Research/rkm-datastage-dsapi)
- [NuGet package](https://www.nuget.org/packages/RKM.DataStage.DSAPI/)
- [Public examples](https://github.com/RKM-Research/rkm-datastage-dsapi-examples)

## Underlying IBM API

See the [IBM InfoSphere DataStage Development Kit (11.7) documentation](https://www.ibm.com/docs/en/iis/11.7.0?topic=interfaces-infosphere-datastage-development-kit) for IBM's native DataStage interfaces.

This reference describes an independent RKM Research Ltd wrapper. IBM and DataStage are trademarks of their respective owners; the project is not affiliated with or endorsed by IBM.


Found a problem or have a suggestion? Contact [support@rkmresearch.com](mailto:support@rkmresearch.com).
