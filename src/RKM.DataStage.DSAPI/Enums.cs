/*
 * RKM.DataStage.DSAPI
 * Low-level .NET wrapper for the IBM DataStage DSAPI
 *
 * Copyright © 2022–2026 Richard K. Morris / RKM Research
 */

namespace RKM.DataStage
{
    public static partial class DSAPI
    {
        /// <summary>
        /// DSJOBINFO infoType values.
        /// </summary>
        /// <see cref="DSAPI.DSGetJobInfo"/>
        public enum JobInfoType
        {
            /// <summary>
            /// Current status of the job.
            /// </summary>
            DSJ_JOBSTATUS = 1,

            /// <summary>
            /// Name of the job referenced by JobHandle.
            /// </summary>
            DSJ_JOBNAME = 2,

            /// <summary>
            /// Name of job controlling the job referenced by JobHandle.
            /// </summary>
            DSJ_JOBCONTROLLER = 3,

            /// <summary>
            /// Date and time when the job started.
            /// </summary>
            DSJ_JOBSTARTTIMESTAMP = 4,

            /// <summary>
            /// Wave number of last or current run.
            /// </summary>
            DSJ_JOBWAVENO = 5,

            /// <summary>
            /// List of job parameter names.
            /// </summary>
            DSJ_PARAMLIST = 6,

            /// <summary>
            /// List of stage names in the job.
            /// </summary>
            DSJ_STAGELIST = 7,

            /// <summary>
            /// User status set by the job, if any.
            /// </summary>
            DSJ_USERSTATUS = 8,

            /// <summary>
            /// Job control state: stop, suspend or resume.
            /// </summary>
            DSJ_JOBCONTROL = 9,

            /// <summary>
            /// Process ID of the DSD.RUN process.
            /// </summary>
            DSJ_JOBPID = 10,

            /// <summary>
            /// Server date and time when the job last finished (YYYY-MM-DD HH:MM:SS).
            /// </summary>
            DSJ_JOBLASTTIMESTAMP = 11,

            /// <summary>
            /// Comma-separated list of job invocation IDs.
            /// </summary>
            DSJ_JOBINVOCATIONS = 12,

            /// <summary>
            /// Current interim job status.
            /// </summary>
            DSJ_JOBINTERIMSTATUS = 13,

            /// <summary>
            /// Invocation name of the referenced job.
            /// </summary>
            DSJ_JOBINVOCATIONID = 14,

            /// <summary>
            /// Job description.
            /// </summary>
            DSJ_JOBDESC = 15,

            /// <summary>
            /// List of stages not in DSJ_STAGELIST.
            /// </summary>
            DSJ_STAGELIST2 = 16,

            /// <summary>
            /// Elapsed job time in seconds.
            /// </summary>
            DSJ_JOBELAPSED = 17,

            /// <summary>
            /// Job EOT count.
            /// </summary>
            DSJ_JOBEOTCOUNT = 18,

            /// <summary>
            /// Job EOT timestamp.
            /// </summary>
            DSJ_JOBEOTTIMESTAMP = 19,

            /// <summary>
            /// Whether the job is a DMI (web) service.
            /// </summary>
            DSJ_JOBDMISERVICE = 20,

            /// <summary>
            /// Whether the job supports multiple invocations.
            /// </summary>
            DSJ_JOBMULTIINVOKABLE = 21,

            /// <summary>
            /// Full job description.
            /// </summary>
            DSJ_JOBFULLDESC = 22,

            /// <summary>
            /// Whether the job can be restarted.
            /// </summary>
            DSJ_JOBRESTARTABLE = 24
        }

        /// <summary>
        /// DSJOBINFO jobStatus values.
        /// </summary>
        /// <see cref="DSAPI.DSGetJobInfo"/>
        public enum JobStatus
        {
            /// <summary>
            /// Job running.
            /// </summary>
            DSJS_RUNNING = 0,

            /// <summary>
            /// Job finished a normal run with no warnings.
            /// </summary>
            DSJS_RUNOK = 1,

