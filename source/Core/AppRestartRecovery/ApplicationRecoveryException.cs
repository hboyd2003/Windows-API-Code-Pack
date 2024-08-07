using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Microsoft.WindowsAPICodePack.ApplicationServices;

/// <summary>
///     This exception is thrown when there are problems with registering, unregistering or updating applications using
///     Application Restart Recovery.
/// </summary>
public class ApplicationRecoveryException : ExternalException
{
    /// <summary>Default constructor.</summary>
    public ApplicationRecoveryException()
    {
    }

    /// <summary>Initializes an exception with a custom message.</summary>
    /// <param name="message">A custom message for the exception.</param>
    public ApplicationRecoveryException(string message) : base(message)
    {
    }

    /// <summary>Initializes an exception with custom message and inner exception.</summary>
    /// <param name="message">A custom message for the exception.</param>
    /// <param name="innerException">Inner exception.</param>
    public ApplicationRecoveryException(string message, Exception innerException)
        : base(message, innerException)
    {
        // Empty
    }

    /// <summary>Initializes an exception with custom message and error code.</summary>
    /// <param name="message">A custom message for the exception.</param>
    /// <param name="errorCode">An error code (hresult) from which to generate the exception.</param>
    public ApplicationRecoveryException(string message, int errorCode) : base(message, errorCode)
    {
    }
}
