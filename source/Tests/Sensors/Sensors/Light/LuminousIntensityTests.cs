// Copyright (c) Microsoft Corporation.  All rights reserved.

using Microsoft.WindowsAPICodePack.Sensors;
using Xunit;

namespace Tests;

public class LuminousIntensityTests
{
    [Fact]
    public void Construction()
    {
        var sr = new SensorReport();
        var li = new LuminousIntensity(sr);
        Assert.Equal(0, li.Intensity);
    }
}
