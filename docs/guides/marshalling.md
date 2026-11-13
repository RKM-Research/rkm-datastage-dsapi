# Marshalling data and managing memory

> **Validation status:** Examples are awaiting validation against a DataStage environment.

Marshalling moves data between managed .NET values and the native IBM DSAPI representation. The wrapper handles native entry points and structure layouts, but some output fields remain `IntPtr` values. Those values are addresses, not managed strings.

## Know what the wrapper has already converted

| Returned value | Caller action |
| --- | --- |
| `string`, for example `DSGetIdForJob` | Use the managed string directly |
| `List<string>`, for example `DSGetProjectList` | Use the managed list directly; check for `null` |
| `int[]` from `DSGetLogEventIds` | Check the result code, then use the managed array |
| An `IntPtr` string field in an output structure | Copy with `ExtractStringFromIntPtr` |
| An `IntPtr` double-null-terminated string list | Copy with `ExtractStringListFromIntPtr` |
| `DSPROJECT` or `DSJOB` | Treat as an opaque handle and use its matching close method |

## Copy a string or list

For a successful job-name query:

```csharp
string name = DSAPI.Utilities.ExtractStringFromIntPtr(nameInfo.info.jobName);
```

For a successful project job-list query:

```csharp
var jobs = DSAPI.Utilities.ExtractStringListFromIntPtr(projectInfo.info.jobList);
```

The variables above are the output structures from the corresponding information calls. See [Reading job information](reading-data.md) and [Getting started](getting-started.md) for the calls and error checks.

A native string ends with a zero byte. A native string list contains a zero byte after each entry and an additional zero byte after the final entry. The list helper walks these bytes; it must only receive a valid buffer of that format. It is not a general pointer validator or a decoder for arbitrary binary data.

Both helpers return `null` for `IntPtr.Zero`. A valid empty list produces an empty managed list. Neither helper frees the original buffer.

## Ownership and lifetime

| Memory or resource | Owner | Release action |
| --- | --- | --- |
| Pointer returned inside a native output structure | Native DSAPI | Do not pass it to `FreeHGlobal` or `Marshal.FreeHGlobal` |
| Pointer created by `Utilities.StringToHGlobalAnsi` | Your application | Release once with `Utilities.FreeHGlobal` in `finally` |
| Managed string or list copied by a helper | .NET | Normal managed lifetime |
| Project or job handle | Native DSAPI resource held by your application | `DSCloseProject` or `DSCloseJob` |

Copy native output strings and lists promptly, before another DSAPI call or closing the handle. The wrapper does not document a universal lifetime guarantee for all native buffers; consult IBM's documentation for the particular function. Keeping a structure in a managed variable does not extend the lifetime of any native memory it points to. Copying a structure copies its pointers, not the pointed-to data.

## ANSI conversion

The helpers use `Marshal.PtrToStringAnsi` and `Marshal.StringToHGlobalAnsi`. Do not assume the data is UTF-8 or UTF-16. On Windows, ANSI conversion depends on the active code page; verify non-ASCII names and parameter values against the client and engine configuration.

## Structures, unions, and timestamps

Use the wrapper's existing structures rather than defining your own layouts. Native declarations use C calling conventions, explicit unions, and a 32-bit runtime. Changing pointer sizes, field types, or packing can corrupt the interpretation of native memory.

For timestamp fields documented as Unix seconds, use the wrapper's UTC conversion helper:

```csharp
DateTime utc = DSAPI.Utilities.UnixTimestampToDateTime(logDetail.timestamp);
DateTime local = utc.ToLocalTime();
```

Here `logDetail` is a successfully retrieved `DSLOGDETAIL`. Do not apply the timestamp helper to duration fields or unrelated integers.

Related: [Utilities](../api/RKM.DataStage.DSAPI.Utilities.html), [Pass parameter values](parameters.md), [IBM Development Kit](https://www.ibm.com/docs/en/iis/11.7.0?topic=interfaces-infosphere-datastage-development-kit).

Found a problem or have a suggestion? Contact [support@rkmresearch.com](mailto:support@rkmresearch.com).