            /// <summary>
            /// Job finished a normal run with warnings.
            /// </summary>
            DSJS_RUNWARN = 2,

            /// <summary>
            /// Job finished a normal run with a fatal error.
            /// </summary>
            DSJS_RUNFAILED = 3,

            /// <summary>
            /// Job queued waiting for resource allocation.
            /// </summary>
            DSJS_QUEUED = 4,

            /// <summary>
            /// Job finished a validation run with no warnings.
            /// </summary>
            DSJS_VALOK = 11,

            /// <summary>
            /// Job finished a validation run with warnings.
            /// </summary>
            DSJS_VALWARN = 12,

            /// <summary>
            /// Job failed a validation run.
            /// </summary>
            DSJS_VALFAILED = 13,

            /// <summary>
            /// Job finished a reset run.
            /// </summary>
            DSJS_RESET = 21,

            /// <summary>
            /// Job was stopped by some indeterminate action.
            /// </summary>
            DSJS_CRASHED = 96,

            /// <summary>
            /// Job was stopped by operator intervention (can't tell run type).
            /// </summary>
            DSJS_STOPPED = 97,

            /// <summary>
            /// Job has not been compiled.
            /// </summary>
            DSJS_NOTRUNNABLE = 98,

            /// <summary>
            /// Any other status.
            /// </summary>
            DSJS_NOTRUNNING = 99
        }

        /// <summary>
        /// DSLOGDETAIL and DSLOGDETAILFULL event type values.
        /// </summary>
        /// <see cref="DSAPI.DSGetLogEntry(DSJOB, int, out DSLOGDETAIL)"/>
        /// <see cref="DSAPI.DSGetLogEntryFull(DSJOB, int, out DSLOGDETAILFULL)"/>
        public enum LogEventType
        {
            /// <summary>
            /// Information message.
            /// </summary>
            DSJ_LOGINFO = 1,

            /// <summary>
            /// Warning message.
            /// </summary>
            DSJ_LOGWARNING = 2,

            /// <summary>
            /// Fatal error message.
            /// </summary>
            DSJ_LOGFATAL = 3,

            /// <summary>
            /// Rejected row message.
            /// </summary>
            DSJ_LOGREJECT = 4,

            /// <summary>
            /// Job started message.
            /// </summary>
            DSJ_LOGSTARTED = 5,

            /// <summary>
            /// Job reset message.
            /// </summary>
            DSJ_LOGRESET = 6,

            /// <summary>
            /// Batch control event.
            /// </summary>
            DSJ_LOGBATCH = 7,

            /// <summary>
            /// Event type not covered by the preceding categories.
            /// </summary>
            DSJ_LOGOTHER = 98,

            /// <summary>
            /// Any event type.
            /// </summary>
            DSJ_LOGANY = 99
        }

        /// <summary>
        /// DSLINKINFO infoType values.
        /// </summary>
        /// <see cref="DSAPI.DSGetLinkInfo"/>
        public enum LinkInfoType
        {
            /// <summary>
            /// Last error message reported by link.
            /// </summary>
            DSJ_LINKLASTERR = 1,

            /// <summary>
            /// Actual link name.
            /// </summary>
            DSJ_LINKNAME = 2,

            /// <summary>
            /// Number of rows that have passed down the link.
            /// </summary>
            DSJ_LINKROWCOUNT = 3,

            /// <summary>
            /// SQLSTATE value from the last error message.
            /// </summary>
            DSJ_LINKSQLSTATE = 4,

            /// <summary>
            /// DBMS code from the last error message.
            /// </summary>
            DSJ_LINKDBMSCODE = 5,

            /// <summary>
            /// Link description.
            /// </summary>
            DSJ_LINKDESC = 6,

            /// <summary>
            /// Stage at the other end of the link.
            /// </summary>
            DSJ_LINKSTAGE = 7,

            /// <summary>
            /// Comma-separated list of row counts for each stage instance.
            /// </summary>
            DSJ_INSTROWCOUNT = 8,

