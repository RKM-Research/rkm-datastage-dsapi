/*
 * RKM.DataStage.DSAPI
 * Low-level .NET wrapper for the IBM DataStage DSAPI.
 *
 * Copyright © 2022–2026 Richard K. Morris / RKM Research
 */

using System;
using System.Collections.Generic;

namespace RKM.DataStage
{
    /// <summary>
    /// Provides managed access to the native IBM InfoSphere DataStage API (DSAPI).
    /// </summary>
    /// <remarks>
    /// Exposes low-level operations for connecting to DataStage and managing projects,
    /// jobs, parameters, stages, links, and job logs.
    /// Requires a compatible 32-bit IBM DataStage native runtime and a Windows x86 process.
    /// </remarks>
    public static partial class DSAPI
    {
        #region APIs for Accessing projects

        /// <summary>
        /// Closes a previously opened project.
        /// </summary>
        /// <param name="projectHandle">The value returned from <see cref="DSAPI.DSOpenProject"/></param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// Any open jobs in the project are closed, running jobs are allowed to finish, and the function
        /// returns immediately.
        /// </remarks>
        public static int DSCloseProject(DSPROJECT projectHandle)
        {
            return NativeMethods.DSCloseProject(projectHandle.NativePointer);
        }

        /// <summary>
        /// Retrieves a list of all projects on the engine.
        /// </summary>
        /// <returns>
        /// If the function succeeds, the return value is a string list of projects.
        /// If the function fails, the return value is null.
        /// Use <see cref="DSAPI.DSGetLastError"/> to retrieve the error code.
        /// </returns>
        /// <remarks>
        /// This function can be called before any other DSAPI-CS function.
        /// 
        /// Note: DSGetProjectList opens, uses, and closes its own communications link with the engine,
        /// so it might take some time to retrieve the project list.
        /// </remarks>
        public static List<string> DSGetProjectList()
        {
            IntPtr pointer = NativeMethods.DSGetProjectList();
            return Utilities.ExtractStringListFromIntPtr(pointer);
        }

        /// <summary>
        /// Retrieves information about a project, such as a list of jobs, etc.
        /// </summary>
        /// <param name="projectHandle">The value returned from <see cref="DSAPI.DSOpenProject"/></param>
        /// <param name="infoType">One of the <see cref="ProjectInfoType"/> values indicating the type of information to be returned</param>
        /// <param name="returnInfo">[out] Reference to <see cref="DSPROJECTINFO"/> structure to receive the information</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// The <see cref="DSAPI.DSPROJECTINFO"/> data structure contains a union with an element for each of
        /// the possible return values from a call to DSGetProjectInfo.</remarks>
        public static int DSGetProjectInfo(DSPROJECT projectHandle, ProjectInfoType infoType, out DSPROJECTINFO returnInfo)
        {
            return NativeMethods.DSGetProjectInfo(projectHandle.NativePointer, (int)infoType, out returnInfo);
        }

        /// <summary>
        /// Opens a DataStage project.
        /// </summary>
        /// <param name="projectName">Specifies the name of the project to open</param>
        /// <returns>
        /// The return value is a <see cref="DSAPI.DSPROJECT"/> handle to the project.
        /// If the function fails, the IsHandleValid property of the returned handle will be false.
        /// Use <see cref="DSAPI.DSGetLastError"/> to retrieve the error code.
        /// </returns>
        /// <remarks>
        /// The DSGetProjectList function can return the name of a project that does not contain valid jobs,
        /// but this is detected when DSOpenProject is called. A process can only have one project open at a time.
        /// </remarks>
        public static DSPROJECT DSOpenProject(string projectName)
        {
            const int DSAPI_VERSION = 1;
            return new DSPROJECT(NativeMethods.DSOpenProjectEx(DSAPI_VERSION, projectName));
        }

        /// <summary>
        /// Sets the hostname, username, and password to use for a job.
        /// </summary>
        /// <param name="domain">A string specifying the name of the domain machine, or empty for localhost</param>
        /// <param name="username">A string specifying the username to use for the session</param>
        /// <param name="password">A string specifying the password for the session</param>
        /// <param name="server">A string specifying the name of the engine tier</param>
        public static void DSSetServerParams(string domain, string username, string password, string server)
        {
            NativeMethods.DSSetServerParams(domain, username, password, server);
        }

        #endregion

        #region APIs for Accessing jobs

