using System;
using System.Runtime.Serialization;

/* Unmerged change from project 'Core (netcoreapp3.0)'
Before:
using System.Text;
using System.Runtime.InteropServices;
After:
using System.Runtime.InteropServices;
using System.Text;
*/

/* Unmerged change from project 'Core (net462)'
Before:
using System.Text;
using System.Runtime.InteropServices;
After:
using System.Runtime.InteropServices;
using System.Text;
*/

/* Unmerged change from project 'Core (net472)'
Before:
using System.Text;
using System.Runtime.InteropServices;
After:
using System.Runtime.InteropServices;
using System.Text;
*/

namespace Microsoft.WindowsAPICodePack.ApplicationServices;

/// <summary>This exception is thrown when there are problems with getting piece of data within PowerManager.</summary>
public class PowerManagerException : Exception
{
    /// <summary>Default constructor.</summary>
    public PowerManagerException()
    {
    }

    /// <summary>Initializes an excpetion with a custom message.</summary>
    /// <param name="message">A custom message for the exception.</param>
    public PowerManagerException(string message) : base(message)
    {
    }

    /// <summary>Initializes an exception with custom message and inner exception.</summary>
    /// <param name="message">A custom message for the exception.</param>
    /// <param name="innerException">An inner exception on which to base this exception.</param>
    public PowerManagerException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
