﻿//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Runtime.InteropServices;
using MS.WindowsAPICodePack.Internal;

namespace Microsoft.WindowsAPICodePack.ApplicationServices;

internal static class AppRestartRecoveryNativeMethods
{
    internal static InternalRecoveryCallback InternalCallback { get; } = InternalRecoveryHandler;

    [DllImport("kernel32.dll")]
    internal static extern void ApplicationRecoveryFinished(
        [MarshalAs(UnmanagedType.Bool)] bool success);

    [DllImport("kernel32.dll")]
    [PreserveSig]
    internal static extern HResult ApplicationRecoveryInProgress(
        [Out] [MarshalAs(UnmanagedType.Bool)] out bool canceled);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    [PreserveSig]
    internal static extern HResult RegisterApplicationRecoveryCallback(
        InternalRecoveryCallback callback, IntPtr param,
        uint pingInterval,
        uint flags);

    [DllImport("kernel32.dll")]
    [PreserveSig]
    internal static extern HResult RegisterApplicationRestart(
        [MarshalAs(UnmanagedType.BStr)] string commandLineArgs,
        RestartRestrictions flags);

    // Unused.
    [DllImport("kernel32.dll")]
    [PreserveSig]
    internal static extern HResult UnregisterApplicationRecoveryCallback();

    [DllImport("kernel32.dll")]
    [PreserveSig]
    internal static extern HResult UnregisterApplicationRestart();

    private static uint InternalRecoveryHandler(IntPtr parameter)
    {
        ApplicationRecoveryInProgress(out var cancelled);

        var handle = GCHandle.FromIntPtr(parameter);
        var data = handle.Target as RecoveryData;
        data.Invoke();
        handle.Free();

        return 0;
    }

    internal delegate uint InternalRecoveryCallback(IntPtr state);
}