        /// <summary>
        /// Closes a previously opened job.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// If the job is locked when DSCloseJob is called, it is unlocked.
        /// If the job is running when DSCloseJob is called, the job is allowed to finish, and the function
        /// returns a value of DSJE_NOERROR immediately.
        /// </remarks>
        public static int DSCloseJob(DSJOB jobHandle)
        {
            return NativeMethods.DSCloseJob(jobHandle.NativePointer);
        }

        /// <summary>
        /// Retrieves information about the status of a job.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="infoType">One of the <see cref="JobInfoType"/> values indicating the information to be returned</param>
        /// <param name="returnInfo">Reference to <see cref="DSJOBINFO"/> structure to receive the information</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSGetJobInfo(DSJOB jobHandle, JobInfoType infoType, out DSJOBINFO returnInfo)
        {
            return NativeMethods.DSGetJobInfo(jobHandle.NativePointer, (int)infoType, out returnInfo);
        }

        /// <summary>
        /// Locks a job prior to setting job parameters or starting a job run.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// Locking a job prevents any other process from modifying the job details or status.
        /// This function must be called before any call of <see cref="DSAPI.DSSetJobLimit"/>, <see cref="DSAPI.DSSetParam"/>,
        /// or <see cref="DSAPI.DSRunJob"/>. If you try to lock a job you already have locked, the call succeeds.
        /// If you have the same job open on several DSAPI handles, locking the job on one handle locks the
        /// job on all the handles.
        /// </remarks>
        public static int DSLockJob(DSJOB jobHandle)
        {
            return NativeMethods.DSLockJob(jobHandle.NativePointer);
        }

        /// <summary>
        /// Opens a job.
        /// You must call this function before any other function that manipulates the job.
        /// </summary>
        /// <param name="projectHandle">The value returned from <see cref="DSAPI.DSOpenProject"/></param>
        /// <param name="jobName">A string specifying the name of the job that is to be opened</param>
        /// <returns>
        /// The return value is a <see cref="DSAPI.DSJOB"/> handle to the job.
        /// If the function fails, the IsHandleValid property of the returned handle will be false.
        /// Use <see cref="DSAPI.DSGetLastError"/> to retrieve the error code.
        /// </returns>
        /// <remarks>
        /// The DSOpenJob function must be used to return a job handle before a job can be addressed
        /// by any of the DSAPI-CS functions. You can gain exclusive access to the job by locking it
        /// with <see cref="DSAPI.DSLockJob"/>.
        /// The same job can be opened more than once and each call to DSOpenJob will return a unique
        /// job handle. Each handle must be separately closed.
        /// </remarks>
        public static DSJOB DSOpenJob(DSPROJECT projectHandle, string jobName)
        {
            return new DSJOB(NativeMethods.DSOpenJob(projectHandle.NativePointer, jobName));
        }

        /// <summary>
        /// Start a job run.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="runMode">One of the <see cref="DSAPI.RunModeType"/> values indicating the run mode</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// The job specified by jobHandle must be locked, using <see cref="DSAPI.DSLockJob"/>, before the
        /// DSRunJob function is called.
        /// If no limits were set by calling DSSetJobLimit, the default limits are used.
        /// </remarks>
        public static int DSRunJob(DSJOB jobHandle, RunModeType runMode)
        {
            return NativeMethods.DSRunJob(jobHandle.NativePointer, (int)runMode);
        }

        /// <summary>
        /// Aborts a running job.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// The DSStopJob function should be used only after a <see cref="DSAPI.DSRunJob"/> function has been issued.
        /// The stop request is sent regardless of the job's current status. To ascertain if the job has stopped,
        /// use the <see cref="DSAPI.DSWaitForJob"/> or the <see cref="DSAPI.DSGetJobInfo"/> functions.</remarks>
        public static int DSStopJob(DSJOB jobHandle)
        {
            return NativeMethods.DSStopJob(jobHandle.NativePointer);
        }

        /// <summary>
        /// Unlocks a job, enabling other processes to use it.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSOpenJob"/></param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// The DSUnlockJob function returns immediately without waiting for the job to finish.
        /// Attempting to unlock a job that is not locked does not cause an error. If you have the same
        /// job open on several handles, unlocking the job on one handle unlocks it on all handles.
        /// </remarks>
        public static int DSUnlockJob(DSJOB jobHandle)
        {
            return NativeMethods.DSUnlockJob(jobHandle.NativePointer);
        }

        /// <summary>
        /// Waits until a job has completed running.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// This function is only valid if the current job has issued a <see cref="DSAPI.DSRunJob"/> call on the given jobHandle.
        /// It returns if the job was started since the last <see cref="DSAPI.DSRunJob"/>, and has since finished. The
        /// finishing status can be found by calling <see cref="DSAPI.DSGetJobInfo"/>.
        /// </remarks>
        public static int DSWaitForJob(DSJOB jobHandle)
        {
            return NativeMethods.DSWaitForJob(jobHandle.NativePointer);
        }

