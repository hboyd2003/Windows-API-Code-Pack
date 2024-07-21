// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Threading;
using Microsoft.WindowsAPICodePack.ApplicationServices;
using Xunit;

namespace Tests;

public class PowerManagerTests
{
    [Fact]
    public void BatteryLifePercentIsValid()
    {
        Assert.InRange(PowerManager.BatteryLifePercent, 0, 100);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void MonitorRequiredPropertyWorks(bool newValue)
    {
        var originalValue = PowerManager.MonitorRequired;

        PowerManager.MonitorRequired = newValue;
        Assert.Equal(newValue, PowerManager.MonitorRequired);

        PowerManager.MonitorRequired = originalValue;
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void RequestBlockSleepPropertyWorks(bool newValue)
    {
        var originalValue = PowerManager.RequestBlockSleep;

        PowerManager.RequestBlockSleep = newValue;
        Assert.Equal(newValue, PowerManager.RequestBlockSleep);

        PowerManager.RequestBlockSleep = originalValue;
    }

    [Theory]
    [InlineData(true, PowerSource.AC)]
    [InlineData(false, PowerSource.Battery)]
    public void PowerSourceCorrespondsToAcOnlineProperty(bool acOnlineState, PowerSource powerSourceState)
    {
        Assert.Equal(
            acOnlineState == PowerManager.GetCurrentBatteryState().ACOnline,
            powerSourceState == PowerManager.PowerSource);
    }

    [Theory]
    [InlineData(true, PowerSource.Ups)]
    public void PowerSourceCorrespondsToIsUpsPresentProperty(bool isUpsPresentState, PowerSource powerSourceState)
    {
        // BUG: It is strange that IsUpsPresent is exposed on PM, while ACOnline is exposed on the BatteryState property
        Assert.Equal(
            isUpsPresentState == PowerManager.IsUpsPresent,
            powerSourceState == PowerManager.PowerSource);
    }

    [Fact]
    public void PowerPersonalityPropertyDoesNotThrow()
    {
        var exception = Record.Exception(() => PowerManager.PowerPersonality);
        Assert.Null(exception);
    }

    [Theory(Skip = "Event dependent property does not return before timeout on some computers.")]
    [InlineData(true, true)]
    // [InlineData(false, false)] // BUG: Possible bug
    public void IsMonitorOnPropertyWorks(bool monitorRequired, bool expectedIsMonitorOn)
    {
        var monitorRequiredOriginal = PowerManager.MonitorRequired;

        PowerManager.MonitorRequired = monitorRequired;
        Assert.Equal(expectedIsMonitorOn, PowerManager.IsMonitorOn);

        PowerManager.MonitorRequired = monitorRequiredOriginal;
    }

    // TODO: Remove the skip attribute when test is complete.
    [Theory(Skip = "Not Implemented")]
    [InlineData(true)]
    [InlineData(false)]
    public void IsMonitorOnChangedEventWorks(bool isMonitorOnValueToSet)
    {
        var eventFired = false;
        PowerManager.IsMonitorOnChanged += (sender, args) => { eventFired = true; };

        // TODO: Fire PowerManager.IsMonitorOnChanged event

        var secTimeout = 5; //wait 5 seconds for event to be fired.
        for (var i = 0; i < secTimeout * 10 && !eventFired; i++) Thread.Sleep(100);

        Assert.True(eventFired);
    }

    // TODO: Remove the skip attribute when test is complete.
    [Theory(Skip = "Not Implemented")]
    [InlineData(PowerPersonality.Automatic)]
    [InlineData(PowerPersonality.HighPerformance)]
    [InlineData(PowerPersonality.PowerSaver)]
    public void PowerPersonalityChangedEventWorks(PowerPersonality powerPersonalityToSet)
    {
        var original = PowerManager.PowerPersonality;
        var eventFired = false;

        PowerManager.PowerPersonalityChanged += (sender, e) => { eventFired = true; };

        // TODO: Change PowerManager.PowerPersonality, it is readonly.

        var secTimeout = 5; //wait 5 seconds for event to be fired.
        for (var i = 0; i < secTimeout * 10 && !eventFired; i++) Thread.Sleep(100);

        // TODO: PowerManager.PowerPersonality = original;

        Assert.True(eventFired);
    }

    // TODO: Remove skip attribute when test is complete.
    [Theory(Skip = "Not Implemented")]
    [InlineData(PowerSource.AC)]
    [InlineData(PowerSource.Battery)]
    [InlineData(PowerSource.Ups)]
    public void PowerSourceChangedEventWorks(PowerSource powerSourceToSet)
    {
        var eventFired = false;
        PowerManager.PowerSourceChanged += (sender, args) => { eventFired = true; };

        // TODO: Fire Powermanager.PowerSourceChanged event.

        var secTimeout = 5; //wait 5 seconds for event to be fired.
        for (var i = 0; i < secTimeout * 10 && !eventFired; i++) Thread.Sleep(100);

        Assert.True(eventFired);
    }

    // TODO: Remove skip attribute when test is complete.
    [Fact(Skip = "Not Implemented")]
    public void SystemBusyChangedEventWorks()
    {
        var eventFired = false;
        PowerManager.SystemBusyChanged += (sender, args) => { eventFired = true; };

        // TODO: Fire PowerManager.SystemBusyChanged event.

        var secTimeout = 5; //wait 5 seconds for event to be fired.
        for (var i = 0; i < secTimeout * 10 && !eventFired; i++) Thread.Sleep(100);

        Assert.True(eventFired);
    }
}
