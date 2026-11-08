/*
 * RKM.DataStage.DSAPI
 * Low-level .NET wrapper for the IBM DataStage DSAPI.
 *
 * Copyright © 2022–2026 Richard K. Morris / RKM Research
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace RKM.DataStage
{
    public static partial class DSAPI
    {
        /// <summary>
        /// Utility methods
        /// </summary>
        public static class Utilities
        {
            /// <summary>
            /// Returns a managed string from an unmanaged null-terminated character buffer.
            /// </summary>
            /// <param name="pointer">Pointer to unmanaged memory</param>
            /// <returns>The decoded string, or null if the pointer is zero.</returns>
            public static string ExtractStringFromIntPtr(IntPtr pointer)
            {
                return Marshal.PtrToStringAnsi(pointer);
            }

            /// <summary>
            /// Reads a double-null-terminated list of strings from unmanaged memory.
            /// </summary>
            /// <param name="pointer">Pointer to unmanaged memory</param>
            /// <returns>The decoded strings, or null if the pointer is zero.</returns>
            public static List<string> ExtractStringListFromIntPtr(IntPtr pointer)
            {
                if (pointer == IntPtr.Zero)
                {
                    return null;
                }

                List<string> strings = new List<string>();

                while (true)
                {
                    int byteLength = 0;
                    while (Marshal.ReadByte(pointer, byteLength) != 0)
                    {
                        byteLength++;
                    }

                    if (byteLength == 0)
                    {
                        break;
                    }

                    strings.Add(Marshal.PtrToStringAnsi(pointer));
                    pointer = IntPtr.Add(pointer, byteLength + 1);
                }

                return strings;
            }

            /// <summary>
            /// Converts a Unix timestamp in seconds into a UTC <see cref="DateTime"/> value.
            /// </summary>
            /// <param name="unixTime">The timestamp in seconds to convert</param>
            /// <returns>The corresponding UTC date and time.</returns>
            public static DateTime UnixTimestampToDateTime(long unixTime)
            {
                DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
                return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
            }

            /// <summary>
            /// Converts a <see cref="DateTime"/> value into seconds since the Unix epoch.
            /// </summary>
            /// <param name="dateTime">The date and time to convert</param>
            /// <returns>The number of seconds since the Unix epoch.</returns>
            public static long DateTimeToUnixTimestamp(DateTime dateTime)
            {
                long epochTicks = new DateTime(1970, 1, 1).Ticks;
                long unixTime = ((dateTime.Ticks - epochTicks) / TimeSpan.TicksPerSecond);
                return unixTime;
            }

            /// <summary>
            /// Converts a <see cref="System.DateTime"/> object to local time with daylight saving time (DST).
            /// </summary>
            /// <param name="dateTime">The date and time to convert</param>
            /// <returns>The date and time in the local time zone</returns>
            public static DateTime DateTimeToLocalTime(DateTime dateTime)
            {
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
                return TimeZoneInfo.ConvertTime(dateTime, tzi);
            }

            /// <summary>
            /// Formats a <see cref="DateTime"/> value for <see cref="DSAPI.DSSetParam"/>
            /// when the parameter type is <see cref="ParamType.DSJ_PARAMTYPE_DATE"/>.
            /// </summary>
            /// <param name="dateTime">The date and time to convert</param>
            /// <returns>The DataStage parameter value in its required format.</returns>
            public static string DateTimeToParamShortDateString(DateTime dateTime)
            {
                return dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            /// <summary>
            /// Formats a <see cref="DateTime"/> value for <see cref="DSAPI.DSSetParam"/>
            /// when the parameter type is <see cref="ParamType.DSJ_PARAMTYPE_TIME"/>.
            /// </summary>
            /// <param name="dateTime">The date and time to convert</param>
            /// <returns>The DataStage parameter value in its required format.</returns>
            public static string DateTimeToParamShortTimeString(DateTime dateTime)
            {
                return dateTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
            }

            /// <summary>
            /// Allocates an unmanaged ANSI string containing the supplied managed text.
            /// </summary>
            /// <param name="managedString">The managed string to copy.</param>
            /// <returns>A pointer to the newly allocated unmanaged string.</returns>
            /// <remarks>
            /// The caller owns this allocation and must release it using <see cref="FreeHGlobal"/>.
            /// Do not use <see cref="FreeHGlobal"/> on memory owned by the native DataStage API.
            /// </remarks>
            public static IntPtr StringToHGlobalAnsi(string managedString)
            {
                return Marshal.StringToHGlobalAnsi(managedString);
            }

            /// <summary>
            /// Frees unmanaged memory previously allocated by <see cref="StringToHGlobalAnsi"/>.
            /// </summary>
            /// <param name="managedStringPointer">Pointer to a caller-owned unmanaged allocation.</param>
            public static void FreeHGlobal(IntPtr managedStringPointer)
            {
                Marshal.FreeHGlobal(managedStringPointer);
            }

            /// <summary>
            /// Reads a <see cref="DSREPOSJOBINFO"/> structure from unmanaged memory.
            /// </summary>
            /// <param name="pointer">Pointer to unmanaged memory</param>
            /// <returns>The marshalled <see cref="DSREPOSJOBINFO"/> structure.</returns>
            public static DSREPOSJOBINFO GetReposJobInfoStructFromIntPtr(IntPtr pointer)
            {
                if (pointer == IntPtr.Zero)
                {
                    throw new ArgumentException("Pointer must not be zero.", "pointer");
                }

                return (DSREPOSJOBINFO)Marshal.PtrToStructure(pointer, typeof(DSREPOSJOBINFO));
            }

            /// <summary>
            /// Reads a <see cref="DSREPOSUSAGEJOB"/> structure from unmanaged memory.
            /// </summary>
            /// <param name="pointer">Pointer to unmanaged memory</param>
            /// <returns>The marshalled <see cref="DSREPOSUSAGEJOB"/> structure.</returns>
            public static DSREPOSUSAGEJOB GetReposUsageJobStructFromIntPtr(IntPtr pointer)
            {
                if (pointer == IntPtr.Zero)
                {
                    throw new ArgumentException("Pointer must not be zero.", "pointer");
                }

                return (DSREPOSUSAGEJOB)Marshal.PtrToStructure(pointer, typeof(DSREPOSUSAGEJOB));
            }
        }
    }
}
