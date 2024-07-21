// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using Microsoft.WindowsAPICodePack.ApplicationServices;
using Xunit;

namespace Tests;

public class BatteryStateTests
{
    [Fact]
    public void ConfirmBatteryState()
    {
        var s = PowerManager.GetCurrentBatteryState();

        Assert.InRange(s.CurrentCharge, 0, int.MaxValue);
        // TODO: add more tests with heuristics here (i.e. when not plugged in, est time remaining < a reasonable number, etc.)
        Assert.InRange(s.EstimatedTimeRemaining, TimeSpan.MinValue, TimeSpan.MaxValue);
        Assert.InRange(s.MaxCharge, 0, int.MaxValue);

        // The max values below are just numbers we picked.
        Assert.InRange(s.SuggestedBatteryWarningCharge, 0, 10000);
        Assert.InRange(s.SuggestedCriticalBatteryCharge, 0, 10000);
    }

    [Fact]
    public void DischargeRateIsNonNegativeIfPluggedIn()
    {
        var s = PowerManager.GetCurrentBatteryState();
        if (s.ACOnline) Assert.True(s.ChargeRate >= 0);
    }

    [Fact]
    public void DischargeRateIsNegativeIfNotPluggedIn()
    {
        var s = PowerManager.GetCurrentBatteryState();
        if (!s.ACOnline) Assert.True(s.ChargeRate < 0);
    }
}