            /// <summary>
            /// Link EOT row count.
            /// </summary>
            DSJ_LINKEOTROWCOUNT = 9,

            /// <summary>
            /// Extended row count represented as strings.
            /// </summary>
            DSJ_LINKEXTROWCOUNT = 10
        }

        /// <summary>
        /// DSPARAM paramType values.
        /// </summary>
        /// <see cref="DSAPI.DSSetParam"/>
        public enum ParamType
        {
            /// <summary>
            /// String parameter.
            /// </summary>
            DSJ_PARAMTYPE_STRING = 0,

            /// <summary>
            /// Encrypted parameter.
            /// </summary>
            DSJ_PARAMTYPE_ENCRYPTED = 1,

            /// <summary>
            /// Integer parameter.
            /// </summary>
            DSJ_PARAMTYPE_INTEGER = 2,

            /// <summary>
            /// Floating-point parameter.
            /// </summary>
            DSJ_PARAMTYPE_FLOAT = 3,

            /// <summary>
            /// Pathname parameter.
            /// </summary>
            DSJ_PARAMTYPE_PATHNAME = 4,

            /// <summary>
            /// List parameter.
            /// </summary>
            DSJ_PARAMTYPE_LIST = 5,

            /// <summary>
            /// Date parameter.
            /// </summary>
            DSJ_PARAMTYPE_DATE = 6,

            /// <summary>
            /// Time parameter.
            /// </summary>
            DSJ_PARAMTYPE_TIME = 7
        }

        /// <summary>
        /// DSPARAMINFO infoType values.
        /// </summary>
        /// <see cref="DSAPI.DSGetParamInfo"/>
        public enum ParamInfoType
        {
            /// <summary>
            /// Current default value.
            /// </summary>
            DSJ_PARAMDEFAULT = 1,

            /// <summary>
            /// Long description.
            /// </summary>
            DSJ_PARAMHELPTEXT = 2,

            /// <summary>
            /// Prompt string.
            /// </summary>
            DSJ_PARAMPROMPT = 3,

            /// <summary>
            /// Parameter type.
            /// </summary>
            DSJ_PARAMTYPE = 4,

            /// <summary>
            /// Current value.
            /// </summary>
            DSJ_PARAMVALUE = 5,

            /// <summary>
            /// Default value set for parameter at design time.
            /// </summary>
            DSJ_PARAMDES_DEFAULT = 6,

            /// <summary>
            /// Current list of allowed/suggested values for 'list' type parameters.
            /// </summary>
            DSJ_PARAMLISTVALUES = 7,

            /// <summary>
            /// The list of allowed/suggested values for 'list' type parameters as set at design time.
            /// </summary>
            DSJ_PARAMDES_LISTVALUES = 8,

            /// <summary>
            /// Indication that the parameter value should always be prompted for at the start of a job run.
            /// </summary>
            DSJ_PARAMPROMPT_AT_RUN = 9,

            /// <summary>
            /// Everything.
            /// </summary>
            DSJ_PARAMALL = 99
        }

        /// <summary>
        /// DSPROJECTINFO infoType values.
        /// </summary>
        /// <see cref="DSAPI.DSGetProjectInfo"/>
        public enum ProjectInfoType
        {
            /// <summary>
            /// List of jobs in project.
            /// </summary>
            DSJ_JOBLIST = 1,

            /// <summary>
            /// Name of current project.
            /// </summary>
            DSJ_PROJECTNAME = 2,

            /// <summary>
            /// Host name of the server.
            /// </summary>
            DSJ_HOSTNAME = 3,

            /// <summary>
            /// Install tag of the server DSEngine.
            /// </summary>
            DSJ_INSTALLTAG = 4,

            /// <summary>
            /// TCP port of the server DSEngine.
            /// </summary>
            DSJ_TCPPORT = 5,

            /// <summary>
            /// Directory path of current project.
            /// </summary>
            DSJ_PROJECTPATH = 6
        }

