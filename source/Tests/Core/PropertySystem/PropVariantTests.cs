// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MS.WindowsAPICodePack.Internal;
using Xunit;

namespace Tests;

public class PropVariantTests
{
    /// <summary>
    ///     Enumeration of values used for testing tht Propvariant.FromObject test.
    /// </summary>
    public static IEnumerable<object[]> FromObjectTestValues
    {
        get
        {
            object[] values =
            {
                new object[] { "hello", VarEnum.VT_LPWSTR },
                new object[] { new[] { "hello", "world" }, VarEnum.VT_VECTOR | VarEnum.VT_LPWSTR },
                new object[] { new[] { true, false }, VarEnum.VT_VECTOR | VarEnum.VT_BOOL },
                new object[] { new short[] { 1, 2, 3 }, VarEnum.VT_VECTOR | VarEnum.VT_I2 },
                new object[] { new ushort[] { 1, 2, 3 }, VarEnum.VT_VECTOR | VarEnum.VT_UI2 },
                new object[] { new[] { 1, 2, 3 }, VarEnum.VT_VECTOR | VarEnum.VT_I4 },
                new object[] { new uint[] { 1, 2, 3 }, VarEnum.VT_VECTOR | VarEnum.VT_UI4 },
                new object[] { new long[] { 1, 2, 3 }, VarEnum.VT_VECTOR | VarEnum.VT_I8 },
                new object[] { new ulong[] { 1, 2, 3 }, VarEnum.VT_VECTOR | VarEnum.VT_UI8 },
                new object[] { new[] { .123, 1.23, 12.3 }, VarEnum.VT_VECTOR | VarEnum.VT_R8 },
                new object[]
                    { new DateTime[] { new(2010, 1, 1), new(2000, 1, 1) }, VarEnum.VT_VECTOR | VarEnum.VT_FILETIME },
                new object[] { true, VarEnum.VT_BOOL },
                new object[] { DateTime.Now, VarEnum.VT_FILETIME },
                new object[] { (byte)123, VarEnum.VT_UI1 },
                new object[] { (sbyte)123, VarEnum.VT_I1 },
                new object[] { (short)123, VarEnum.VT_I2 },
                new object[] { (ushort)123, VarEnum.VT_UI2 },
                new object[] { 123, VarEnum.VT_I4 },
                new object[] { (uint)123, VarEnum.VT_UI4 },
                new object[] { (decimal)123, VarEnum.VT_DECIMAL },
                new object[] { (long)123, VarEnum.VT_I8 },
                new object[] { (ulong)123, VarEnum.VT_UI8 },
                new object[] { 12.3, VarEnum.VT_R8 }
            };

            foreach (object[] value in values) yield return value;
        }
    }

    // This is for use with the PropertyData attribute, each enumeration returns a set of input values for
    // the theory calling this from its PropertyData attribute.
    public static IEnumerable<object[]> InputObjectValues
    {
        get
        {
            object[] values =
            {
                new object[] { true },
                //new object[]{new bool[] { true, false }}, // BUG: PropVariant.SetBoolVector does not work.
                new object[] { (byte)123 },
                new object[] { (decimal)1.23 },
                new object[] { DateTime.Now },
                //Datetime's must be after 1970
                new object[] { new[] { DateTime.Now, DateTime.MaxValue } },
                new object[] { 123 },
                new object[] { new[] { 1, 2, 3 } },
                new object[] { (long)123 },
                new object[] { new long[] { 1, 2, 3 } },
                new object[] { (sbyte)123 },
                new object[] { (short)123 },
                new object[] { new short[] { 1, 2, 3 } },
                new object[] { "hello" },
                new object[] { new[] { "hello", "world" } },
                new object[] { (uint)123 },
                new object[] { new uint[] { 1, 2, 3 } },
                new object[] { (ulong)123 },
                new object[] { new ulong[] { 1, 2, 3 } },
                new object[] { (ushort)123 },
                new object[] { new ushort[] { 1, 2, 3 } }
            };


            foreach (object[] value in values) yield return value;
        }
    }
    // BUG: PropVariantTests - Its comment says its intended for internal use only, however is publicly accessible.

    // BUG: Xunit bug, Assert.Equal() documentation says Equal(expected, actual), when the UI
    // actually treats it as Equal(actual, expected).
    // This doesn't affect usability, but makes for some interesting error messages.

    [Theory]
    [MemberData("FromObjectTestValues")]
    public void StaticFromObjectTest(object inputObject, VarEnum expectedVarType)
    {
        using (var pv = PropVariant.FromObject(inputObject))
        {
            Assert.Equal(pv.Value, inputObject);
            Assert.Equal(pv.VarType, expectedVarType);
            Assert.False(pv.IsNullOrEmpty);
        }
    }

    [Theory]
    [MemberData("InputObjectValues")]
    public void SetEmptyValueTest(object inputObject)
    {
        using (var pv = PropVariant.FromObject(inputObject))
        {
            Assert.NotNull(pv.Value);
            Assert.NotEqual(VarEnum.VT_EMPTY, pv.VarType);
            Assert.False(pv.IsNullOrEmpty);

            pv.Dispose();

            Assert.Null(pv.Value);
            Assert.Equal(VarEnum.VT_EMPTY, pv.VarType);
            Assert.True(pv.IsNullOrEmpty);
        }
    }

    [Theory]
    [MemberData("InputObjectValues")]
    public void ClearTest(object inputObject)
    {
        using (var pv = PropVariant.FromObject(inputObject))
        {
            Assert.NotNull(pv.Value);
            Assert.NotEqual(VarEnum.VT_EMPTY, pv.VarType);
            Assert.False(pv.IsNullOrEmpty);

            pv.Dispose();

            Assert.Null(pv.Value);
            Assert.Equal(VarEnum.VT_EMPTY, pv.VarType);
            Assert.True(pv.IsNullOrEmpty);
        }
    }

    [Fact]
    public void FromObjectNullTest()
    {
        using (var pv = new PropVariant())
        {
            Assert.True(pv.IsNullOrEmpty);
            Assert.Equal(VarEnum.VT_EMPTY, pv.VarType);
            Assert.Null(pv.Value);
        }
    }

    [Fact]
    public void DefaultConstructorTest()
    {
        using (var pv = new PropVariant())
        {
            Assert.True(pv.IsNullOrEmpty);
            Assert.Equal(VarEnum.VT_EMPTY, pv.VarType);
            Assert.Null(pv.Value);
        }
    }
}
