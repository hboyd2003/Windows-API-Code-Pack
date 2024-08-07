using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.ShellExtensions.Interop;
using HandlerNativeMethods = Microsoft.WindowsAPICodePack.Shell.Interop.HandlerNativeMethods;

namespace Microsoft.WindowsAPICodePack.ShellExtensions;

/// <summary>
///     This is the base class for all WinForms-based preview handlers and provides their basic functionality. To create a
///     custom preview
///     handler that contains a WinForms user control, a class must derive from this, use the
///     <see cref="PreviewHandlerAttribute" />, and
///     implement 1 or more of the following interfaces: <see cref="IPreviewFromStream" />,
///     <see cref="IPreviewFromShellObject" />, <see cref="IPreviewFromFile" />.
/// </summary>
public abstract class WinFormsPreviewHandler : PreviewHandler, IDisposable
{
    /// <summary>This control must be populated by the deriving class before the preview is shown.</summary>
    public UserControl Control { get; protected set; }

    /// <inheritdoc />
    protected override IntPtr Handle => Control.Handle;

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    ~WinFormsPreviewHandler()
    {
        Dispose(false);
    }

    /// <summary>
    ///     Provides means to dispose the object. When overridden, it is imperative that base.Dispose(true) is called within
    ///     the implementation.
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing && Control != null) Control.Dispose();
    }

    /// <summary>Called when an exception is thrown during initialization of the preview control.</summary>
    /// <param name="caughtException"></param>
    [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
        Justification =
            "The object remains reachable through the Controls collection which can be disposed at a later time.")]
    protected override void HandleInitializeException(Exception caughtException)
    {
        if (caughtException == null) throw new ArgumentNullException("caughtException");

        Control = new UserControl();
        Control.Controls.Add(new TextBox
        {
            ReadOnly = true,
            Multiline = true,
            Dock = DockStyle.Fill,
            Text = caughtException.ToString(),
            BackColor = Color.OrangeRed
        });
    }

    /// <inheritdoc />
    protected override void SetBackground(int argb)
    {
        Control.BackColor = Color.FromArgb(argb);
    }

    /// <inheritdoc />
    protected override void SetFocus()
    {
        Control.Focus();
    }

    /// <inheritdoc />
    protected override void SetFont(LogFont font)
    {
        Control.Font = Font.FromLogFont(font);
    }

    /// <inheritdoc />
    protected override void SetForeground(int argb)
    {
        Control.ForeColor = Color.FromArgb(argb);
    }

    /// <inheritdoc />
    protected override void SetParentHandle(IntPtr handle)
    {
        HandlerNativeMethods.SetParent(Control.Handle, handle);
    }

    /// <inheritdoc />
    protected override void UpdateBounds(NativeRect bounds)
    {
        Control.Bounds = Rectangle.FromLTRB(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
        Control.Visible = true;
    }
}