        /// <summary>
        /// DSREPOSINFO object type values.
        /// </summary>
        /// <see cref="DSAPI.DSGetReposInfo"/>
        public enum ReposInfoObjectType
        {
            /// <summary>
            /// The object type to return.
            /// </summary>
            DSS_JOBS = 1
        }

        /// <summary>
        /// DSREPOSJOBINFO infoType values.
        /// </summary>
        /// <see cref="DSAPI.DSGetReposInfo"/>
        public enum ReposInfoType
        {
            /// <summary>
            /// List all jobs.
            /// </summary>
            DSS_JOB_ALL = 15,

            /// <summary>
            /// List all server jobs.
            /// </summary>
            DSS_JOB_SERVER = 1,

            /// <summary>
            /// List all parallel jobs.
            /// </summary>
            DSS_JOB_PARALLEL = 2,

            /// <summary>
            /// List all mainframe jobs.
            /// </summary>
            DSS_JOB_MAINFRAME = 4,

            /// <summary>
            /// List all sequence jobs.
            /// </summary>
            DSS_JOB_SEQUENCE = 8
        }

        /// <summary>
        /// DSREPOSUSAGEJOB infoType values.
        /// </summary>
        public enum ReposUsageType
        {
            /// <summary>
            /// List all jobs that the supplied job uses.
            /// </summary>
            DSS_JOB_USES_JOB = 1,

            /// <summary>
            /// List all jobs that use the supplied job.
            /// </summary>
            DSS_JOB_USEDBY_JOB = 2,

            /// <summary>
            /// List all jobs that use the supplied table as a source in a DRS stage.
            /// </summary>
            DSS_JOB_HASSOURCE_TABLEDEF = 3,

            /// <summary>
            /// List all jobs that use the supplied table as a target in a DRS stage.
            /// </summary>
            DSS_JOB_HASTARGET_TABLEDEF = 4,

            /// <summary>
            /// List all jobs that use the supplied table as a source or target in a DRS stage.
            /// </summary>
            DSS_JOB_HASSOURCEORTARGET_TABLEDEF = 5
        }

        /// <summary>
        /// DSSTAGEINFO infoType values.
        /// </summary>
        /// <see cref="DSAPI.DSGetStageInfo"/>
        public enum StageInfoType
        {
            /// <summary>
            /// List of stage link names.
            /// </summary>
            DSJ_LINKLIST = 1,

            /// <summary>
            /// Last error message reported from any link of the stage.
            /// </summary>
            DSJ_STAGELASTERR = 2,

            /// <summary>
            /// Actual name of stage.
            /// </summary>
            DSJ_STAGENAME = 3,

            /// <summary>
            /// Stage type name.
            /// </summary>
            DSJ_STAGETYPE = 4,

            /// <summary>
            /// Primary links input row number.
            /// </summary>
            DSJ_STAGEINROWNUM = 5,

            /// <summary>
            /// List of stage variable names.
            /// </summary>
            DSJ_VARLIST = 6,

            /// <summary>
            /// Date and time when stage started.
            /// </summary>
            DSJ_STAGESTARTTIMESTAMP = 7,

            /// <summary>
            /// Date and time when stage finished.
            /// </summary>
            DSJ_STAGEENDTIMESTAMP = 8,

            /// <summary>
            /// Stage Description.
            /// </summary>
            DSJ_STAGEDESC = 9,

            /// <summary>
            /// Comma-separated list of stage instance ids.
            /// </summary>
            DSJ_STAGEINST = 10,

            /// <summary>
            /// Comma-separated list of stage instance CPU in seconds.
            /// </summary>
            DSJ_STAGECPU = 11,

            /// <summary>
            /// Comma-separated list of link types.
            /// </summary>
            DSJ_LINKTYPES = 12,

            /// <summary>
            /// Stage elapsed time in seconds.
            /// </summary>
            DSJ_STAGEELAPSED = 13,

