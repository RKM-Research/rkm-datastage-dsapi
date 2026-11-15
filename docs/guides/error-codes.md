# DSAPI error codes and troubleshooting

## Common codes

| Code | Constant | Practical interpretation and first checks |
| --- | --- | --- |
| 0 | DSJE_NOERROR | Success where the function returns an API result code; inspect returned data or job status separately |
| -1 | DSJE_BADHANDLE | Check that job opening succeeded and that the handle is still valid |
| -2 | DSJE_BADSTATE | Check the operation's state requirements and current job status; do not automatically reset or rerun a job |
| -3 | DSJE_BADPARAM | Compare the supplied parameter name with the job's parameter metadata |
| -4 | DSJE_BADVALUE | Meaning depends on the function: DSGetLogEventIds uses it for an invalid run number; DSSetParam uses it for an unsuitable parameter value |
| -5 | DSJE_BADTYPE | Check the particular selector, filter, event type or parameter type expected by the function |
| -6 | DSJE_WRONGJOB | Check whether the current process started the job through DSRunJob |
| -7 | DSJE_BADSTAGE | Verify the stage name and that it belongs to the selected job |
| -9 | DSJE_BADLINK | Verify the link within the selected stage, not just within the job |
| -10 | DSJE_JOBLOCKED | Identify the lock owner and whether the operation needs a lock; avoid blindly removing locks |
| -12 | DSJE_BADNAME | Invalid name; check the job or project name relevant to the operation |
| -14 | DSJE_TIMEOUT | Establish whether the operation completed on the server before retrying a state-changing request |
| -28 | DSJE_NOROLEPERMISSIONS | Check the user's roles for the requested operation |
| -99 | DSJE_REPERROR | Broad server failure; retain the accompanying engine text |
| -1001 | DSJE_NOMORE | End of matching results where documented; normally finish iteration |
| -1002 | DSJE_BADPROJECT | Check project name, selected server and project visibility |
| -1004 | DSJE_OPENFAIL | Job opening failed; inspect accompanying text and check job existence and access |
| -1006 | DSJE_SERVER_ERROR | Retain complete engine text; do not assume one root cause |
| -1007 | DSJE_NOT_AVAILABLE | Check whether the requested information exists for this job and state; absence may be expected |
| -1008 | DSJE_BAD_VERSION | Check the supplied API version and client/engine compatibility |
| -1009 | DSJE_INCOMPATIBLE_SERVER | Check supported client/engine version combinations |
| -1010 | DSJE_DOMAINLOGTOFAILED | Inspect domain authentication diagnostics and the selected domain configuration |
| -1011 | DSJE_NOPRIVILEGE | Check DataStage privileges for the authenticated identity |

## DSAPI code reference

### Job operations and general server results

| Code | Constant | Description |
| --- | --- | --- |
| 0 | `DSJE_NOERROR` | No error. |
| -1 | `DSJE_BADHANDLE` | Invalid job handle. |
| -2 | `DSJE_BADSTATE` | Job is not in the right state (must be compiled and not running). |
| -3 | `DSJE_BADPARAM` | Parameter name is not a parameter name in the job. |
| -4 | `DSJE_BADVALUE` | Limit value is not appropriate for the limiting condition type. |
| -5 | `DSJE_BADTYPE` | Invalid event type value. |
| -6 | `DSJE_WRONGJOB` | Job for this job handle was not started from a call to DSRunJob by the current process. |
| -7 | `DSJE_BADSTAGE` | Stage name does not refer to a known stage in the job. |
| -8 | `DSJE_NOTINSTAGE` | INTERNAL TO SERVER. |
| -9 | `DSJE_BADLINK` | Link name does not refer to a known link for the stage in question. |
| -10 | `DSJE_JOBLOCKED` | Job is locked by another user. |
| -11 | `DSJE_JOBDELETED` | Job has been deleted! |
| -12 | `DSJE_BADNAME` | Invalid job or project name for the operation. |
| -13 | `DSJE_BADTIME` | Timestamp parameter was badly formed. |
| -14 | `DSJE_TIMEOUT` | Given up waiting for something to happen. |
| -15 | `DSJE_DECRYPTERR` | Decryption of encrypted value failed. |
| -16 | `DSJE_NOACCESS` | Cannot get values, default or design-default values for any job except the current job (Job Handle == DSJ.ME). |
| -17 | `DSJE_NOTEMPLATE` | Cannot find template file. |
| -18 | `DSJE_BADTEMPLATE` | Error encountered when processing template file. |
| -19 | `DSJE_NOPARAM` | Parameter name missing - field does not look like 'name:value'. |
| -20 | `DSJE_NOFILEPATH` | File path name not given. |
| -21 | `DSJE_CMDERROR` | Error when executing external command. |
| -22 | `DSJE_BADVAR` | Stage Variable name not recognised. |
| -23 | `DSJE_NONUNIQUEID` | ID already exists for a job in this project. |
| -24 | `DSJE_INVALIDID` | Invalid job ID. |
| -25 | `DSJE_INVALIDQUEUE` | Invalid queue. |
| -26 | `DSJE_WLMDISABLED` | WLM is not enabled. |
| -27 | `DSJE_WLMNOTRUNNING` | WLM is not running. |
| -28 | `DSJE_NOROLEPERMISSIONS` | User does not have required role permissions to perform this operation. |
| -99 | `DSJE_REPERROR` | General server 'other error'. |

