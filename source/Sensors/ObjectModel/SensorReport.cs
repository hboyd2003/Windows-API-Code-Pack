// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.WindowsAPICodePack.Sensors;

/// <summary>
///     Represents all the data from a single sensor data report.
/// </summary>
public class SensorReport
{
    /// <summary>
    ///     Gets the time when the data report was generated.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "TimeStamp")]
    public DateTime TimeStamp { get; private set; }

    /// <summary>
    ///     Gets the data values in the report.
    /// </summary>
    public SensorData Values { get; private set; }

    /// <summary>
    ///     Gets the sensor that is the source of this data report.
    /// </summary>
    public Sensor Source { get; private set; }

    #region implementation

    internal static SensorReport FromNativeReport(Sensor originator, ISensorDataReport iReport)
    {
        var systemTimeStamp = new SystemTime();
        try
        {
            iReport.GetTimestamp(out systemTimeStamp);
        }
        catch (COMException ex)
        {
            Trace.WriteLine(ex);
            return null;
        }

        // ReSharper disable once RedundantNameQualifier
        var ftTimeStamp = new System.Runtime.InteropServices.ComTypes.FILETIME();
        SensorNativeMethods.SystemTimeToFileTime(ref systemTimeStamp, out ftTimeStamp);
        var lTimeStamp = ((long)ftTimeStamp.dwHighDateTime << 32) + ftTimeStamp.dwLowDateTime;
        var timeStamp = DateTime.FromFileTime(lTimeStamp);

        var sensorReport = new SensorReport();
        sensorReport.Source = originator;
        sensorReport.TimeStamp = timeStamp;
        sensorReport.Values = SensorData.FromNativeReport(originator.internalObject, iReport);

        return sensorReport;
    }

    #endregion
}