            /// <summary>
            /// Comma-separated list of stage instance PIDs.
            /// </summary>
            DSJ_STAGEPID = 14,

            /// <summary>
            /// Stage status.
            /// </summary>
            DSJ_STAGESTATUS = 15,

            /// <summary>
            /// Stage EOT count.
            /// </summary>
            DSJ_STAGEEOTCOUNT = 16,

            /// <summary>
            /// Stage EOT timestamp.
            /// </summary>
            DSJ_STAGEEOTTIMESTAMP = 17,

            /// <summary>
            /// List of custom info names.
            /// </summary>
            DSJ_CUSTINFOLIST = 18
        }

        /// <summary>
        /// DSSTAGEINFO stageStatus values.
        /// </summary>
        /// <see cref="DSAPI.DSGetStageInfo"/>
        /// <remarks>Note: These definitions are missing from dsapi.h</remarks>
        public enum StageStatus
        {
            /// <summary>
            /// Stage running.
            /// </summary>
            DSJ_RUNNING = 1,

            /// <summary>
            /// Stage finished.
            /// </summary>
            DSJ_FINISHED = 2,

            /// <summary>
            /// Stage aborted.
            /// </summary>
            DSJ_ABORTED = 3,

            /// <summary>
            /// Stage paused.
            /// </summary>
            DSJ_PAUSED = 4,

            /// <summary>
            /// Stage stopped.
            /// </summary>
            DSJ_STOPPED = 5,

            /// <summary>
            /// Stage not compiled.
            /// </summary>
            DSJ_NOTCOMPILED = 6,

            /// <summary>
            /// Stage waiting.
            /// </summary>
            DSJ_WAITING = 7,

            /// <summary>
            /// Stage ready.
            /// </summary>
            DSJ_READY = 8,

            /// <summary>
            /// Stage starting.
            /// </summary>
            DSJ_STARTING = 9
        }

        /// <summary>
        /// DSVARINFO infoType values.
        /// </summary>
        /// <see cref="DSAPI.DSGetVarInfo"/>
        public enum StageVariableType
        {
            /// <summary>
            /// Stage variable value.
            /// </summary>
            DSJ_VARVALUE = 1,

            /// <summary>
            /// Stage variable description.
            /// </summary>
            DSJ_VARDESC = 2
        }

        /// <summary>
        /// DSCUSTINFO infoType values.
        /// </summary>
        /// <see cref="DSAPI.DSGetCustInfo"/>
        public enum CustInfoType
        {
            /// <summary>
            /// Custom info value.
            /// </summary>
            DSJ_CUSTINFOVALUE = 1,

            /// <summary>
            /// Custom info description.
            /// </summary>
            DSJ_CUSTINFODESC = 2
        }

        /// <summary>
        /// DSREPORTINFO reportType values.
        /// </summary>
        /// <see cref="DSAPI.DSMakeJobReport"/>
        public enum ReportInfoType
        {
            /// <summary>
            /// Basic.
            /// </summary>
            DSJ_REPORT0 = 0,

            /// <summary>
            /// Basic plus Stage/Link details.
            /// </summary>
            DSJ_REPORT1 = 1,

            /// <summary>
            /// Full XML report.
            /// </summary>
            DSJ_REPORT2 = 2
        }

        /// <summary>
        /// DSRunJob() runMode values.
        /// </summary>
        /// <see cref="DSAPI.DSRunJob"/>
        public enum RunModeType
        {
            /// <summary>
            /// Standard job run.
            /// </summary>
            DSJ_RUNNORMAL = 1,

            /// <summary>
            /// Job is to be reset.
            /// </summary>
            DSJ_RUNRESET = 2,

            /// <summary>
            /// Job is to be validated only.
            /// </summary>
            DSJ_RUNVALIDATE = 3,

            /// <summary>
            /// Restart job with previous parameters, job must be in Restartable state.
            /// </summary>
            DSJ_RUNRESTART = 4
        }