### Administration

| Code | Constant | Description |
| --- | --- | --- |
| -100 | `DSJE_NOTADMINUSER` | User is not an administrative user. |
| -101 | `DSJE_ISADMINFAILED` | Unable to determine if user is an administrative user. |
| -102 | `DSJE_READPROJPROPERTY` | Reading project properties failed. |
| -103 | `DSJE_WRITEPROJPROPERTY` | Writing project properties failed. |
| -104 | `DSJE_BADPROPERTY` | Property name is invalid. |
| -105 | `DSJE_PROPNOTSUPPORTED` | Unsupported property. |
| -106 | `DSJE_BADPROPVALUE` | Value given is not valid for this property. |
| -107 | `DSJE_OSHVISIBLEFLAG` | Failed to set OSHVisible value. |
| -108 | `DSJE_BADENVVARNAME` | Invalid environment variable name. |
| -109 | `DSJE_BADENVVARTYPE` | Invalid environment variable type. |
| -110 | `DSJE_BADENVVARPROMPT` | Missing environment variable prompt. |
| -111 | `DSJE_READENVVARDEFNS` | Reading environment variable definitions failed. |
| -112 | `DSJE_READENVVARVALUES` | Reading environment variable values failed. |
| -113 | `DSJE_WRITEENVVARDEFNS` | Writing environment variable definitions failed. |
| -114 | `DSJE_WRITEENVVARVALUES` | Writing environment variable values failed. |
| -115 | `DSJE_DUPENVVARNAME` | Environment variable name already exists. |
| -116 | `DSJE_BADENVVAR` | Environment variable name not recognised. |
| -117 | `DSJE_NOTUSERDEFINED` | Environment variable is not user defined. |
| -118 | `DSJE_BADBOOLEANVALUE` | Invalid value given for a boolean environment variable. |
| -119 | `DSJE_BADNUMERICVALUE` | Invalid value given for a numeric environment variable. |
| -120 | `DSJE_BADLISTVALUE` | Invalid value given for a list environment variable. |
| -121 | `DSJE_PXNOTINSTALLED` | PX not installed. |
| -122 | `DSJE_ISPARALLELLICENCED` | Failed to find out if Parallel Engine is licensed. |
| -123 | `DSJE_ENCODEFAILED` | Encoding of an encrypted value failed. |
| -124 | `DSJE_DELPROJFAILED` | Deletion of project definition and schema failed. |
| -125 | `DSJE_DELPROJFILESFAILED` | Deletion of project files and subdirectories failed. |
| -126 | `DSJE_LISTSCHEDULEFAILED` | Failed to get list of scheduled jobs for project. |
| -127 | `DSJE_CLEARSCHEDULEFAILED` | Failed to clear scheduled jobs for project. |
| -128 | `DSJE_BADPROJNAME` | Project name contains invalid characters. |
| -129 | `DSJE_GETDEFAULTPATHFAILED` | Failed to get default path for project. |
| -130 | `DSJE_BADPROJLOCATION` | Project location path contains invalid characters. |
| -131 | `DSJE_INVALIDPROJECTLOCATION` | Project location is invalid. |
| -132 | `DSJE_OPENFAILED` | Failed to open file. |
| -133 | `DSJE_READUFAILED` | Failed to lock administration record. |
| -134 | `DSJE_ADDPROJECTBLOCKED` | Administration record locked by another user. |
| -135 | `DSJE_ADDPROJECTFAILED` | Adding project failed. |
| -136 | `DSJE_LICENSEPROJECTFAILED` | Licensing project failed. |
| -137 | `DSJE_RELEASEFAILED` | Failed to release administration record. |
| -138 | `DSJE_DELETEPROJECTBLOCKED` | Project locked by another user. |
| -139 | `DSJE_NOTAPROJECT` | Failed to log to project. |
| -140 | `DSJE_ACCOUNTPATHFAILED` | Failed to get account path. |
| -141 | `DSJE_LOGTOFAILED` | Failed to log to UV account. |
| -142 | `DSJE_PROTECTFAILED` | Protect or unprotect project failed. |

