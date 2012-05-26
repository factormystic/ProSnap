using System;

namespace ProSnap.Uploading
{
    public class UploaderProgressEventArgs : EventArgs
    {
        public long Percent { get; private set; }

        public UploaderProgressEventArgs(long percent)
        {
            this.Percent = percent;
        }
    }
}