        /// <summary>
        /// DSSetJobLimit() limitType values.
        /// </summary>
        /// <see cref="DSAPI.DSSetJobLimit"/>
        public enum JobLimitType
        {
            /// <summary>
            /// Job to be stopped after limit value warning events.
            /// </summary>
            DSJ_LIMITWARN = 1,

            /// <summary>
            /// Stages to be limited to limit value rows.
            /// </summary>
            DSJ_LIMITROWS = 2
        }

        /// <summary>
        /// API error codes.
        /// </summary>
        public enum ErrorCodes
        {
            /// <summary>
            /// No error.
            /// </summary>
            DSJE_NOERROR = 0,

            /// <summary>
            /// Invalid job handle.
            /// </summary>
            DSJE_BADHANDLE = -1,

            /// <summary>
            /// Job is not in the right state (must be compiled and not running).
            /// </summary>
            DSJE_BADSTATE = -2,

            /// <summary>
            /// Parameter name is not a parameter name in the job.
            /// </summary>
            DSJE_BADPARAM = -3,

            /// <summary>
            /// Limit value is not appropriate for the limiting condition type.
            /// </summary>
            DSJE_BADVALUE = -4,

            /// <summary>
            /// Invalid event type value.
            /// </summary>
            DSJE_BADTYPE = -5,

            /// <summary>
            /// Job for this job handle was not started from a call to DSRunJob by the current process.
            /// </summary>
            DSJE_WRONGJOB = -6,

            /// <summary>
            /// Stage name does not refer to a known stage in the job.
            /// </summary>
            DSJE_BADSTAGE = -7,

            /// <summary>
            /// INTERNAL TO SERVER.
            /// </summary>
            DSJE_NOTINSTAGE = -8,

            /// <summary>
            /// Link name does not refer to a known link for the stage in question.
            /// </summary>
            DSJE_BADLINK = -9,

            /// <summary>
            /// Job is locked by another user.
            /// </summary>
            DSJE_JOBLOCKED = -10,

            /// <summary>
            /// Job has been deleted!
            /// </summary>
            DSJE_JOBDELETED = -11,

            /// <summary>
            /// Job name badly formed.
            /// </summary>
            DSJE_BADNAME = -12,

            /// <summary>
            /// Timestamp parameter was badly formed.
            /// </summary>
            DSJE_BADTIME = -13,

            /// <summary>
            /// Given up waiting for something to happen.
            /// </summary>
            DSJE_TIMEOUT = -14,

            /// <summary>
            /// Decryption of encrypted value failed.
            /// </summary>
            DSJE_DECRYPTERR = -15,

            /// <summary>
            /// Cannot get values, default or design-default values for any job except the current job (Job Handle == DSJ.ME).
            /// </summary>
            DSJE_NOACCESS = -16,

            /// <summary>
            /// Cannot find template file.
            /// </summary>
            DSJE_NOTEMPLATE = -17,

            /// <summary>
            /// Error encountered when processing template file.
            /// </summary>
            DSJE_BADTEMPLATE = -18,

            /// <summary>
            /// Parameter name missing - field does not look like 'name:value'.
            /// </summary>
            DSJE_NOPARAM = -19,

            /// <summary>
            /// File path name not given.
            /// </summary>
            DSJE_NOFILEPATH = -20,

            /// <summary>
            /// Error when executing external command.
            /// </summary>
            DSJE_CMDERROR = -21,

            /// <summary>
            /// Stage Variable name not recognised.
            /// </summary>
            DSJE_BADVAR = -22,

            /// <summary>
            /// ID already exists for a job in this project.
            /// </summary>
            DSJE_NONUNIQUEID = -23,

            /// <summary>
            /// Invalid job ID.
            /// </summary>
            DSJE_INVALIDID = -24,

            /// <summary>
            /// Invalid queue.
            /// </summary>
            DSJE_INVALIDQUEUE = -25,

            /// <summary>
            /// WLM is not enabled.
            /// </summary>
            DSJE_WLMDISABLED = -26,