### Repository information

| Code | Constant | Description |
| --- | --- | --- |
| -201 | `DSJE_UNKNOWN_JOBNAME` | Could not find the supplied job name. |

### DSAPI client, connection and diagnostic results

| Code | Constant | Description |
| --- | --- | --- |
| -1001 | `DSJE_NOMORE` | All events matching the filter criteria have been returned. |
| -1002 | `DSJE_BADPROJECT` | Unknown project name. |
| -1003 | `DSJE_NO_DATASTAGE` | DataStage not installed on server. |
| -1004 | `DSJE_OPENFAIL` | Attempt to open job failed. |
| -1005 | `DSJE_NO_MEMORY` | Malloc failure. |
| -1006 | `DSJE_SERVER_ERROR` | Server generated error - error msg text describes it. |
| -1007 | `DSJE_NOT_AVAILABLE` | No data available from server. |
| -1008 | `DSJE_BAD_VERSION` | Invalid API version or version mismatch. |
| -1009 | `DSJE_INCOMPATIBLE_SERVER` | Server version incompatible with this version of the DSAPI. |
| -1010 | `DSJE_DOMAINLOGTOFAILED` | Failed to authenticate to Domain. |
| -1011 | `DSJE_NOPRIVILEGE` | The ISF user does not have sufficient DataStage privileges. |

## Lower-level code reference

### Numbers relating to the C library on the PC.

| Code | Constant | Meaning |
| --- | --- | --- |
| 14002 | `UVE_ENOENT` | No such file or directory |
| 14005 | `UVE_EIO` | I/O error |
| 14009 | `UVE_EBADF` | Bad file number |
| 14012 | `UVE_ENOMEM` | No memory available |
| 14013 | `UVE_EACCES` | Permission denied |
| 14022 | `UVE_EINVAL` | Invalid argument |
| 14023 | `UVE_ENFILE` | File table overflow |
| 14024 | `UVE_EMFILE` | Too many open files |
| 14028 | `UVE_ENOSPC` | No space left on device |

### Numbers relating to the Virtual Socket Library on the PC.