        #endregion

        #region APIs for Accessing job parameters

        /// <summary>
        /// Retrieves information about a job parameter.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="paramName">A string specifying the name of the parameter to retrieve information about</param>
        /// <param name="returnInfo">[out] Reference to <see cref="DSAPI.DSPARAMINFO"/> structure to receive the information</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// Unlike the other information retrieval functions, DSGetParamInfo returns all the information
        /// relating to the specified item in a single call. The DSPARAMINFO data structure contains all
        /// the information required to request a new parameter value from a user and partially validate
        /// it.
        /// This function can be used either before or after a <see cref="DSAPI.DSRunJob"/> call has been issued:
        /// - If called after a successful call to <see cref="DSAPI.DSRunJob"/>, the information retrieved refers
        ///   to that run of the job.
        /// - If called before a call to <see cref="DSAPI.DSRunJob"/>, the information retrieved refers to any
        ///   previous run of the job, and not to any call to <see cref="DSAPI.DSSetParam"/> that might have
        ///   been issued.
        /// </remarks>
        public static int DSGetParamInfo(DSJOB jobHandle, string paramName, out DSPARAMINFO returnInfo)
        {
            return NativeMethods.DSGetParamInfo(jobHandle.NativePointer, paramName, out returnInfo);
        }

        /// <summary>
        /// Sets row or warning limits for a job run.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="limitType">One of the <see cref="JobLimitType"/> values indicating the type of limit to set</param>
        /// <param name="limitValue">Value to set limit to</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// The job specified by jobHandle must be locked, using <see cref="DSAPI.DSLockJob"/>, before the
        /// DSSetJobLimit function is called.  Any job limits that are not set explicitly before a run
        /// will use the default values.  Make two calls to DSSetJobLimit in order to set both types
        /// of limit. Set the value to 0 to indicate that there should be no limit for the job.
        /// </remarks>
        public static int DSSetJobLimit(DSJOB jobHandle, JobLimitType limitType, int limitValue)
        {
            return NativeMethods.DSSetJobLimit(jobHandle.NativePointer, (int)limitType, limitValue);
        }

        /// <summary>
        /// Sets job parameter values prior to a job run.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="paramName">A string specifying the name of the parameter to set</param>
        /// <param name="param">[in] Reference to <see cref="DSAPI.DSPARAM"/> structure containing the parameter value</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// The job specified by jobHandle must be locked, using <see cref="DSAPI.DSLockJob"/>, before
        /// the DSSetParam function is called.
        /// </remarks>
        public static int DSSetParam(DSJOB jobHandle, string paramName, ref DSPARAM param)
        {
            return NativeMethods.DSSetParam(jobHandle.NativePointer, paramName, ref param);
        }

        #endregion

        #region APIs for Accessing stages

        /// <summary>
        /// Retrieves information about a stage within a job.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="stageName">A string specifying the name of the stage to be interrogated</param>
        /// <param name="infoType">One of the <see cref="StageInfoType"/> values indicating the type of information to be returned</param>
        /// <param name="returnInfo">[out] Reference to <see cref="DSAPI.DSSTAGEINFO"/> structure to receive the information</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// This function can be used either before or after a <see cref="DSAPI.DSRunJob"/> function has been issued.
        /// The <see cref="DSAPI.DSSTAGEINFO"/> data structure contains a union with an element for each of the possible
        /// return values from the call to DSGetStageInfo.</remarks>
        public static int DSGetStageInfo(DSJOB jobHandle, string stageName, StageInfoType infoType, out DSSTAGEINFO returnInfo)
        {
            return NativeMethods.DSGetStageInfo(jobHandle.NativePointer, stageName, (int)infoType, out returnInfo);
        }

        #endregion

        #region APIs for Accessing links

        /// <summary>
        /// Retrieves information about a custom info.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="stageName">A string specifying the name of the stage to be interrogated</param>
        /// <param name="custInfoName">A string specifying the custom info to be interrogated</param>
        /// <param name="infoType">One of the <see cref="DSAPI.CustInfoType"/> values indicating the type of information to be returned</param>
        /// <param name="returnInfo">[out] Reference to <see cref="DSAPI.DSCUSTINFO"/> structure to receive the information</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSGetCustInfo(DSJOB jobHandle, string stageName, string custInfoName, CustInfoType infoType, out DSCUSTINFO returnInfo)
        {
            return NativeMethods.DSGetCustInfo(jobHandle.NativePointer, stageName, custInfoName, (int)infoType, out returnInfo);
        }

