﻿using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace ProSnap.Uploading
{
    public class MultipartFormDataUploadService : IUploadService
    {
        public string Name { get; set; }

        public string EndpointUrl { get; set; }
        public NameValueCollection UploadValues { get; set; }

        public string ImageLinkXPath { get; set; }
        public string DeleteLinkXPath { get; set; }

        public bool isActive { get; set; }

        public event EventHandler UploadStarted = delegate { };
        public event EventHandler<UploaderProgressEventArgs> UploadProgress = delegate { };
        public event EventHandler<UploaderEndedEventArgs> UploadEnded = delegate { };

        public MultipartFormDataUploadService(string name)
        {
            this.Name = name;
            this.UploadValues = new NameValueCollection();
        }

        internal static MultipartFormDataUploadService Create()
        {
            return new MultipartFormDataUploadService("New Upload Service")
            {
                EndpointUrl = "Url To Service"
            };
        }

        public Task<UploaderEndedEventArgs> Upload(ExtendedScreenshot screenshot)
        {
            Trace.WriteLine("Starting upload process...", string.Format("MultipartFormDataUploadService.Upload [{0}]", System.Threading.Thread.CurrentThread.Name));

            var t = new TaskCompletionSource<UploaderEndedEventArgs>();

            try
            {
                using (var wc = new WebClient())
                {
                    wc.UploadProgressChanged += (s, e) =>
                    {
                        this.UploadProgress(this, new UploaderProgressEventArgs((e.BytesSent * 100) / e.TotalBytesToSend));
                    };

                    wc.UploadValuesCompleted += (s, e) =>
                    {
                        Trace.WriteLine("Upload complete.", string.Format("MultipartFormDataUploadService.Upload [{0}]", System.Threading.Thread.CurrentThread.Name));

                        XmlDocument ResponseDoc = new XmlDocument();
                        ResponseDoc.Load(new MemoryStream(e.Result));
                        ResponseDoc.Save(Configuration.LocalPath + @"\response.txt");

                        string ImageLinkUrl = null;
                        string DeleteLinkUrl = null;

                        XmlNodeList ImageLink = ResponseDoc.SelectNodes(this.ImageLinkXPath);
                        if (ImageLink.Count > 0)
                            ImageLinkUrl = ImageLink[0].InnerText;

                        XmlNodeList DeleteLink = ResponseDoc.SelectNodes(this.DeleteLinkXPath);
                        if (DeleteLink.Count > 0)
                            DeleteLinkUrl = DeleteLink[0].InnerText;

                        var result = new UploaderEndedEventArgs(ImageLinkUrl, DeleteLinkUrl);
                        this.UploadEnded(this, result);
                        t.SetResult(result);
                    };

                    Trace.WriteLine("Starting stream conversion... ", string.Format("MultipartFormDataUploadService.Upload [{0}]", System.Threading.Thread.CurrentThread.Name));

                    var ImageData = string.Empty;
                    using (var ms = screenshot.EditedScreenshotPNGImageStream())
                        ImageData = Convert.ToBase64String(ms.ToArray());

                    var CurrentUploadValues = new NameValueCollection();
                    foreach (var k in this.UploadValues.AllKeys.ToList())
                        CurrentUploadValues.Add(k, this.UploadValues[k].Replace("%i", ImageData));

                    Trace.WriteLine("Stream conversion complete.", string.Format("MultipartFormDataUploadService.Upload [{0}]", System.Threading.Thread.CurrentThread.Name));
                    Trace.WriteLine("Starting upload...", string.Format("MultipartFormDataUploadService.Upload [{0}]", System.Threading.Thread.CurrentThread.Name));

                    wc.UploadValuesAsync(new Uri(this.EndpointUrl), CurrentUploadValues);

                    this.UploadStarted(this, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception occurred during upload: {0}", ex.GetBaseException()), string.Format("MultipartFormDataUploadService.Upload [{0}]", System.Threading.Thread.CurrentThread.Name));

                var result = new UploaderEndedEventArgs(ex.GetBaseException());
                this.UploadEnded(this, result);
                t.SetResult(result);
            }

            return t.Task;
        }
    }
}