            /// <summary>
            /// WLM is not running.
            /// </summary>
            DSJE_WLMNOTRUNNING = -27,

            /// <summary>
            /// User does not have required role permissions to perform this operation.
            /// </summary>
            DSJE_NOROLEPERMISSIONS = -28,

            /// <summary>
            /// General server 'other error'.
            /// </summary>
            DSJE_REPERROR = -99,

            // Errors generated from the administration API functions

            /// <summary>
            /// User is not an administrative user.
            /// </summary>
            DSJE_NOTADMINUSER = -100,

            /// <summary>
            /// Unable to determine if user is an administrative user.
            /// </summary>
            DSJE_ISADMINFAILED = -101,

            /// <summary>
            /// Reading project properties failed.
            /// </summary>
            DSJE_READPROJPROPERTY = -102,

            /// <summary>
            /// Writing project properties failed.
            /// </summary>
            DSJE_WRITEPROJPROPERTY = -103,

            /// <summary>
            /// Property name is invalid.
            /// </summary>
            DSJE_BADPROPERTY = -104,

            /// <summary>
            /// Unsupported property.
            /// </summary>
            DSJE_PROPNOTSUPPORTED = -105,

            /// <summary>
            /// Value given is not valid for this property.
            /// </summary>
            DSJE_BADPROPVALUE = -106,

            /// <summary>
            /// Failed to set OSHVisible value.
            /// </summary>
            DSJE_OSHVISIBLEFLAG = -107,

            /// <summary>
            /// Invalid environment variable name.
            /// </summary>
            DSJE_BADENVVARNAME = -108,

            /// <summary>
            /// Invalid environment variable type.
            /// </summary>
            DSJE_BADENVVARTYPE = -109,

            /// <summary>
            /// Missing environment variable prompt.
            /// </summary>
            DSJE_BADENVVARPROMPT = -110,

            /// <summary>
            /// Reading environment variable definitions failed.
            /// </summary>
            DSJE_READENVVARDEFNS = -111,

            /// <summary>
            /// Reading environment variable values failed.
            /// </summary>
            DSJE_READENVVARVALUES = -112,

            /// <summary>
            /// Writing environment variable definitions failed.
            /// </summary>
            DSJE_WRITEENVVARDEFNS = -113,

            /// <summary>
            /// Writing environment variable values failed.
            /// </summary>
            DSJE_WRITEENVVARVALUES = -114,

            /// <summary>
            /// Environment variable name already exists.
            /// </summary>
            DSJE_DUPENVVARNAME = -115,

            /// <summary>
            /// Environment variable name not recognised.
            /// </summary>
            DSJE_BADENVVAR = -116,

            /// <summary>
            /// Environment variable is not user defined.
            /// </summary>
            DSJE_NOTUSERDEFINED = -117,

            /// <summary>
            /// Invalid value given for a boolean environment variable.
            /// </summary>
            DSJE_BADBOOLEANVALUE = -118,

            /// <summary>
            /// Invalid value given for a numeric environment variable.
            /// </summary>
            DSJE_BADNUMERICVALUE = -119,

            /// <summary>
            /// Invalid value given for a list environment variable.
            /// </summary>
            DSJE_BADLISTVALUE = -120,

            /// <summary>
            /// PX not installed.
            /// </summary>
            DSJE_PXNOTINSTALLED = -121,

            /// <summary>
            /// Failed to find out if Parallel Engine is licensed.
            /// </summary>
            DSJE_ISPARALLELLICENCED = -122,

            /// <summary>
            /// Encoding of an encrypted value failed.
            /// </summary>
            DSJE_ENCODEFAILED = -123,

            /// <summary>
            /// Deletion of project definition and schema failed.
            /// </summary>
            DSJE_DELPROJFAILED = -124,

            /// <summary>
            /// Deletion of project files and subdirectories failed.
            /// </summary>
            DSJE_DELPROJFILESFAILED = -125,