        /// <summary>
        /// Retrieves information about a link of an active stage within a job.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="stageName">A string specifying the name of the stage</param>
        /// <param name="linkName">A string specifying the name of the link</param>
        /// <param name="infoType">One of the <see cref="DSAPI.LinkInfoType"/> values indicating the type of information to be returned</param>
        /// <param name="returnInfo">[out] Reference to <see cref="DSAPI.DSLINKINFO"/> structure to receive the information</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// This function can be used either before or after a call to <see cref="DSAPI.DSRunJob"/>.
        /// </remarks>
        public static int DSGetLinkInfo(DSJOB jobHandle, string stageName, string linkName, LinkInfoType infoType, out DSLINKINFO returnInfo)
        {
            return NativeMethods.DSGetLinkInfo(jobHandle.NativePointer, stageName, linkName, (int)infoType, out returnInfo);
        }

        /// <summary>
        /// Retrieves information about a stage variable used in Transformer stages.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="stageName">A string specifying the name of the stage to be interrogated</param>
        /// <param name="varName">A string specifying the name of the variable to be interrogated</param>
        /// <param name="infoType">One of the <see cref="DSAPI.StageVariableType"/> values indicating the type of information to be returned</param>
        /// <param name="returnInfo">[out] Reference to <see cref="DSAPI.DSVARINFO"/> structure to receive the information</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSGetVarInfo(DSJOB jobHandle, string stageName, string varName, StageVariableType infoType, out DSVARINFO returnInfo)
        {
            return NativeMethods.DSGetVarInfo(jobHandle.NativePointer, stageName, varName, (int)infoType, out returnInfo);
        }

        #endregion

        #region APIs for Accessing log entries

        /// <summary>
        /// Retrieves entries in a log that meet the specified criteria.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="eventType">One of the <see cref="DSAPI.LogEventType"/> values indicating the type of log events to be returned</param>
        /// <param name="startTime">Limits the returned log events to those that occurred on, or after the specified date and time (UNIX timestamp).
        ///   Set this value to 0 to return the earliest event.</param>
        /// <param name="endTime">Limits the returned log events to those that occurred before the specified data and time (UNIX timestamp)
        ///   Set this value to 0 to return all entries up to the latest.</param>
        /// <param name="maxNumber">The maximum number of log entries to retrieve, starting from the latest</param>
        /// <param name="returnEvent">[out] Reference to <see cref="DSAPI.DSLOGEVENT"/> structure to receive the first entry</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// The retrieved log entries are cached for retrieval by subsequent calls to <see cref="DSAPI.DSFindNextLogEntry"/>. Any
        /// cached log entries that are not processed by a call to DSFindNextLogEntry are discarded at the next
        /// DSFindFirstLogEntry call (for any job), or when the project is closed.
        /// </remarks>
        /// <seealso cref="DSAPI.Utilities.DateTimeToUnixTimestamp"/>
        public static int DSFindFirstLogEntry(DSJOB jobHandle, LogEventType eventType, long startTime, long endTime, int maxNumber, out DSLOGEVENT returnEvent)
        {
            return NativeMethods.DSFindFirstLogEntry(jobHandle.NativePointer, (int)eventType, startTime, endTime, maxNumber, out returnEvent);
        }

        /// <summary>
        /// Continues to retrieve the next log entry from the cache.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="returnEvent">[out] Reference to <see cref="DSAPI.DSLOGEVENT"/> structure to receive next log entry</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// This function retrieves the next log entry from the cache of entries produced by a call to <see cref="DSAPI.DSFindFirstLogEntry"/>.
        /// </remarks>
        public static int DSFindNextLogEntry(DSJOB jobHandle, out DSLOGEVENT returnEvent)
        {
            return NativeMethods.DSFindNextLogEntry(jobHandle.NativePointer, out returnEvent);
        }

        /// <summary>
        /// Retrieves the specified log entry.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="eventId">The identifier for the event to be retrieved (see remarks)</param>
        /// <param name="returnEvent">[out] Reference to <see cref="DSAPI.DSLOGDETAIL"/> structure to receive the log entry</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// Entries in the log file are numbered sequentially starting from 0. The latest event ID can be obtained through
        /// a call to <see cref="DSAPI.DSGetNewestLogId"/>. When a log is cleared, there always remains a single entry saying when
        /// the log was cleared.
        /// </remarks>
        public static int DSGetLogEntry(DSJOB jobHandle, int eventId, out DSLOGDETAIL returnEvent)
        {
            return NativeMethods.DSGetLogEntry(jobHandle.NativePointer, eventId, out returnEvent);
        }

