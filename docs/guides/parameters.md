# Passing job parameter values

> **Validation status:** Examples are awaiting validation against a DataStage environment.

`DSSetParam` takes a `DSPARAM` containing a type discriminator and a union of possible values. Set the discriminator and the matching member. The parameter must already exist in the job and its declared type must match the supplied value.

The example below assumes an open `DSAPI.DSJOB job`, `using System; using RKM.DataStage;`, and a string parameter named `InputFile`. It changes the value for a subsequent run through this handle; it does not start the job or edit its design-time default.

## Allocate, set, and free a string value

The wrapper's method documentation requires the job to be locked before setting parameters. Only unlock after a successful lock.

```csharp
int lockResult = DSAPI.DSLockJob(job);
if (lockResult != 0)
    throw new InvalidOperationException("DSLockJob failed: " + lockResult);

try
{
    IntPtr text = DSAPI.Utilities.StringToHGlobalAnsi(@"C:\Data\input.csv");
    try
    {
        var parameter = new DSAPI.DSPARAM
        {
            paramType = (int)DSAPI.ParamType.DSJ_PARAMTYPE_STRING,
            paramValue = new DSAPI.DSPARAM.PARAM_UNION { pString = text }
        };

        int result = DSAPI.DSSetParam(job, "InputFile", ref parameter);
        if (result != 0)
            throw new InvalidOperationException("DSSetParam failed: " + result);
    }
    finally
    {
        // Release only the buffer allocated by this example
        DSAPI.Utilities.FreeHGlobal(text);
    }
}
finally
{
    int result = DSAPI.DSUnlockJob(job);
    if (result != 0)
        Console.Error.WriteLine("DSUnlockJob failed: " + result);
}
```

Keep the allocation alive for the synchronous `DSSetParam` call and release it afterwards. Do not use the pointer again after freeing it. Close the job and project using the surrounding `finally` blocks shown in the earlier guides.

## Choose the correct member

| Parameter type | Union member | Representation |
| --- | --- | --- |
| `DSJ_PARAMTYPE_STRING` | `pString` | Pointer to an ANSI string |
| `DSJ_PARAMTYPE_INTEGER` | `pInt` | Managed `int` directly |
| `DSJ_PARAMTYPE_FLOAT` | `pFloat` | Managed `float` directly |
| `DSJ_PARAMTYPE_PATHNAME` | `pPath` | Pointer to an ANSI string |
| `DSJ_PARAMTYPE_DATE` | `pDate` | Pointer to a date string formatted as `yyyy-MM-dd` |
| `DSJ_PARAMTYPE_TIME` | `pTime` | Pointer to a time string formatted as `HH:mm:ss` |

For date and time strings use `DateTimeToParamShortDateString` and `DateTimeToParamShortTimeString`, then allocate and release the resulting ANSI string as above. Integer and float values need no unmanaged string allocation. This table covers the common types; consult the enum and IBM reference for other types.

Related: [DSPARAM](../api/RKM.DataStage.DSAPI.DSPARAM.html), [ParamType](../api/RKM.DataStage.DSAPI.ParamType.html), [Memory ownership](marshalling.md).

Found a problem or have a suggestion? Contact [support@rkmresearch.com](mailto:support@rkmresearch.com).
