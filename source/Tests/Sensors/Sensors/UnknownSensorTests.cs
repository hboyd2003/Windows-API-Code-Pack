// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using Microsoft.WindowsAPICodePack.Sensors;
using Xunit;

namespace Tests;

public class UnknownSensorTests
{
    [Fact]
    public void ConstructAndConfirmThatGettersThrow()
    {
        var us = new UnknownSensor();

        Assert.Throws<SensorPlatformException>(() =>
        {
            var b = us.AutoUpdateDataReport;
        }); // BUG: Inconsistency with the rest of the API
        Assert.Throws<NullReferenceException>(() =>
        {
            var g = us.CategoryId;
        });
        Assert.Throws<NullReferenceException>(() =>
        {
            var t = us.ConnectionType;
        });
        Assert.Equal(null, us.DataReport); // BUG: Inconsistency
        Assert.Throws<NullReferenceException>(() =>
        {
            var s = us.Description;
        });
        Assert.Throws<NullReferenceException>(() =>
        {
            var s = us.DevicePath;
        });
        Assert.Throws<NullReferenceException>(() =>
        {
            var s = us.FriendlyName;
        });
        Assert.Throws<NullReferenceException>(() =>
        {
            var s = us.Manufacturer;
        });
        Assert.Throws<NullReferenceException>(() =>
        {
            var u = us.MinimumReportInterval;
        });
        Assert.Throws<NullReferenceException>(() =>
        {
            var s = us.Model;
        });
        Assert.Throws<NullReferenceException>(() =>
        {
            var u = us.ReportInterval;
        });
        Assert.Throws<NullReferenceException>(() =>
        {
            var g = us.SensorId;
        });
        Assert.Throws<NullReferenceException>(() =>
        {
            var s = us.SerialNumber;
        });
        Assert.Throws<NullReferenceException>(() =>
        {
            var s = us.State;
        });
        Assert.Throws<NullReferenceException>(() =>
        {
            var g = us.TypeId;
        });
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void AutoUpdateDataReportPropertySetterThrows(bool autoUpdate)
    {
        var us = new UnknownSensor();
        Assert.Throws<SensorPlatformException>(() =>
        {
            us.AutoUpdateDataReport = autoUpdate;
        }); // BUG: Inconsistency with the rest of the API
    }

    [Theory]
    [InlineData(uint.MinValue)]
    [InlineData(uint.MinValue + 1)]
    [InlineData(uint.MinValue + 2)]
    [InlineData(uint.MaxValue - 1)]
    [InlineData(uint.MaxValue)]
    public void ReportIntervalPropertySetterThrows(uint interval)
    {
        var us = new UnknownSensor();
        Assert.Throws<NullReferenceException>(() => { us.ReportInterval = interval; });
    }
}
