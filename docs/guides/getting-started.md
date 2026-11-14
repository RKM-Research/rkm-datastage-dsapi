# Getting started

> **Validation status:** Examples are awaiting validation against a DataStage environment.

Use this guide to make a first read-only connection with the low-level wrapper. The NuGet package is named `RKM.DataStage.DSAPI`; its namespace is `RKM.DataStage`, and its entry point is the static `DSAPI` class.

## 1. Create a console project

On Windows, open Visual Studio with the **.NET desktop development** workload and .NET 10 SDK installed.

1. Select **Create a new project**, choose the C# **Console App** template, and select **Next**.
2. Name the project `DataStageGettingStarted`, choose its location, and select **Next**.
3. Select **.NET 10.0** and create the project.
4. In Solution Explorer, right-click the project and select **Edit Project File**. Change `TargetFramework` from `net10.0` to `net10.0-windows`, then save.

The package also supports .NET Framework 4.8. For that framework, create a **Console App (.NET Framework)** targeting 4.8 instead; the SDK-style project settings below are for .NET 10.

## 2. Install the NuGet package

NuGet adds the managed wrapper to your project and restores its package dependencies.

1. Right-click the **project** in Solution Explorer and select **Manage NuGet Packages**.
2. Select **Browse** and choose **nuget.org** as the package source.
3. Tick **Include prerelease** to find preview releases.
4. Search for **RKM.DataStage.DSAPI** and select the [RKM.DataStage.DSAPI package](https://www.nuget.org/packages/RKM.DataStage.DSAPI/).
5. Choose the version to use, select **Install**, and accept any prompts after reviewing them.
6. Check that the package appears under **Dependencies > Packages**.

You do not need to download or build the wrapper source to use the NuGet package.

## 3. Make the application 32-bit (x86)

IBM's native DSAPI library is 32-bit, so the **application process** must also be 32-bit, even on 64-bit Windows.

1. Right-click the project, select **Properties**, and open its **Build** settings.
2. Set **Platform target** to **x86** for both Debug and Release configurations. The exact location of this setting varies between Visual Studio versions.
3. For the .NET 10 project, use **Edit Project File** to check that the following properties are present in an unconditional `PropertyGroup`. Keep the `PackageReference` that NuGet added.

```xml
<PropertyGroup>
  <OutputType>Exe</OutputType>
  <TargetFramework>net10.0-windows</TargetFramework>
  <PlatformTarget>x86</PlatformTarget>
  <RuntimeIdentifier>win-x86</RuntimeIdentifier>
</PropertyGroup>
```

`PlatformTarget` compiles the application for x86; `RuntimeIdentifier` selects a matching Windows x86 executable host. Install the **.NET 10 Runtime for Windows x86** when running a framework-dependent application. A 64-bit runtime alone cannot run it.

For .NET Framework 4.8, select **x86** in the project's Build properties. Do not rely on **Any CPU** or **Prefer 32-bit** for this guide.

## 4. Prepare the IBM client runtime

Install a compatible, licensed IBM **32-bit DataStage client runtime**, including `vmdsapi.dll` and its native dependencies, on the Windows machine where the application will run. Follow your IBM installation's instructions so the native DLLs can be found at runtime.

**The NuGet package does not install IBM software or supply a DataStage licence.** A missing native dependency can cause a DLL-loading failure even when `vmdsapi.dll` itself is present.

Have your DataStage domain, server, project name and connection credentials ready.

## 5. Check the process before connecting

Temporarily replace `Program.cs` with:

```csharp
using System;

Console.WriteLine(Environment.Is64BitProcess
    ? "64-bit process: check the project's x86 settings"
    : "32-bit process: ready to try the connection example");
```

Select **Build > Build Solution**, then **Debug > Start Without Debugging**. Confirm that it reports **32-bit process**. This checks the process architecture only; the next example checks access to the IBM runtime and DataStage environment.

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