| Code | Constant | Meaning |
| --- | --- | --- |
| 14560 | `UVE_BW_TIMEDOUT` | Connection timed out |
| 14561 | `UVE_BW_CONNREFUSED` | Connection refused |
| 14564 | `UVE_BW_HOSTDOWN` | Host is down |
| 14565 | `UVE_BW_HOSTUNREACH` | Host is unreachable |
| 14566 | `UVE_BW_NOTEMPTY` | Directory not empty |
| 14567 | `UVE_BW_PROCLIM` | Too many processes |
| 14568 | `UVE_BW_USERS` | Too many users |
| 14569 | `UVE_BW_DQUOT` | Disc quota exceeded |
| 14570 | `UVE_BW_STALE` | Stale NFS file handle |
| 14571 | `UVE_BW_REMOTE` | Too many levels of remote in path |
| 14572 | `UVE_BW_NOSTR` | Device is not a stream |
| 14573 | `UVE_BW_TIME` | Timer expired |
| 14574 | `UVE_BW_NOSR` | Out of streams resources |
| 14578 | `UVE_BW_DEADLK` | Deadlock condition. |
| 14579 | `UVE_BW_NOLCK` | No record locks available. |
| 14582 | `UVE_BW_TOOMANYSOCK` | Too many open sockets |
| 14584 | `UVE_BW_RESET` | The socket has reset |
| 14591 | `UVE_BW_NOTCPIP` | TCPIP not loaded |
| 14592 | `UVE_BW_DRVBUSY` | TCPIP busy |
| 14593 | `UVE_COMMFILE_SECURITY` | Unable to read the command file, possible security breach |

### Errors generated from library routine calls

| Code | Constant | Meaning |
| --- | --- | --- |
| 11000 | `UVE_NOACCESS` | Requested access denied |
| 11001 | `UVE_NOSUPPORT` | Function not supported on this system |
| 11002 | `UVE_NOTRELATIVE` | Relative pathname expected and not given |
| 11003 | `UVE_PATHNOTFOUND` | Pathname could not be found |
| 11004 | `UVE_NOTASSIGNED` | Device not assigned |
| 11005 | `UVE_NODEVICE` | Device not known |
| 11006 | `UVE_ROFS` | Device assigned with Read Only access |
| 11007 | `UVE_BADSTTY` | Bad stty option when device assigned |
| 11008 | `UVE_UNKNOWN_USER` | Attempting to send message to user not in PI |
| 11009 | `UVE_SND_REQ_REC` | Sender requires receive enabled |
| 11010 | `UVE_MSG_REJECTED` | Message rejected by recipient |
| 22003 | `UVE_IID` | Illegal record ID |
| 22004 | `UVE_LRR` | last record read (READNEXT) |
| 22005 | `UVE_NFI` | file.tag is not a file identifier |

### Generic and visible file system errors

| Code | Constant | Meaning |
| --- | --- | --- |
| 30001 | `UVE_RNF` | Record not found |
| 30002 | `UVE_LCK` | File or record is locked by another user |
| 30012 | `UVE_EXS` | File already exists in an attempt to create |
| 30014 | `UVE_EXCL` | File opened exclusively by another user |
| 30075 | `UVE_NAM` | Bad file name |
| 30095 | `UVE_FIFS` | Fileid is incorrect for session |
| 30097 | `UVE_SELFAIL` | Select Failed |
| 30098 | `UVE_LOCKINVALID` | Lock number provided is invalid |
| 30099 | `UVE_SEQOPENED` | File opened for sequential access and hashed access tried |
| 30100 | `UVE_HASHOPENED` | File opened for hashed access and sequential access tried |
| 30101 | `UVE_SEEKFAILED` | Seek command failed |
| 30103 | `UVE_INVALIDATKEY` | Invalid Key used for GET/SET at variables |
| 30105 | `UVE_UNABLETOLOADSUB` | Unable to load subroutine on host |
| 30106 | `UVE_BADNUMARGS` | Bad number of arguments for subroutine, either too many or not enough |
| 30107 | `UVE_SUBERROR` | Subroutine failed to complete successfully |
| 30108 | `UVE_ITYPEFTC` | IType failed to complete correctly |
| 30109 | `UVE_ITYPEFAILEDTOLOAD` | IType failed to load |
| 30110 | `UVE_ITYPENOTCOMPILED` | The IType has not been compiled |
| 30111 | `UVE_BADITYPE` | It is not an itype or the itype is corrupt |
| 30112 | `UVE_INVALIDFILENAME` | Filename is null |
| 30113 | `UVE_WEOFFAILED` | Weofseq failed |
| 30114 | `UVE_EXECUTEISACTIVE` | An execute is currently active |
| 30115 | `UVE_EXECUTENOTACTIVE` | Execute state does not permit the operation |
| 30125 | `UVE_CANT_ACCESS_PF` | Cannot access part files |
| 30126 | `UVE_FAIL_TO_CANCEL` | Failed to cancel an execute |
| 30127 | `UVE_INVALID_INFO_KEY` | Bad key for session information |
| 30128 | `UVE_CREATE_FAILED` | The creation of a sequential file failed |
| 30129 | `UVE_DUPHANDLE_FAILED` | Failed to duplicate a pipe handle |

