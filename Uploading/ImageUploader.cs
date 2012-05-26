using System;
using System.Threading;

namespace ProSnap.Uploading
{
    public abstract class ImageUploader
    {
        public delegate void UploaderProgressEventHandler(object sender, UploaderProgressEventArgs e);
        public delegate void UploaderEndedEventHandler(object sender, UploaderEndedEventArgs e);

        public event EventHandler UploadStarted = delegate { };
        public event UploaderProgressEventHandler UploadProgress = delegate { };
        public event UploaderEndedEventHandler UploadEnded = delegate { };

        public virtual bool InProgress { get; protected set; }

        private SynchronizationContext _context;

        public abstract bool Upload(UploadService activeService, ExtendedScreenshot screenshot);

        protected ImageUploader()
        {
            _context = SynchronizationContext.Current;
        }

        protected void OnUploadStarted(EventArgs e)
        {
            _context.Post(o => UploadStarted(this, e), null);
        }

        protected void OnUploadProgress(UploaderProgressEventArgs e)
        {
            _context.Post(o => UploadProgress(this, e), null);
        }

        protected void OnUploadEnded(UploaderEndedEventArgs e)
        {
            _context.Post(o => UploadEnded(this, e), null);
        }
    }
}