        /// <summary>
        /// Retrieves the specified log entry, including the message and invocation ID.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="eventId">The identifier for the event to be retrieved (see remarks)</param>
        /// <param name="returnEvent">[out] Reference to <see cref="DSAPI.DSLOGDETAILFULL"/> structure to receive the log entry</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// Entries in the log file are numbered sequentially starting from 0. The latest event ID can be obtained through
        /// a call to <see cref="DSAPI.DSGetNewestLogId"/>. When a log is cleared, there always remains a single entry saying when
        /// the log was cleared.
        /// </remarks>
        public static int DSGetLogEntryFull(DSJOB jobHandle, int eventId, out DSLOGDETAILFULL returnEvent)
        {
            return NativeMethods.DSGetLogEntryFull(jobHandle.NativePointer, eventId, out returnEvent);
        }

        /// <summary>
        /// Retrieves a list of event log IDs for a given job invocation.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="runNumber">Identifies the job invocation run for which event IDs are returned</param>
        /// <param name="filter">Restricts the types of event log entry for which IDs are returned, by default IDs for all entries are returned</param>
        /// <param name="returnEventIds">[out] The returned integer array of event IDs</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// The run number for a job invocation is reset when the job is compiled, thus it is not possible
        /// to use this method to retrieve job event IDs for runs that occurred prior to the most recent job compilation.
        /// </remarks>
        public static int DSGetLogEventIds(DSJOB jobHandle, int runNumber, string filter, out int[] returnEventIds)
        {
            /* native string list pointer */
            IntPtr pointer;

            int status = NativeMethods.DSGetLogEventIds(jobHandle.NativePointer, runNumber, filter, out pointer);
            if (status == (int)ErrorCodes.DSJE_NOERROR && pointer != IntPtr.Zero)
            {
                /* convert '\' delimited string list of IDs into integer array */
                string strList = Utilities.ExtractStringFromIntPtr(pointer);
                string[] sarray = strList.Split('\\');

                returnEventIds = Array.ConvertAll(sarray, int.Parse);
            }
            else
            {
                /* return empty array for IDs */
                returnEventIds = new int[0];
            }

            return status;
        }

        /// <summary>
        /// Retrieves the newest entry in the log.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="eventType">One of the <see cref="DSAPI.LogEventType"/> values indicating the type of information to be returned</param>
        /// <returns>
        /// If the function succeeds, the return value is the positive identifier of the most recent entry of the requested type in the job log file.
        /// If the function fails, the return value is -1. Use <see cref="DSAPI.DSGetLastError"/> to retrieve the error code.
        /// </returns>
        /// <remarks>
        /// Use this function to determine the ID of the latest entry in a log file before starting a job run.
        /// After the job has started or finished, it is then possible to determine which entries have been added by the job run.
        /// </remarks>
        public static int DSGetNewestLogId(DSJOB jobHandle, LogEventType eventType)
        {
            return NativeMethods.DSGetNewestLogId(jobHandle.NativePointer, (int)eventType);
        }

        /// <summary>
        /// Adds a new entry to the log.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="eventType">One of the <see cref="LogEventType"/> values indicating the type of the event log entry</param>
        /// <param name="message">A string specifying the event message to be logged</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// Messages that contain more that one line of text should contain a newline character to indicate the end of a line.
        /// </remarks>
        public static int DSLogEvent(DSJOB jobHandle, LogEventType eventType, string message)
        {
            return NativeMethods.DSLogEvent(jobHandle.NativePointer, (int)eventType, null, message);
        }

        /// <summary>
        /// Purges the log entries of a job log and status entries.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="purgeSpec">The purge specification. The value can be the following:
        /// 0 = Use auto-purge setting defined in the project.
        /// Negative value = number of days.
        /// Positive value = number of runs.</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSPurgeJob(DSJOB jobHandle, int purgeSpec)
        {
            return NativeMethods.DSPurgeJob(jobHandle.NativePointer, purgeSpec);
        }

        #endregion

        #region APIs for Administering projects and jobs

