using System;

namespace ProSnap.Uploading
{
    public class UploaderEndedEventArgs
    {
        public string ImageLinkUrl { get; private set; }
        public string DeleteLinkUrl { get; private set; }

        public Exception exception { get; private set; }

        public bool IsSuccess
        {
            get
            {
                return exception == null;
            }
        }

        public UploaderEndedEventArgs(string imageLinkUrl, string deleteLinkUrl)
        {
            this.ImageLinkUrl = imageLinkUrl;
            this.DeleteLinkUrl = deleteLinkUrl;
        }

        public UploaderEndedEventArgs(Exception exception)
        {
            this.exception = exception;
        }
    }
}
