/*
 * RKM.DataStage.DSAPI
 * Low-level .NET wrapper for the IBM DataStage DSAPI
 *
 * Copyright © 2022–2026 Richard K. Morris / RKM Research
 */

using System;
using System.Runtime.InteropServices;

namespace RKM.DataStage
{
    public static partial class DSAPI
    {
        /// <summary>
        /// Native entry points exposed by the IBM DataStage DSAPI library
        /// </summary>
        /// <remarks>
        /// Keep these declarations consistent with the native C API definitions
        /// </remarks>
        internal static class NativeMethods
        {
            // Library name used by the native function declarations
            private const string nativeLibName = "vmdsapi.dll";


            // DSServerMessage is not supported by this wrapper

            /// <summary>
            /// Closes a job handle
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSCloseJob(IntPtr jobHandle);

            /// <summary>
            /// Closes a project handle
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSCloseProject(IntPtr projectHandle);

            /// <summary>
            /// Finds the first job log entry matching the supplied criteria
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSFindFirstLogEntry(IntPtr jobHandle, int eventType, long startTime, long endTime, int maxNumber, out DSLOGEVENT returnEvent);

            /// <summary>
            /// Finds the next job log entry
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSFindNextLogEntry(IntPtr jobHandle, out DSLOGEVENT returnEvent);

            /// <summary>
            /// Retrieves job information for the requested information type
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetJobInfo(IntPtr jobHandle, int infoType, out DSJOBINFO returnInfo);

            /// <summary>
            /// Retrieves the last native DSAPI error code
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetLastError();

            /// <summary>
            /// Retrieves the native error message for a project handle
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr DSGetLastErrorMsg(IntPtr projectHandle);

            /// <summary>
            /// Retrieves information about a stage link
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetLinkInfo(IntPtr jobHandle, string stageName, string linkName, int infoType, out DSLINKINFO returnInfo);

            /// <summary>
            /// Retrieves information about a stage variable
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetVarInfo(IntPtr jobHandle, string stageName, string varName, int infoType, out DSVARINFO returnInfo);

            /// <summary>
            /// Retrieves custom information for a stage
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetCustInfo(IntPtr jobHandle, string stageName, string custInfoName, int infoType, out DSCUSTINFO returnInfo);

            /// <summary>
            /// Retrieves details for a job log entry
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetLogEntry(IntPtr jobHandle, int eventId, out DSLOGDETAIL returnEvent);

            /// <summary>
            /// Retrieves full details for a job log entry
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetLogEntryFull(IntPtr jobHandle, int eventId, out DSLOGDETAILFULL returnEvent);

            /// <summary>
            /// Retrieves the newest job log event identifier for a type
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetNewestLogId(IntPtr jobHandle, int eventType);

            /// <summary>
            /// Retrieves information about a job parameter
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetParamInfo(IntPtr jobHandle, string paramName, out DSPARAMINFO returnInfo);

            /// <summary>
            /// Retrieves project information for the requested information type
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetProjectInfo(IntPtr projectHandle, int infoType, out DSPROJECTINFO returnInfo);

            /// <summary>
            /// Retrieves the list of projects
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr DSGetProjectList();

            /// <summary>
            /// Retrieves repository object information
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetReposInfo(IntPtr projectHandle,
                int objectType, int infoType,
                string searchCriteria, string startingCategory,
                int subCategories,
                out DSREPOSINFO returnInfo);

            /// <summary>
            /// Retrieves repository object usage information
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetReposUsage(IntPtr projectHandle,
                int relationshipType,
                string objectName,
                int recursive,
                out DSREPOSUSAGE returnInfo);

            /// <summary>
            /// Retrieves information about a job stage
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetStageInfo(IntPtr jobHandle, string stageName, int infoType, out DSSTAGEINFO returnInfo);

            /// <summary>
            /// Locks a job
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSLockJob(IntPtr jobHandle);

            /// <summary>
            /// Writes an event to the job log
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSLogEvent(IntPtr jobHandle, int eventType, string reserved, string message);

            /// <summary>
            /// Opens a job within a project
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr DSOpenJob(IntPtr projectHandle, string jobName);

            /// <summary>
            /// Opens a project using a specified API version
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr DSOpenProjectEx(int version, string projectName);

            /// <summary>
            /// Runs a job using the specified run mode
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSRunJob(IntPtr jobHandle, int runMode);

            /// <summary>
            /// Sets a job execution limit
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSSetJobLimit(IntPtr jobHandle, int limitType, int limitValue);

            /// <summary>
            /// Sets the operational metadata generation option
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSSetGenerateOpMetaData(IntPtr jobHandle, int value);

            /// <summary>
            /// Sets the project handler disable option
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSSetDisableProjectHandler(IntPtr jobHandle, int value);

            /// <summary>
            /// Sets the job handler disable option
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSSetDisableJobHandler(IntPtr jobHandle, int value);

            /// <summary>
            /// Sets a named job parameter
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSSetParam(IntPtr jobHandle, string paramName, ref DSPARAM param);

            /// <summary>
            /// Sets the server connection parameters
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void DSSetServerParams(string domain, string username, string password, string server);

            /// <summary>
            /// Requests that a job stop
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSStopJob(IntPtr jobHandle);

            /// <summary>
            /// Unlocks a job
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSUnlockJob(IntPtr jobHandle);

            /// <summary>
            /// Waits for a job
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSWaitForJob(IntPtr jobHandle);

            /// <summary>
            /// Generates a report for a job
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSMakeJobReport(IntPtr jobHandle, int reportType, string lineSeparator, out DSREPORTINFO returnInfo);

            /// <summary>
            /// Retrieves a job identifier by job name
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr DSGetIdForJob(IntPtr projectHandle, string jobName);

            /// <summary>
            /// Sets the identifier for a named job
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSSetIdForJob(IntPtr projectHandle, string jobName, string jobId);

            /// <summary>
            /// Retrieves a job name by its identifier
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr DSJobNameFromJobId(IntPtr projectHandle, string jobId);

            /// <summary>
            /// Retrieves log event identifiers matching a filter
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetLogEventIds(IntPtr jobHandle, int runNumber, string filter, out IntPtr returnList);

            /// <summary>
            /// Sets the job queue
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSSetJobQueue(IntPtr jobHandle, string queueName);

            /// <summary>
            /// Retrieves the workload management enabled state
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSGetWLMEnabled();

            /// <summary>
            /// Retrieves the list of job queues
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr DSGetQueueList();

            /// <summary>
            /// Purges job information according to the purge specification
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSPurgeJob(IntPtr jobHandle, int purgeSpec);

            /// <summary>
            /// Adds a project
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSAddProject(string projectName, string projectLocation);

            /// <summary>
            /// Deletes a project
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSDeleteProject(string projectName);

            /// <summary>
            /// Sets a project property
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSSetProjectProperty(IntPtr projectHandle, string property, string value);

            /// <summary>
            /// Adds a project environment variable
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSAddEnvVar(IntPtr projectHandle, string envVarName, string type, string promptText, string value);

            /// <summary>
            /// Sets a project environment variable value
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSSetEnvVar(IntPtr projectHandle, string name, string value);

            /// <summary>
            /// Deletes a project environment variable
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int DSDeleteEnvVar(IntPtr projectHandle, string envVarName);

            /// <summary>
            /// Lists project properties
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr DSListProjectProperties(IntPtr projectHandle);

            /// <summary>
            /// Lists project environment variables
            /// </summary>
            [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr DSListEnvVars(IntPtr projectHandle);
        }
    }
}