        /// <summary>
        /// Adds a new environment variable to the user-defined category.
        /// </summary>
        /// <param name="projectHandle">The value returned from <see cref="DSAPI.DSOpenProject"/></param>
        /// <param name="variable">The name of an environment variable to add</param>
        /// <param name="type">A string specifying the environment variable type</param>
        /// <param name="prompt">A string specifying the prompt text</param>
        /// <param name="value">A value to assign to the variable</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSAddEnvVar(DSPROJECT projectHandle, string variable, string type, string prompt, string value)
        {
            return NativeMethods.DSAddEnvVar(projectHandle.NativePointer, variable, type, prompt, value);
        }

        /// <summary>
        /// Add a new DataStage project.
        /// </summary>
        /// <param name="projectName">The name of the project to create</param>
        /// <param name="projectLocation">A string specifying the project location.
        /// To create a project in the default project directory, this argument can be null or empty</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// The user who runs the code that contains this function must be an InfoSphere® DataStage® administrator.
        /// </remarks>
        public static int DSAddProject(string projectName, string projectLocation)
        {
            return NativeMethods.DSAddProject(projectName, projectLocation);
        }

        /// <summary>
        /// Delete a user-defined environment variable in a specified project.
        /// </summary>
        /// <param name="projectHandle">The value returned from <see cref="DSAPI.DSOpenProject"/></param>
        /// <param name="variable">The name of an environment variable</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSDeleteEnvVar(DSPROJECT projectHandle, string variable)
        {
            return NativeMethods.DSDeleteEnvVar(projectHandle.NativePointer, variable);
        }

        /// <summary>
        /// Delete an existing DataStage project.
        /// </summary>
        /// <param name="projectName">A string specifying the name of the project to be deleted</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        /// <remarks>
        /// The user who runs the code that contains this function must be an InfoSphere® DataStage® administrator.
        /// Any jobs that are scheduled to be run that are included in this project are also deleted.
        /// </remarks>
        public static int DSDeleteProject(string projectName)
        {
            return NativeMethods.DSDeleteProject(projectName);
        }

        /// <summary>
        /// List environment variables.
        /// </summary>
        /// <param name="projectHandle">The value returned from <see cref="DSAPI.DSOpenProject"/></param>
        /// <returns>
        /// If the function succeeds, the return value is a string list of environment variables.
        /// If the function fails, the return value is null.
        /// Use <see cref="DSAPI.DSGetLastError"/> to retrieve the error code..
        /// </returns>
        public static List<string> DSListEnvVars(DSPROJECT projectHandle)
        {
            IntPtr pointer = NativeMethods.DSListEnvVars(projectHandle.NativePointer);
            return Utilities.ExtractStringListFromIntPtr(pointer);
        }

        /// <summary>
        /// List the properties of a project.
        /// </summary>
        /// <param name="projectHandle">The value returned from <see cref="DSAPI.DSOpenProject"/></param>
        /// <returns>
        /// If the function succeeds, the return value is a string list of project properties.
        /// If the function fails, the return value is null.
        /// Use <see cref="DSAPI.DSGetLastError"/> to retrieve the error code.
        /// </returns>
        public static List<string> DSListProjectProperties(DSPROJECT projectHandle)
        {
            IntPtr pointer = NativeMethods.DSListProjectProperties(projectHandle.NativePointer);
            return Utilities.ExtractStringListFromIntPtr(pointer);
        }

        /// <summary>
        /// Set an environment variable.
        /// </summary>
        /// <param name="projectHandle">The value returned from <see cref="DSAPI.DSOpenProject"/></param>
        /// <param name="variable">The name of an environment variable</param>
        /// <param name="value">A value to assign to the variable</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSSetEnvVar(DSPROJECT projectHandle, string variable, string value)
        {
            return NativeMethods.DSSetEnvVar(projectHandle.NativePointer, variable, value);
        }

        /// <summary>
        /// Set a property for a project.
        /// </summary>
        /// <param name="projectHandle">The value returned from <see cref="DSAPI.DSOpenProject"/></param>
        /// <param name="property">A string specifying the project property to set</param>
        /// <param name="value">A value to assign to the property</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSSetProjectProperty(DSPROJECT projectHandle, string property, string value)
        {
            return NativeMethods.DSSetProjectProperty(projectHandle.NativePointer, property, value);
        }

        #endregion

        #region APIs for Handling errors

        /// <summary>
        /// Retrieves the last error code value generated by the calling thread.
        /// </summary>
        /// <returns>The return value is the last <see cref="DSAPI.ErrorCodes"/> error code, or 0 (DSJE_NOERROR) if there is no error.</returns>
        public static int DSGetLastError()
        {
            return NativeMethods.DSGetLastError();
        }

