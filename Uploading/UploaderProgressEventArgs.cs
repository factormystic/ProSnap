using System;

namespace ProSnap.Uploading
{
    public class UploaderProgressEventArgs : EventArgs
    {
        public ExtendedScreenshot Screenshot { get; private set; }
        public long Percent { get; private set; }

        public UploaderProgressEventArgs(ExtendedScreenshot screenshot, long percent)
        {
            this.Screenshot = screenshot;
            this.Percent = percent;
        }
    }
}