### Errors for OPEN

| Code | Constant | Meaning |
| --- | --- | --- |
| 31000 | `UVE_NVR` | No VOC record |
| 31001 | `UVE_NPN` | No pathname in VOC record |
| 31002 | `UVE_VNF` | VOC file record not a File record |

### Errors for CLEARFILE

| Code | Constant | Meaning |
| --- | --- | --- |
| 31100 | `UVE_CFNEA` | Clear file no exclusive access |

### Errors for client library, taken from ICI

| Code | Constant | Meaning |
| --- | --- | --- |
| 33200 | `UVE_LNA` | Select list not active |
| 33211 | `UVE_BSLN` | Bad select list number |
| 33212 | `UVE_BPID` | Bad partfile id |
| 33213 | `UVE_BAK` | Bad AK file |

### Error numbers generated explicitly by the DSAPI server

| Code | Constant | Meaning |
| --- | --- | --- |
| 39000 | `UVE_BAD_COMMAND` | command not recognized by server |
| 39001 | `UVE_NO_LOGOUT` | no way to perform a LOGOUT command |
| 39002 | `UVE_BAD_LENGTH` | data.length not a valid number |
| 39003 | `UVE_NO_VOC` | can't open the VOC file |
| 39004 | `UVE_CLIENT_RESET` | internal - client RESET received OK |
| 39005 | `UVE_INVALID_SRC` | @SYSTEM.RETURN.CODE non-numeric after EXECUTE |
| 39006 | `UVE_TOOLONG_SRC` | @SYSTEM.RETURN.CODE has more than 2 fields |
| 39007 | `UVE_KEY_NOT_IMP` | DSAPI server key not implemented |
| 39008 | `UVE_WRITE_FAILURE` | WRITE failed and taken ELSE clause |

### Errors for the Client/Server interface

| Code | Constant | Meaning |
| --- | --- | --- |
| 39101 | `UVE_NODATA` | Host not responding |
| 39103 | `UVE_RCV_TIMEOUT` | Timeout on receiving packets |
| 39119 | `UVE_AT_INPUT` | Server waiting for input |
| 39120 | `UVE_SESSION_NOT_OPEN` | Session is not opened when an action has be tried on it |
| 39121 | `UVE_UVEXPIRED` | The database license has expired |
| 39122 | `UVE_CSVERSION` | Client or server is out of date Client/server functions have been updated |
| 39123 | `UVE_COMMSVERSION` | Client or server is out of date comms support has been updated |
| 39124 | `UVE_BADSIG` | Incorrect client/server being communicated with |
| 39125 | `UVE_BADDIR` | The directory you are connecting to, either is not a correct database account or does not exist |
| 39126 | `UVE_SERVERERR` | An error has occurred on the server when trying to transmit an error code to the client |
| 39127 | `UVE_BAD_UVHOME` | Unable to find UV account |
| 39128 | `UVE_INVALID_PATH` | Invalid path found in UV.ACCOUNT |
| 39129 | `UVE_INVALIDACCOUNT` | Invalid account name supplied |
| 39130 | `UVE_BAD_UVACCOUNT_FILE` | The UV.ACCOUNT file could not be opened |
| 39131 | `UVE_FTA_NEW_ACCOUNT` | Failed to attach to specified account |
| 39132 | `UVE_NOT_UVACCOUNT` | not a valid database account |
| 39133 | `UVE_FTS_TERMINAL` | failed to setup the terminal for server |
| 39134 | `UVE_ULR` | The user limit has been reached on the server |
| 39135 | `UVE_NO_NLS` | NLS support not available |
| 39136 | `UVE_MAP_NOT_FOUND` | NLS map not found |
| 39137 | `UVE_NO_LOCALE` | NLS locale support not available |
| 39138 | `UVE_LOCALE_NOT_FOUND` | NLS locale not found |
| 39139 | `UVE_CATEGORY_NOT_FOUND` | NLS locale category not found |
| 39200 | `UVE_SR_CREATE_PIPE_FAIL` | Server failed to create the slave pipes |
| 39201 | `UVE_SR_SOCK_CON_FAIL` | The server failed to connect to the socket |
| 39202 | `UVE_SR_GA_FAIL` | Slave failed to give server the Go Ahead message |
| 39203 | `UVE_SR_MEMALLOC_FAIL` | Failed to allocate memory for the message from the slave |
| 39204 | `UVE_SR_SLAVE_EXEC_FAIL` | The slave failed to start correctly |
| 39205 | `UVE_SR_PASS_TO_SLAVE_FAIL` | Failed to the pass the message to the slave correctly |
| 39206 | `UVE_SR_EXEC_ALLOC_FAIL` | Server failed to allocate the memory for the execute buffer correctly |
| 39207 | `UVE_SR_SLAVE_READ_FAIL` | Failed to read from the slave correctly |
| 39208 | `UVE_SR_REPLY_WRITE_FAIL` | Failed to write the reply to the slave (ic_inputreply) |
| 39209 | `UVE_SR_SIZE_READ_FAIL` | Failed to read the size of the message from the slave |