        /// <summary>
        /// Retrieves the text of the last reported error from the engine.
        /// </summary>
        /// <param name="nativeProjectHandle">The native project handle returned by a previous call to the <see cref="DSAPI.DSOpenProject"/> function, or IntPtr.Zero</param>
        /// <returns>The return value is a string list containing the last error message, or null if there is no error.</returns>
        public static List<string> DSGetLastErrorMsg(IntPtr nativeProjectHandle)
        {
            //TODO maybe change to use DSPROJECT with a constant for Null or Zero
            IntPtr pointer = NativeMethods.DSGetLastErrorMsg(nativeProjectHandle);
            return Utilities.ExtractStringListFromIntPtr(pointer);
        }

        #endregion

        #region APIS for working with Workload Manager (WLM) queues

        /// <summary>
        /// Get the list of WLM queues.
        /// </summary>
        /// <returns>
        /// If the function succeeds, the return value is a string list of queue names.
        /// If the function fails, the return value is null.
        /// Use <see cref="DSAPI.DSGetLastError"/> to retrieve the error code.
        /// </returns>
        public static List<string> DSGetQueueList()
        {
            IntPtr pointer = NativeMethods.DSGetQueueList();
            return Utilities.ExtractStringListFromIntPtr(pointer);
        }

        /// <summary>
        /// Determines the WLM status.
        /// </summary>
        /// <returns>The return value is 0 (disabled), 1 (enabled but not running), or 2 (enabled and running).</returns>
        // TODO raise issue for lack of error handling, any error would simply return 0
        public static int DSGetWLMEnabled()
        {
            return NativeMethods.DSGetWLMEnabled();
        }

        /// <summary>
        /// Set the WLM queue name for a job run.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="queueName">A string specifying the name of the queue to set</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSSetJobQueue(DSJOB jobHandle, string queueName)
        {
            return NativeMethods.DSSetJobQueue(jobHandle.NativePointer, queueName);
        }

        #endregion

        #region APIs for reporting, repository usage and data lineage

        /// <summary>
        /// List design-time objects in the repository.
        /// </summary>
        /// <param name="projectHandle">The value returned from <see cref="DSAPI.DSOpenProject"/></param>
        /// <param name="objectType">Must currently be set to <see cref="DSAPI.ReposInfoObjectType.DSS_JOBS"/> to indicate that you want to search for jobs</param>
        /// <param name="infoType">One of the <see cref="DSAPI.DSGetReposInfo"/> values indicating the type of information to be returned</param>
        /// <param name="searchCriteria">Search criteria using wildcards. Can be null or empty to return all items</param>
        /// <param name="startingCategory">The category to start the search. Can be null or empty to search from the root category</param>
        /// <param name="subCategories">true to search sub categories; otherwise, false</param>
        /// <param name="returnInfo">[out] Reference to <see cref="DSAPI.DSREPOSINFO"/> structure to receive the information</param>
        /// <returns>
        /// If the function succeeds, the return value is the number of objects that have been found.
        /// A return value of 0 means no objects were returned.
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSGetReposInfo(DSPROJECT projectHandle, ReposInfoObjectType objectType, ReposInfoType infoType,
            string searchCriteria, string startingCategory, bool subCategories, out DSREPOSINFO returnInfo)
        {
            /* return repository objects */
            return NativeMethods.DSGetReposInfo(projectHandle.NativePointer, (int)objectType, (int)infoType,
                searchCriteria, startingCategory, subCategories ? 1 : 0, out returnInfo);
        }

        /// <summary>
        /// List repository objects based on required relationships.
        /// </summary>
        /// <param name="projectHandle">The value returned from <see cref="DSAPI.DSOpenProject"/></param>
        /// <param name="relationshipType">Key indicating the type of information to be returned</param>
        /// <param name="objectName">Specifies the job or table name, and varies according to which relationship type is specified</param>
        /// <param name="recursive">Used by the DSS_USES_JOB and DSS_USEDBY_JOB relationships. When true, for job found that uses
        ///                         objectName, the jobs which that job is used in are found and so on. For all other relationship
        ///                         types the parameter is ignored.</param>
        /// <param name="returnInfo">Reference to <see cref="DSAPI.DSREPOSUSAGE"/> structure to receive the information</param>
        /// <returns>
        /// If the function succeeds, the return value is the number of objects that have been found.
        /// A return value of 0 means no objects were returned.
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSGetReposUsage(DSPROJECT projectHandle, ReposUsageType relationshipType, string objectName,
            bool recursive, out DSREPOSUSAGE returnInfo)
        {
            /* return repository objects */
            return NativeMethods.DSGetReposUsage(projectHandle.NativePointer, (int)relationshipType, objectName,
                recursive ? 1 : 0, out returnInfo);
        }

