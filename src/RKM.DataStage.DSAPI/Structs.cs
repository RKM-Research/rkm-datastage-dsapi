/*
 * RKM.DataStage.DSAPI
 * Low-level .NET wrapper for the IBM DataStage DSAPI
 *
 * Copyright © 2022–2026 Richard K. Morris / RKM Research Ltd
 * All rights reserved
 */

using System;
using System.Runtime.InteropServices;

namespace RKM.DataStage
{
    public static partial class DSAPI
    {
        /// <summary>
        /// Job information data structure
        /// </summary>
        /// <see cref="DSAPI.DSGetJobInfo"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSJOBINFO
        {
            /// <summary>
            /// Key indicating type of information
            /// </summary>
            public int infoType;

            /// <summary>
            /// Information value
            /// </summary>
            public INFO_UNION info;

            /// <summary>
            /// Union structure
            /// </summary>
            [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
            public struct INFO_UNION
            {
                /// <summary>
                /// Current status of job
                /// </summary>
                [FieldOffset(0)] public int jobStatus;

                /// <summary>
                /// Name of controlling job
                /// </summary>
                [FieldOffset(0)] public IntPtr jobController;

                /// <summary>
                /// Date and time when job started
                /// </summary>
                [FieldOffset(0)] public long jobStartTime;

                /// <summary>
                /// Wave number of current or last run
                /// </summary>
                [FieldOffset(0)] public int jobWaveNumber;

                /// <summary>
                /// Last set user status
                /// </summary>
                [FieldOffset(0)] public IntPtr userStatus;

                /// <summary>
                /// List of stage names in job
                /// </summary>
                [FieldOffset(0)] public IntPtr stageList;

                /// <summary>
                /// List of job parameter names
                /// </summary>
                [FieldOffset(0)] public IntPtr paramList;

                /// <summary>
                /// Name of this job
                /// </summary>
                [FieldOffset(0)] public IntPtr jobName;

                /// <summary>
                /// Current job control status
                /// </summary>
                [FieldOffset(0)] public int jobControl;

                /// <summary>
                /// Job process ID
                /// </summary>
                [FieldOffset(0)] public int jobPid;

                /// <summary>
                /// Date and time when job finished
                /// </summary>
                [FieldOffset(0)] public long jobLastTime;

                /// <summary>
                /// List of job invocation ids
                /// </summary>
                [FieldOffset(0)] public IntPtr jobInvocations;

                /// <summary>
                /// Interim status of job
                /// </summary>
                [FieldOffset(0)] public int jobInterimStatus;

                /// <summary>
                /// Job invocation ID
                /// </summary>
                [FieldOffset(0)] public IntPtr jobInvocationId;

                /// <summary>
                /// Job description
                /// </summary>
                [FieldOffset(0)] public IntPtr jobDesc;

                /// <summary>
                /// List of stages not in DSJ.STAGELIST
                /// </summary>
                [FieldOffset(0)] public IntPtr stageList2;

                /// <summary>
                /// Job elapsed time in seconds
                /// </summary>
                [FieldOffset(0)] public int jobElapsed;

                /// <summary>
                /// Job is a DMI (aka WEB) service
                /// </summary>
                [FieldOffset(0)] public int jobDMIService;

                /// <summary>
                /// Job can be invoked multiple times
                /// </summary>
                [FieldOffset(0)] public int jobMultiInvokable;

                /// <summary>
                /// Full job description
                /// </summary>
                [FieldOffset(0)] public IntPtr jobFullDesc;

                /// <summary>
                /// Job is restartable
                /// </summary>
                [FieldOffset(0)] public int jobRestartable;
            }
        }

        /// <summary>
        /// Log detail information data structure
        /// </summary>
        /// <see cref="DSAPI.DSGetLogEntry"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSLOGDETAIL
        {
            /// <summary>
            /// Id of this event
            /// </summary>
            public int eventId;

            /// <summary>
            /// Date and time event occurred
            /// </summary>
            public long timestamp;

            /// <summary>
            /// Type of event
            /// </summary>
            public int type;

            /// <summary>
            /// User name associated with event
            /// </summary>
            public IntPtr username;

            /// <summary>
            /// Full description of event
            /// </summary>
            public IntPtr fullMessage;
        }

        /// <summary>
        /// Full log detail information data structure
        /// </summary>
        /// <see cref="DSAPI.DSGetLogEntryFull"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSLOGDETAILFULL
        {
            /// <summary>
            /// Id of this event
            /// </summary>
            public int eventId;

            /// <summary>
            /// Date and time event occurred
            /// </summary>
            public long timestamp;

            /// <summary>
            /// Type of event
            /// </summary>
            public int type;

            /// <summary>
            /// User name associated with event
            /// </summary>
            public IntPtr username;

            /// <summary>
            /// Full description of event
            /// </summary>
            public IntPtr fullMessage;

            /// <summary>
            /// Message ID
            /// </summary>
            public IntPtr messageId;

            /// <summary>
            /// Invocation ID, if any
            /// </summary>
            public IntPtr invocationId;
        }

        /// <summary>
        /// Log summary information data structure
        /// </summary>
        /// <see cref="DSAPI.DSFindFirstLogEntry"/>
        /// <see cref="DSAPI.DSFindNextLogEntry"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSLOGEVENT
        {
            /// <summary>
            /// Id of this event
            /// </summary>
            public int eventId;

            /// <summary>
            /// Date and time event occurred
            /// </summary>
            public long timestamp;

            /// <summary>
            /// Type of event
            /// </summary>
            public int type;

            /// <summary>
            /// Short event message
            /// </summary>
            public IntPtr message;
        }

        /// <summary>
        /// Job parameter data structure
        /// </summary>
        /// <see cref="DSAPI.DSSetParam"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSPARAM
        {
            /// <summary>
            /// Type of parameter
            /// </summary>
            public int paramType;

            /// <summary>
            /// Parameter value
            /// </summary>
            public PARAM_UNION paramValue;

            /// <summary>
            /// Parameter union structure
            /// </summary>
            [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
            public struct PARAM_UNION
            {
                /// <summary>
                /// String type
                /// </summary>
                [FieldOffset(0)] public IntPtr pString;
                /// <summary>
                /// Encrypted string
                /// </summary>
                [FieldOffset(0)] public IntPtr pEncrypt;
                /// <summary>
                /// Integer
                /// </summary>
                [FieldOffset(0)] public int pInt;
                /// <summary>
                /// Float
                /// </summary>
                [FieldOffset(0)] public float pFloat;
                /// <summary>
                /// Pathname
                /// </summary>
                [FieldOffset(0)] public IntPtr pPath;
                /// <summary>
                /// String from a list
                /// </summary>
                [FieldOffset(0)] public IntPtr pListValue;
                /// <summary>
                /// Date string
                /// </summary>
                [FieldOffset(0)] public IntPtr pDate;
                /// <summary>
                /// Time string
                /// </summary>
                [FieldOffset(0)] public IntPtr pTime;
            }
        }

        /// <summary>
        /// Job parameter information data structure
        /// </summary>
        /// <see cref="DSAPI.DSGetParamInfo"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSPARAMINFO
        {
            /// <summary>
            /// Default value of parameter
            /// </summary>
            public DSPARAM defaultValue;

            /// <summary>
            /// Description of parameter
            /// </summary>
            public IntPtr helpText;

            /// <summary>
            /// Prompt for the parameter
            /// </summary>
            public IntPtr paramPrompt;

            /// <summary>
            /// Type of parameter
            /// </summary>
            public int paramType;

            /// <summary>
            /// Designer-set default value of parameter
            /// </summary>
            public DSPARAM desDefaultValue;

            /// <summary>
            /// Set of valid strings for List type parameter
            /// </summary>
            public IntPtr listValues;

            /// <summary>
            /// Set of designer-set valid strings for list type parameter
            /// </summary>
            public IntPtr desListValues;

            /// <summary>
            /// Flag indicating whether a prompt is required at run time
            /// </summary>
            public int promptAtRun;
        }

        /// <summary>
        /// Project information data structure
        /// </summary>
        /// <see cref="DSAPI.DSGetProjectInfo"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSPROJECTINFO
        {
            /// <summary>
            /// Key indicating type of information
            /// </summary>
            public int infoType;

            /// <summary>
            /// Information value
            /// </summary>
            public INFO_UNION info;

            /// <summary>
            /// Project information union structure
            /// </summary>
            [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
            public struct INFO_UNION
            {
                /// <summary>
                /// List of names of all jobs in the project
                /// </summary>
                [FieldOffset(0)] public IntPtr jobList;
                /// <summary>
                /// Name of the current project
                /// </summary>
                [FieldOffset(0)] public IntPtr projectName;
                /// <summary>
                /// Directory path of the current project
                /// </summary>
                [FieldOffset(0)] public IntPtr projectPath;
                /// <summary>
                /// Host name of the server
                /// </summary>
                [FieldOffset(0)] public IntPtr hostName;
                /// <summary>
                /// Install tag of current server engine
                /// </summary>
                [FieldOffset(0)] public IntPtr installTag;
                /// <summary>
                /// TCP port of current server engine
                /// </summary>
                [FieldOffset(0)] public IntPtr tcpPort;
            }
        }

        /// <summary>
        /// Stage information data structure
        /// </summary>
        /// <see cref="DSAPI.DSGetStageInfo"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSSTAGEINFO
        {
            /// <summary>
            /// Key indicating type of information
            /// </summary>
            public int infoType;

            /// <summary>
            /// Information value
            /// </summary>
            public INFO_UNION info;

            /// <summary>
            /// Stage information union structure
            /// </summary>
            [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
            public struct INFO_UNION
            {
                /// <summary>
                /// Last error message (log) from a link
                /// </summary>
                [FieldOffset(0)] public DSLOGDETAIL lastError;
                /// <summary>
                /// Name of stage type
                /// </summary>
                [FieldOffset(0)] public IntPtr typeName;
                /// <summary>
                /// Primary link input row number
                /// </summary>
                [FieldOffset(0)] public int inRowNum;
                /// <summary>
                /// List of stage link names
                /// </summary>
                [FieldOffset(0)] public IntPtr linkList;
                /// <summary>
                /// Name of current stage
                /// </summary>
                [FieldOffset(0)] public IntPtr stageName;
                /// <summary>
                /// List of stage variable names
                /// </summary>
                [FieldOffset(0)] public IntPtr varList;
                /// <summary>
                /// Date and time when stage started
                /// </summary>
                [FieldOffset(0)] public long stageStartTime;
                /// <summary>
                /// Date and time when stage finished
                /// </summary>
                [FieldOffset(0)] public long stageEndTime;
                /// <summary>
                /// List of link types
                /// </summary>
                [FieldOffset(0)] public IntPtr linkTypes;
                /// <summary>
                /// Stage description
                /// </summary>
                [FieldOffset(0)] public IntPtr stageDesc;
                /// <summary>
                /// List of instance IDs (parallel jobs)
                /// </summary>
                [FieldOffset(0)] public IntPtr instList;
                /// <summary>
                /// List of CPU time in seconds
                /// </summary>
                [FieldOffset(0)] public IntPtr cpuList;
                /// <summary>
                /// List of elapsed time in seconds
                /// </summary>
                [FieldOffset(0)] public IntPtr stageElapsed;
                /// <summary>
                /// List of process IDs
                /// </summary>
                [FieldOffset(0)] public IntPtr pidList;
                /// <summary>
                /// Stage status
                /// </summary>
                [FieldOffset(0)] public int stageStatus;
                /// <summary>
                /// List of custom info names
                /// </summary>
                [FieldOffset(0)] public IntPtr custInfoList;
            }
        }

        /// <summary>
        /// Link information data structure
        /// </summary>
        /// <see cref="DSAPI.DSGetLinkInfo"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSLINKINFO
        {
            /// <summary>
            /// Key indicating type of information
            /// </summary>
            public int infoType;

            /// <summary>
            /// Information value
            /// </summary>
            public INFO_UNION info;

            /// <summary>
            /// Link information union structure
            /// </summary>
            [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
            public struct INFO_UNION
            {
                /// <summary>
                /// Last error message from link
                /// </summary>
                [FieldOffset(0)] public DSLOGDETAIL lastError;
                /// <summary>
                /// Number of rows that have passed down link
                /// </summary>
                [FieldOffset(0)] public int rowCount;
                /// <summary>
                /// Name of this link
                /// </summary>
                [FieldOffset(0)] public IntPtr linkName;
                /// <summary>
                /// SQL state for last error
                /// </summary>
                [FieldOffset(0)] public IntPtr linkSQLState;
                /// <summary>
                /// DBMS code for last error
                /// </summary>
                [FieldOffset(0)] public IntPtr linkDBMSCode;
                /// <summary>
                /// Link description
                /// </summary>
                [FieldOffset(0)] public IntPtr linkDesc;
                /// <summary>
                /// Name of stage at other end of the link
                /// </summary>
                [FieldOffset(0)] public IntPtr linkedStage;
                /// <summary>
                /// List of row counts
                /// </summary>
                [FieldOffset(0)] public IntPtr rowCountList;
            }
        }

        /// <summary>
        /// Stage variable information data structure
        /// </summary>
        /// <see cref="DSAPI.DSGetVarInfo"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSVARINFO
        {
            /// <summary>
            /// Key indicating type of information
            /// </summary>
            public int infoType;

            /// <summary>
            /// Information value
            /// </summary>
            public INFO_UNION info;

            /// <summary>
            /// Union structure
            /// </summary>
            [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
            public struct INFO_UNION
            {
                /// <summary>
                /// Stage variable value
                /// </summary>
                [FieldOffset(0)] public IntPtr varValue;
                /// <summary>
                /// Stage variable description
                /// </summary>
                [FieldOffset(0)] public IntPtr varDesc;
            }
        }

        /// <summary>
        /// Custom information data structure
        /// </summary>
        /// <see cref="DSAPI.DSGetCustInfo"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSCUSTINFO
        {
            /// <summary>
            /// Key indicating type of information
            /// </summary>
            public int infoType;

            /// <summary>
            /// Information value
            /// </summary>
            public INFO_UNION info;

            /// <summary>
            /// Union structure
            /// </summary>
            [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
            public struct INFO_UNION
            {
                /// <summary>
                /// Custom info value
                /// </summary>
                [FieldOffset(0)] public IntPtr custInfoValue;
                /// <summary>
                /// Custom info description
                /// </summary>
                [FieldOffset(0)] public IntPtr custInfoDesc;
            }
        }

        /// <summary>
        /// Job report information data structure
        /// </summary>
        /// <see cref="DSAPI.DSMakeJobReport"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSREPORTINFO
        {
            /// <summary>
            /// Key indicating type of information
            /// </summary>
            public int reportType;

            /// <summary>
            /// Information value
            /// </summary>
            public INFO_UNION info;

            /// <summary>
            /// Union structure
            /// </summary>
            [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
            public struct INFO_UNION
            {
                /// <summary>
                /// Report text
                /// </summary>
                [FieldOffset(0)] public IntPtr reportText;
            }
        }

        /// <summary>
        /// Repository job information data structure
        /// </summary>
        /// <see cref="DSAPI.DSGetReposInfo"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSREPOSJOBINFO
        {
            /// <summary>
            /// Includes category
            /// </summary>
            public IntPtr jobname;
            /// <summary>
            /// InfoType constant
            /// </summary>
            public int jobtype;
            /// <summary>
            /// Link to next job
            /// </summary>
            public IntPtr nextjob;
        }

        /// <summary>
        /// Repository object information data structure
        /// </summary>
        /// <see cref="DSAPI.DSGetReposInfo"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSREPOSINFO
        {
            /// <summary>
            /// Key indicating type of information
            /// </summary>
            public int infoType;

            /// <summary>
            /// Information value
            /// </summary>
            public INFO_UNION info;

            /// <summary>
            /// Union structure
            /// </summary>
            [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
            public struct INFO_UNION
            {
                /// <summary>
                /// linked list of found jobs
                /// </summary>
                [FieldOffset(0)] public IntPtr jobs;
            }
        }

        /// <summary>
        /// Repository usage job info data structure
        /// </summary>
        /// <see cref="DSAPI.DSGetReposUsage"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSREPOSUSAGEJOB
        {
            /// <summary>
            /// Job and category name
            /// </summary>
            public IntPtr jobname;

            /// <summary>
            /// type of job
            /// </summary>
            public int jobtype;

            /// <summary>
            /// next job in linked list of jobs at same level
            /// </summary>
            public IntPtr nextjob;

            /// <summary>
            /// first child job in linked list
            /// </summary>
            public IntPtr childjob;
        }

        /// <summary>
        /// Repository usage information data structure
        /// </summary>
        /// <see cref="DSAPI.DSGetReposUsage"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DSREPOSUSAGE
        {
            /// <summary>
            /// Key indicating type of information
            /// </summary>
            public int infoType;

            /// <summary>
            /// Information value
            /// </summary>
            public INFO_UNION info;

            /// <summary>
            /// Union structure
            /// </summary>
            [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
            public struct INFO_UNION
            {
                /// <summary>
                /// Linked list of jobs
                /// </summary>
                [FieldOffset(0)] public IntPtr jobs;
            }
        }

        /// <summary>
        /// Represents a handle to a project
        /// </summary>
        public struct DSPROJECT
        {
            /// <summary>
            /// Creates a project handle wrapper for an existing native DataStage project handle.
            /// </summary>
            /// <param name="projectHandle">The underlying native project handle.</param>
            public DSPROJECT(IntPtr projectHandle)
            {
                this.pointer = projectHandle;
            }

            // Native handle
            private readonly IntPtr pointer;

            /// <summary>
            /// Gets the underlying native project handle
            /// </summary>
            public IntPtr NativePointer
            {
                get { return pointer; }
            }

            /// <summary>
            /// Gets whether the stored project handle is non-zero
            /// </summary>
            public bool HasValue
            {
                get { return pointer != IntPtr.Zero; }
            }
        }

        /// <summary>
        /// Represents a handle to a job
        /// </summary>
        public struct DSJOB
        {
            internal DSJOB(IntPtr jobHandle)
            {
                this.pointer = jobHandle;
            }

            // Native handle
            private readonly IntPtr pointer;

            /// <summary>
            /// Gets the underlying native job handle
            /// </summary>
            public IntPtr NativePointer
            {
                get { return pointer; }
            }

            /// <summary>
            /// Gets whether the stored job handle is non-zero
            /// </summary>
            public bool HasValue
            {
                get { return pointer != IntPtr.Zero; }
            }
        }

    }
}
