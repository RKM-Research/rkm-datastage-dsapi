# Getting started

> **Validation status:** Examples are awaiting validation against a DataStage environment.

Use this guide to make a first read-only connection with the low-level wrapper. The NuGet package is named `RKM.DataStage.DSAPI`; its namespace is `RKM.DataStage`, and its entry point is the static `DSAPI` class.

## 1. Create a console project in Visual Studio

1. Select **Create a new project** and choose the **C# Console App** template.
2. Give the project a name, such as `DsapiGettingStarted`, and select **.NET 10** as the framework.
3. In **Solution Explorer**, right-click the project and select **Edit Project File**. Set the target framework to `net10.0-windows`, as shown below.

## 2. Install the NuGet package

1. Right-click the project in **Solution Explorer** and select **Manage NuGet Packages**.
2. Select **nuget.org** as the package source and open the **Browse** tab.
3. Tick **Include prerelease**, because the wrapper is currently published as a preview.
4. Search for **RKM.DataStage.DSAPI**, select the package, and click **Install**.
5. Review the package details and any licence prompts. Check the **Installed** tab afterwards to confirm that the package was added.

NuGet installs the managed wrapper. IBM's native DataStage client runtime is supplied separately.

## 3. Set the application to 32-bit (x86)

1. Right-click the project and select **Properties**.
2. Under **Build**, find **Platform target** (the exact layout varies between Visual Studio versions) and select **x86**.
3. Apply the setting to each configuration you will use, including **Debug** and **Release**.
4. Edit the project file and check that these properties appear in an unconditional `PropertyGroup`:

```xml
<PropertyGroup>
  <OutputType>Exe</OutputType>
  <TargetFramework>net10.0-windows</TargetFramework>
  <PlatformTarget>x86</PlatformTarget>
  <RuntimeIdentifier>win-x86</RuntimeIdentifier>
</PropertyGroup>
```

Keep the existing package reference and any other project settings. The runtime identifier selects a Windows x86 application host; changing only the solution platform or selecting **Prefer 32-bit** is not a substitute for an x86 process. Install the .NET 10 **x86** runtime on a machine where you run this framework-dependent application.

The wrapper also supports .NET Framework 4.8, but this walkthrough uses .NET 10 for Windows.

## 4. Prepare the IBM client runtime

Install a compatible licensed IBM **32-bit** DataStage client runtime, including `vmdsapi.dll` and its dependencies. Follow IBM's client setup instructions so Windows can locate those DLLs. An x64 process cannot load this native library. A missing dependency can cause a DLL-loading failure even when `vmdsapi.dll` itself is present.

Select **Build > Build Solution** to restore packages and compile the project. A successful build verifies compilation; connecting still requires the IBM runtime, an accessible DataStage environment and valid credentials.

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
