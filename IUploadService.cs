using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ProSnap.Uploading
{
    public interface IUploadService
    {
        string Name { get; }

        string EndpointUrl { get; }
        NameValueCollection UploadValues { get; }

        string ImageLinkXPath { get; }
        string DeleteLinkXPath { get; }

        bool isActive { get; set; }

        event EventHandler<UploaderProgressEventArgs> UploadStarted;
        event EventHandler<UploaderProgressEventArgs> UploadProgress;
        event EventHandler<UploaderEndedEventArgs> UploadEnded;

        Task<UploaderEndedEventArgs> Upload(ExtendedScreenshot screenshot);
    }
}
