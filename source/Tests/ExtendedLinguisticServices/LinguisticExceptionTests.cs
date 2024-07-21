// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Runtime.InteropServices;
using Microsoft.WindowsAPICodePack.ExtendedLinguisticServices;
using Xunit;

namespace Tests;

public class LinguisticExceptionTests
{
    [Fact]
    public void DefaultCtorInitializesWithLastError()
    {
        SetLastError(0);
        var e = new LinguisticException();

        Assert.True(e.Message == string.Empty,
            "e.Message is supposed to be empty. Instead, it is \"" + e.Message + "\"");
        // BUG: Doesn't initialize with last error, or another value is being set to last error after SetLastError(0) is called.
    }

    [Theory]
    [InlineData("")]
    [InlineData("Sample message")]
    [InlineData("Localized message - локализирано съобщение")]
    public void ConstructorWithMessage(string message)
    {
        var e = new LinguisticException(message);

        Assert.Equal<string>(e.Message, message);
        Assert.Equal<string>(e.ResultState.ErrorMessage, message);
        Assert.Equal(e.InnerException, null);
    }

    [Theory]
    [InlineData("", null)]
    [InlineData("Sample message", null)]
    public void ConstructorWithMessageAndInnerException(string message, Exception innerException)
    {
        var e = new LinguisticException(message, innerException);

        Assert.Equal<string>(e.Message, message);
        Assert.Equal<string>(e.ResultState.ErrorMessage, message);
        Assert.Equal(e.InnerException, innerException);
    }


    [DllImport("kernel32.dll")]
    private static extern void SetLastError(int error);
}