        /// <summary>
        /// Generates a report describing the complete status of an attached job.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="reportType">One of the <see cref="DSAPI.ReportInfoType"/> values indicating the type of report to be generated</param>
        /// <param name="lineSeparator">Specifies line termination used in the report. Special values are:
        /// "CRLF" => CHAR(13):CHAR(10)
        /// "LF"   => CHAR(10)
        /// "CR"   => CHAR(13)
        /// The default is CRLF on Windows; otherwise LF.</param>
        /// <param name="returnInfo">Reference to the retun<see cref="DSAPI.DSREPORTINFO"/> structure for the generated report</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSMakeJobReport(DSJOB jobHandle, ReportInfoType reportType, string lineSeparator, out DSREPORTINFO returnInfo)
        {
            return NativeMethods.DSMakeJobReport(jobHandle.NativePointer, (int)reportType, lineSeparator, out returnInfo);
        }

        /// <summary>
        /// Sets whether operational metadata is generated in a job run.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="value">true to generate operational metadata; otherwise, false</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSSetGenerateOpMetaData(DSJOB jobHandle, bool value)
        {
            return NativeMethods.DSSetGenerateOpMetaData(jobHandle.NativePointer, value ? 1 : 0);
        }

        #endregion

        #region APIS for working with user-defined job IDs

        /// <summary>
        /// Get the user-defined ID for a job.
        /// </summary>
        /// <param name="projectHandle">The value returned from <see cref="DSAPI.DSOpenProject"/></param>
        /// <param name="jobName">A string specifying the name of the job of which to return the ID</param>
        /// <returns>
        /// If the function succeeds, the return value is a string containing the ID.
        /// If the function fails, the return value is null.
        /// Use <see cref="DSAPI.DSGetLastError"/> to retrieve the error code.
        /// </returns>
        public static string DSGetIdForJob(DSPROJECT projectHandle, string jobName)
        {
            IntPtr pointer = NativeMethods.DSGetIdForJob(projectHandle.NativePointer, jobName);
            return Utilities.ExtractStringFromIntPtr(pointer);
        }

        /// <summary>
        /// Get the job name from the user-defined ID.
        /// </summary>
        /// <param name="projectHandle">The value returned from <see cref="DSAPI.DSOpenProject"/></param>
        /// <param name="jobId">A string specifying the name of the ID</param>
        /// <returns>
        /// If the function succeeds, the return value is string containing the job name.
        /// If the function fails, the return value is null.
        /// Use <see cref="DSAPI.DSGetLastError"/> to retrieve the error code.
        /// </returns>
        public static string DSJobNameFromJobId(DSPROJECT projectHandle, string jobId)
        {
            IntPtr pointer = NativeMethods.DSJobNameFromJobId(projectHandle.NativePointer, jobId);
            return Utilities.ExtractStringFromIntPtr(pointer);
        }

        /// <summary>
        /// Sets a user-defined ID for a specified job.
        /// </summary>
        /// <param name="projectHandle">The value returned from <see cref="DSAPI.DSOpenProject"/></param>
        /// <param name="jobName">A string specifying the name of the job in which to set the user-defined ID</param>
        /// <param name="jobId">A string specifying the name of the ID</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSSetIdForJob(DSPROJECT projectHandle, string jobName, string jobId)
        {
            return NativeMethods.DSSetIdForJob(projectHandle.NativePointer, jobName, jobId);
        }

        #endregion

        #region APIs for working with Message Handlers

        /// <summary>
        /// Sets whether to disable job-level message handling.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="value">true to disable job-level message handling; otherwise, false</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSSetDisableJobHandler(DSJOB jobHandle, bool value)
        {
            return NativeMethods.DSSetDisableJobHandler(jobHandle.NativePointer, value ? 1 : 0);
        }

        /// <summary>
        /// Sets whether to disable project-level message handling.
        /// </summary>
        /// <param name="jobHandle">The value returned from <see cref="DSAPI.DSOpenJob"/></param>
        /// <param name="value">true to disable project-level message handling; otherwise, false</param>
        /// <returns>
        /// If the function succeeds, the return value is 0 (DSJE_NOERROR).
        /// If the function fails, the return value is one of the <see cref="DSAPI.ErrorCodes"/> error codes.
        /// </returns>
        public static int DSSetDisableProjectHandler(DSJOB jobHandle, bool value)
        {
            return NativeMethods.DSSetDisableProjectHandler(jobHandle.NativePointer, value ? 1 : 0);
        }

        #endregion
    }
}
