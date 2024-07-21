// Copyright (c) Microsoft Corporation. All rights reserved.

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.WindowsAPICodePack.Sensors;

internal static class SensorNativeMethods
{
    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool SystemTimeToFileTime(
        ref SystemTime lpSystemTime,
        // ReSharper disable once RedundantNameQualifier
        out System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime);
}
