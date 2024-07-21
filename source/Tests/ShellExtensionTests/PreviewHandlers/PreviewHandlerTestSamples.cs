using System.IO;
using Microsoft.WindowsAPICodePack.ShellExtensions;
using ShellExtensionTests;

namespace Tests.ShellExtensions.PreviewHandlers;

public class WinformsPreviewHandlerTestSample : WinFormsPreviewHandler, IPreviewFromStream
{
    public WinformsPreviewHandlerTestSample()
    {
        Control = new WinFormsPreviewHandlerSampleForm();
    }


    #region IPreviewFromStream Members

    public void Load(Stream stream)
    {
    }

    #endregion
}
