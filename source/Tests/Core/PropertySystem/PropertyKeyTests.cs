// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using Xunit;

namespace Tests;

public class PropertyKeyTests
{
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", 5)]
    public void ConstructorWithGuid(string formatIdString, int propertyId)
    {
        var formatId = new Guid(formatIdString);
        var pk = new PropertyKey(formatId, propertyId);

        Assert.Equal(formatId, pk.FormatId);
        Assert.Equal(propertyId, pk.PropertyId);
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", 5)]
    public void ConstructorWithString(string formatId, int propertyId)
    {
        var pk = new PropertyKey(formatId, propertyId);

        Assert.Equal(new Guid(formatId), pk.FormatId);
        Assert.Equal(propertyId, pk.PropertyId);
    }

    [Fact]
    public void ToStringReturnsExpectedString()
    {
        var guid = new Guid("00000000-1111-2222-3333-000000000000");
        var property = 1234;

        var key = new PropertyKey(guid, property);

        Assert.Equal<string>(
            "{" + guid + "}, " + property,
            key.ToString());
    }
}