            /// <summary>
            /// Failed to get list of scheduled jobs for project.
            /// </summary>
            DSJE_LISTSCHEDULEFAILED = -126,

            /// <summary>
            /// Failed to clear scheduled jobs for project.
            /// </summary>
            DSJE_CLEARSCHEDULEFAILED = -127,

            /// <summary>
            /// Project name contains invalid characters.
            /// </summary>
            DSJE_BADPROJNAME = -128,

            /// <summary>
            /// Failed to get default path for project.
            /// </summary>
            DSJE_GETDEFAULTPATHFAILED = -129,

            /// <summary>
            /// Project location path contains invalid characters.
            /// </summary>
            DSJE_BADPROJLOCATION = -130,

            /// <summary>
            /// Project location is invalid.
            /// </summary>
            DSJE_INVALIDPROJECTLOCATION = -131,

            /// <summary>
            /// Failed to open file.
            /// </summary>
            DSJE_OPENFAILED = -132,

            /// <summary>
            /// Failed to lock administration record.
            /// </summary>
            DSJE_READUFAILED = -133,

            /// <summary>
            /// Administration record locked by another user.
            /// </summary>
            DSJE_ADDPROJECTBLOCKED = -134,

            /// <summary>
            /// Adding project failed.
            /// </summary>
            DSJE_ADDPROJECTFAILED = -135,

            /// <summary>
            /// Licensing project failed.
            /// </summary>
            DSJE_LICENSEPROJECTFAILED = -136,

            /// <summary>
            /// Failed to release administration record.
            /// </summary>
            DSJE_RELEASEFAILED = -137,

            /// <summary>
            /// Project locked by another user.
            /// </summary>
            DSJE_DELETEPROJECTBLOCKED = -138,

            /// <summary>
            /// Failed to log to project.
            /// </summary>
            DSJE_NOTAPROJECT = -139,

            /// <summary>
            /// Failed to get account path.
            /// </summary>
            DSJE_ACCOUNTPATHFAILED = -140,

            /// <summary>
            /// Failed to log to UV account.
            /// </summary>
            DSJE_LOGTOFAILED = -141,

            /// <summary>
            /// Protect or unprotect project failed.
            /// </summary>
            DSJE_PROTECTFAILED = -142,

            // Errors from the Repository Info and Repository Usage API functions

            /// <summary>
            /// Could not find the supplied job name.
            /// </summary>
            DSJE_UNKNOWN_JOBNAME = -201,

            // Errors generated with the DSAPI

            /// <summary>
            /// All events matching the filter criteria have been returned.
            /// </summary>
            DSJE_NOMORE = -1001,

            /// <summary>
            /// Unknown project name.
            /// </summary>
            DSJE_BADPROJECT = -1002,

            /// <summary>
            /// DataStage not installed on server.
            /// </summary>
            DSJE_NO_DATASTAGE = -1003,

            /// <summary>
            /// Attempt to open job failed.
            /// </summary>
            DSJE_OPENFAIL = -1004,

            /// <summary>
            /// Malloc failure.
            /// </summary>
            DSJE_NO_MEMORY = -1005,

            /// <summary>
            /// Server generated error - error msg text describes it.
            /// </summary>
            DSJE_SERVER_ERROR = -1006,

            /// <summary>
            /// Not data available from server.
            /// </summary>
            DSJE_NOT_AVAILABLE = -1007,

            /// <summary>
            /// Version supplied to DSOpenProjectEx is invalid.
            /// </summary>
            DSJE_BAD_VERSION = -1008,

            /// <summary>
            /// Server version incompatible with this version of the DSAPI.
            /// </summary>
            DSJE_INCOMPATIBLE_SERVER = -1009,

            /// <summary>
            /// Failed to authenticate to Domain.
            /// </summary>
            DSJE_DOMAINLOGTOFAILED = -1010,

            /// <summary>
            /// The ISF user does not have sufficient DataStage privileges.
            /// </summary>
            DSJE_NOPRIVILEGE = -1011
        }
    }
}
