// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using System.Reflection;
using Microsoft.WindowsAPICodePack.Shell;
using Xunit;

namespace Tests;

public class KnownFolderHelperTests
{
    public static IEnumerable<object[]> KnownFoldersFromReflection
    {
        get
        {
            var staticKnownFolders = typeof(KnownFolders).GetProperties(BindingFlags.Static | BindingFlags.Public);
            foreach (var info in staticKnownFolders)
                if (info.PropertyType == typeof(IKnownFolder))
                {
                    IKnownFolder folder = null;
                    try
                    {
                        // the exception this can raise is caused by the path of the known folder
                        // not being found.
                        folder = (IKnownFolder)info.GetValue(null, null);
                    }
                    catch
                    {
                        continue;
                    }

                    yield return new object[] { folder };
                }
        }
    }

    [Theory]
    [MemberData("KnownFoldersFromReflection")]
    public void FromPathNameTest(IKnownFolder folder)
    {
        var test = KnownFolderHelper.FromPath(folder.Path);
        Assert.True(folder.FolderId == test.FolderId);
    }

    [Theory]
    [MemberData("KnownFoldersFromReflection")]
    public void FromParsingNameTest(IKnownFolder folder)
    {
        var test = KnownFolderHelper.FromParsingName(folder.ParsingName);
        Assert.True(folder.FolderId == test.FolderId);
    }

    [Theory]
    [MemberData("KnownFoldersFromReflection")]
    public void FromCanonicalNameTest(IKnownFolder folder)
    {
        var test = KnownFolderHelper.FromCanonicalName(folder.CanonicalName);
        Assert.True(folder.FolderId == test.FolderId);
    }
}
