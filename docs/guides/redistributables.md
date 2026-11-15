# Redistributables and authentication requirements

The RKM.DataStage.DSAPI NuGet package contains the managed wrapper. IBM's native runtime must be supplied separately, and the required components depend on how the application authenticates.

**A deployment using only the listed redistributable DLLs is for direct engine logins, equivalent to `dsjob -domain NONE`. Domain authentication and credential mapping require the fuller IBM runtime, including ASBNode and its matching Java installation.**

## IBM's redistribution guidance

Start with IBM's [Redistributing Applications](https://www.ibm.com/docs/en/iis/11.7.0?topic=kit-redistributing-applications) documentation.

| Target computer | IBM's listed DSAPI libraries |
| --- | --- |
| Engine tier installed | DSAPI libraries are already installed with the engine |
| DataStage client installed | `vmdsapi.dll` |
| Neither engine nor client installed | `vmdsapi.dll`, `dsclnt32.dll`, `dsrpc32.dll`, `ACS_common_cpp.dll`, `ACS_client_cpp.dll`, `invocation_cpp.dll`, `xmogrt.dll` |

Use compatible IBM 32-bit libraries for this wrapper and run the application as x86. The libraries and their dependencies must be discoverable by the application.

The linked redistribution page does not explain the additional ASBNode and Java requirements for domain authentication. Treat its DLL list as a starting point, not a complete deployment recipe for every authentication mode.

## Direct engine login: domain NONE

IBM's [logon clause documentation](https://www.ibm.com/docs/en/iis/11.5.0?topic=jobs-logon-clause) explains that `-domain NONE` logs on to the engine directly, using engine credentials rather than Information Server domain credentials.

For DSAPI, pass an empty domain string:

```csharp
// Authenticate directly to the engine using engine credentials
DSAPI.DSSetServerParams("", engineUsername, enginePassword, engineServer);
```

Here the variables contain the engine username, password and server name. This bypasses domain authentication and domain-to-engine credential mapping. Supplying a domain user's credentials does not make credential mapping available in this mode.

## Domain authentication and credential mapping

For deployments using Information Server domain authentication and credential mapping, our deployment guidance is to install the complete matching ASBNode environment and IBM-supplied JDK/runtime through the appropriate IBM installation.

Copying only the DLLs above is insufficient for this login path. A standalone JDK installation is not a substitute for ASBNode's Java libraries and configuration. Use the IBM runtime supplied for the applicable DataStage version.

### Why the installed dsjob.exe works

IBM's `dsjob.exe` is installed on both the client tier and the engine tier. Both installations include ASBNode, so the installed executable has access to the supporting authentication environment.

Successful domain authentication with `dsjob.exe` on either installed tier therefore does not demonstrate that the executable and the listed redistributable DLLs form a self-contained deployment. Copying those files to a machine without ASBNode does not reproduce the installed environment needed for domain authentication and credential mapping.

## CodeMesh and the embedded JVM

The CodeMesh `xmogrt.dll` runtime provides the native-to-Java bridge used by this authentication path. It needs an embedded JVM within the native application's process to load and execute the dependent Java classes. The bridge DLL alone therefore does not supply the complete authentication runtime: the matching JVM, Java libraries and supporting configuration must also be available.

The presence of a bridging DLL does not itself provide the JVM or the Java classes it needs. IBM's redistribution page does not describe how to provision that Java environment, and does not provide a minimal ASBNode/JDK redistribution recipe.

## Diagnosing deployment failures

Establish which login mode the application requires before investigating missing libraries or authentication errors. A successful direct engine login does not verify that domain authentication or credential mapping is configured.

See [Error codes and troubleshooting](error-codes.md) for DSAPI, UniVerse and RPC errors, and [Getting started](https://rkmresearch.com/docs/dsapi/guides/getting-started.html) for the wrapper's x86 setup.

---

IBM, DataStage and InfoSphere are trademarks or registered trademarks of International Business Machines Corporation in the United States, other countries, or both. RKM Research Ltd is independent of IBM; these tools and documents are not affiliated with, sponsored by or endorsed by IBM. Other product and company names belong to their respective trademark owners.
