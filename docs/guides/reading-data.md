# Reading job information

> **Validation status:** Examples are awaiting validation against a DataStage environment.

The low-level API returns result codes and native-shaped structures. Always check the result before reading an output structure. These examples use an open `DSAPI.DSPROJECT project` from [Getting started](getting-started.md) and `using System; using RKM.DataStage;`.

## Open a job and read its status and name

Replace `ExampleJob` with an existing job. This example queries metadata; it does not run the job.

```csharp
DSAPI.DSJOB job = DSAPI.DSOpenJob(project, "ExampleJob");
if (!job.HasValue)
{
    int error = DSAPI.DSGetLastError();
    throw new InvalidOperationException("DSOpenJob failed: " + error);
}

try
{
    int result = DSAPI.DSGetJobInfo(
        job, DSAPI.JobInfoType.DSJ_JOBSTATUS, out var statusInfo);
    if (result != 0)
        throw new InvalidOperationException("Reading status failed: " + result);

    int status = statusInfo.info.jobStatus;
    Console.WriteLine("Job status code: " + status);

    result = DSAPI.DSGetJobInfo(
        job, DSAPI.JobInfoType.DSJ_JOBNAME, out var nameInfo);
    if (result != 0)
        throw new InvalidOperationException("Reading name failed: " + result);

    string name = DSAPI.Utilities.ExtractStringFromIntPtr(nameInfo.info.jobName);
    Console.WriteLine("Job name: " + name);
}
finally
{
    int result = DSAPI.DSCloseJob(job);
    if (result != 0)
        Console.Error.WriteLine("DSCloseJob failed: " + result);
}
```

Close jobs before closing their project. A successful information call means the query succeeded; it does not mean the job itself finished successfully. Interpret the returned status using the job status values in the reference.

## Match the selector to the union member

`DSJOBINFO.info` is a union: its members share the same memory. Read the member corresponding to the selector used in the call.

| Selector | Member | How to read it |
| --- | --- | --- |
| `DSJ_JOBSTATUS` | `info.jobStatus` | Read the integer directly |
| `DSJ_JOBNAME` | `info.jobName` | Copy with `ExtractStringFromIntPtr` |
| `DSJ_STAGELIST` | `info.stageList` | Copy with `ExtractStringListFromIntPtr` |

Reading a pointer member after requesting an integer can interpret the integer as a memory address. Do not inspect every union member to discover which one contains data.

## Handle errors according to the method

Information calls shown here return zero on success and an error code on failure. Handle-returning methods instead use `HasValue`; obtain `DSGetLastError()` immediately after failure, before another native call can change it. Methods returning lists can use `null` for failure. Other methods, such as log-ID queries, have their own result conventions: consult the individual reference page rather than applying one rule to all calls.

Related: [DSJOBINFO](../api/RKM.DataStage.DSAPI.DSJOBINFO.html), [JobInfoType](../api/RKM.DataStage.DSAPI.JobInfoType.html), [Marshalling](marshalling.md).

Found a problem or have a suggestion? Contact [support@rkmresearch.com](mailto:support@rkmresearch.com).
