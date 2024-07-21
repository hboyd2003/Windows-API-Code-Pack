using System.Drawing;
using System.IO;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.ShellExtensions;

namespace Tests.ShellExtensions;

public class StreamThumbnailProviderTestSample : ThumbnailProvider, IThumbnailFromStream
{
    #region IThumbnailFromStream Members

    public Bitmap ConstructBitmap(Stream stream, int sideSize)
    {
        return new Bitmap(sideSize, sideSize);
    }

    #endregion
}

public class FileThumbnailProviderTestSample : ThumbnailProvider, IThumbnailFromFile
{
    #region IThumbnailFromFile Members

    public Bitmap ConstructBitmap(FileInfo info, int sideSize)
    {
        return new Bitmap(sideSize, sideSize);
    }

    #endregion
}

public class ItemThumbnailProviderTestSample : ThumbnailProvider, IThumbnailFromShellObject
{
    #region IThumbnailFromShellObject Members

    public Bitmap ConstructBitmap(ShellObject shellObject, int sideSize)
    {
        return new Bitmap(sideSize, sideSize);
    }

    #endregion
}