### RPC Error codes

| Code | Constant | Meaning |
| --- | --- | --- |
| 80011 | `UVE_BAD_LOGINNAME` | The user name or password provided is incorrect |
| 80019 | `UVE_BAD_PASSWORD` | The password provided has expired |
| 80036 | `UVE_REM_AUTH_FAILED` | Remote authorisation failed. |
| 80144 | `UVE_ACCOUNT_EXPIRED` | Account has expired |
| 80147 | `UVE_RUN_REMOTE_FAILED` | Unable to run as given user |
| 80148 | `UVE_UPDATE_USER_FAILED` | Unable to update user details |
| 81001 | `UVE_RPC_BAD_CONNECTION` | The connection may be passing corrupt data |
| 81002 | `UVE_RPC_NO_CONNECTION` | The connection is broken |
| 81003 | `UVE_RPC_NOT_INITED` | the rpc has not be initialised |
| 81004 | `UVE_RPC_INVALID_ARG_TYPE` | argument for message is not a valid type |
| 81005 | `UVE_RPC_WRONG_VERSION` | The version of the host RPC does not match |
| 81006 | `UVE_RPC_BAD_SEQNO` | packet message out of step |
| 81007 | `UVE_RPC_NO_MORE_CONNECTIONS` | No more connections available |
| 81008 | `UVE_RPC_BAD_PARAMETER` | bad parameter passed to the rpc |
| 81009 | `UVE_RPC_FAILED` | The RPC failed |
| 81010 | `UVE_RPC_ARG_COUNT` | bad number of arguments for message |
| 81011 | `UVE_RPC_UNKNOWN_HOST` | The host name specified is not valid, or host not responding |
| 81012 | `UVE_RPC_FORK_FAILED` | rpc failed to fork service correctly |
| 81013 | `UVE_RPC_CANT_OPEN_SERV_FILE` | cannot find or open the unirpcservices file |
| 81014 | `UVE_RPC_CANT_FIND_SERVICE` | Cannot find UVRPC in the host services file |
| 81015 | `UVE_RPC_TIMEOUT` | The connection has timed out |
| 81016 | `UVE_RPC_REFUSED` | The connection was refused or RPC daemon not running |
| 81017 | `UVE_RPC_SOCKET_INIT_FAILED` | Failed to initialize network interface |
| 81018 | `UVE_RPC_SERVICE_PAUSED` | The RPC service has been paused |
| 81019 | `UVE_RPC_BAD_TRANSPORT` | An invalid transport has been used |
| 81020 | `UVE_RPC_BAD_PIPE` | Invalid pipe handle |
| 81021 | `UVE_RPC_PIPE_WRITE_ERROR` | Error writing to pipe |
| 81022 | `UVE_RPC_PIPE_READ_ERROR` | Error reading from pipe |
