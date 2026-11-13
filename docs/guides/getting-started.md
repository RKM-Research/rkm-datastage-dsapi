# Getting started

> **Validation status:** Examples are awaiting validation against a DataStage environment.

Use this guide to make a first read-only connection with the low-level wrapper. The NuGet package is named `RKM.DataStage.DSAPI`; its namespace is `RKM.DataStage`, and its entry point is the static `DSAPI` class.

## Prepare a console application

In Visual Studio, create a C# console application targeting .NET 10 for Windows, install the `RKM.DataStage.DSAPI` NuGet package, and select **x86** as the platform target in the project build settings. The wrapper also targets .NET Framework 4.8.

Install a compatible licensed IBM 32-bit DataStage client runtime, including `vmdsapi.dll` and its dependencies. IBM software is supplied separately. An x64 process cannot load this native library. A missing dependency can cause a DLL-loading failure even when `vmdsapi.dll` itself is present.

## Connect, list jobs, and close the project

This complete `Program.cs` reads connection values from environment variables. Set them in your local launch environment; do not place real credentials in source control or print passwords. These environment variables are an example convention, not a wrapper configuration feature.

```csharp
using System;
using RKM.DataStage;

internal static class Program
{
    private static string Required(string name)
    {
        string value = Environment.GetEnvironmentVariable(name);
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException("Set " + name);
        return value;
    }

    private static void Main()
    {
        DSAPI.DSSetServerParams(
            Required("DS_DOMAIN"), Required("DS_USERNAME"),
            Required("DS_PASSWORD"), Required("DS_SERVER"));

        DSAPI.DSPROJECT project = DSAPI.DSOpenProject(Required("DS_PROJECT"));
        if (!project.HasValue)
        {
            int error = DSAPI.DSGetLastError();
            throw new InvalidOperationException("DSOpenProject failed: " + error);
        }

        try
        {
            int result = DSAPI.DSGetProjectInfo(
                project, DSAPI.ProjectInfoType.DSJ_JOBLIST, out var info);
            if (result != 0)
                throw new InvalidOperationException("DSGetProjectInfo failed: " + result);

            // Copy the native list before making another DSAPI call
            var jobs = DSAPI.Utilities.ExtractStringListFromIntPtr(info.info.jobList);
            if (jobs != null)
                foreach (string job in jobs)
                    Console.WriteLine(job);
        }
        finally
        {
            int result = DSAPI.DSCloseProject(project);
            if (result != 0)
                Console.Error.WriteLine("DSCloseProject failed: " + result);
        }
    }
}
```

`DSSetServerParams` supplies connection settings; opening the project establishes access. Check `HasValue` on returned handles. Handles are value types and do not implement `IDisposable`; close them explicitly in `finally`. A copied handle refers to the same native resource and must not be closed twice. Its `HasValue` property only checks for a nonzero pointer and does not become false when the resource is closed.

The wrapper documents one open project per process. Keep this first example sequential. A `Task.Run` around a native call does not establish that concurrent use or changes of thread are supported.

## Find the next example

- [Read job information](reading-data.md)
- [Understand data marshalling](marshalling.md)
- [Pass job parameter values](parameters.md)
- [Browse the DSAPI class](../api/RKM.DataStage.DSAPI.html)

For a larger console application, including JSON authentication-file handling, see the [published examples repository](https://github.com/RKM-Research/rkm-datastage-dsapi-examples). The current wrapper exposes `DSSetServerParams`; the example application's authentication-file handling is separate from that API.

Found a problem or have a suggestion? Contact [support@rkmresearch.com](mailto:support@rkmresearch.com).
