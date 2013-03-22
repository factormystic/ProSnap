using System;

namespace ProSnap.Uploading
{
    public class UploaderEndedEventArgs : EventArgs
    {
        public ExtendedScreenshot Screenshot { get; private set; }
        public Exception exception { get; private set; }

        public bool IsSuccess
        {
            get
            {
                return exception == null;
            }
        }

        public UploaderEndedEventArgs(ExtendedScreenshot screenshot)
        {
            this.Screenshot = screenshot;
        }

        public UploaderEndedEventArgs(Exception exception)
        {
            this.exception = exception;
        }
    }
}
