﻿using System;
using System.Runtime.InteropServices.ComTypes;

namespace Tests.ShellExtensions;

public class ThumbnailProviderTests
{
    // TODO: Rewrite tests

    private class FakeIStream : IStream
    {
        #region IStream Members

        public void Clone(out IStream ppstm)
        {
            throw new NotImplementedException();
        }

        public void Commit(int grfCommitFlags)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
        {
            throw new NotImplementedException();
        }

        public void LockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        public void Read(byte[] pv, int cb, IntPtr pcbRead)
        {
            throw new NotImplementedException();
        }

        public void Revert()
        {
            throw new NotImplementedException();
        }

        public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
        {
            throw new NotImplementedException();
        }

        public void SetSize(long libNewSize)
        {
            throw new NotImplementedException();
        }

        public void Stat(out STATSTG pstatstg, int grfStatFlag)
        {
            throw new NotImplementedException();
        }

        public void UnlockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        public void Write(byte[] pv, int cb, IntPtr pcbWritten)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
