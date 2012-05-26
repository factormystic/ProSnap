using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;

namespace ProSnap.Uploading
{
    public class FormsUploader : ImageUploader
    {
        public UploadService ActiveService { get; private set; }

        public override bool Upload(UploadService activeService, ExtendedScreenshot screenshot)
        {
            if (InProgress)
                return false;

            Trace.WriteLine("Starting upload process...", string.Format("FormsUploader.Upload [{0}]", System.Threading.Thread.CurrentThread.Name));

            this.ActiveService = activeService;
            this.InProgress = true;
            OnUploadStarted(new EventArgs());

            try
            {
                using (var wc = new WebClient())
                {
                    wc.UploadProgressChanged += UploadProgressChanged;
                    wc.UploadValuesCompleted += UploadValuesCompleted;

                    var bg = new BackgroundWorker();
                    bg.DoWork += (o, a) =>
                        {
                            Trace.WriteLine("Starting stream conversion... ", string.Format("FormsUploader.Upload.DoWork [{0}]", System.Threading.Thread.CurrentThread.Name));

                            var ImageData = string.Empty;
                            using (var ms = screenshot.EditedScreenshotPNGImageStream())
                                ImageData = Convert.ToBase64String(ms.ToArray());

                            var CurrentUploadValues = new NameValueCollection();
                            foreach (var k in activeService.UploadValues.AllKeys.ToList())
                                CurrentUploadValues.Add(k, activeService.UploadValues[k].Replace("%i", ImageData));

                            Trace.WriteLine("Stream conversion complete.", string.Format("FormsUploader.Upload.DoWork [{0}]", System.Threading.Thread.CurrentThread.Name));
                            Trace.WriteLine("Starting async upload...", string.Format("FormsUploader.Upload.DoWork [{0}]", System.Threading.Thread.CurrentThread.Name));

                            wc.UploadValuesAsync(new Uri(activeService.EndpointUrl), CurrentUploadValues);

                            Trace.WriteLine("Upload start complete.", string.Format("FormsUploader.Upload.DoWork [{0}]", System.Threading.Thread.CurrentThread.Name));
                        };
                    bg.RunWorkerAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception occurred during upload: {0}", ex.GetBaseException()), string.Format("FormsUploader.Upload [{0}]", System.Threading.Thread.CurrentThread.Name));

                InProgress = false;
                OnUploadEnded(new UploaderEndedEventArgs(ex.GetBaseException()));

                return false;
            }
        }

        private void UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            OnUploadProgress(new UploaderProgressEventArgs((e.BytesSent * 100) / e.TotalBytesToSend));
        }

        private void UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            Trace.WriteLine("Upload complete, parsing results...", string.Format("FormsUploader.UploadValuesCompleted [{0}]", System.Threading.Thread.CurrentThread.Name));

            try
            {
                string ImageLinkUrl = null;
                string DeleteLinkUrl = null;

                XmlDocument ResponseDoc = new XmlDocument();
                ResponseDoc.Load(new MemoryStream(e.Result));
                ResponseDoc.Save(Configuration.LocalPath + @"\response.txt");

                XmlNodeList ImageLink = ResponseDoc.SelectNodes(ActiveService.ImageLinkXPath);
                if (ImageLink.Count > 0)
                    ImageLinkUrl = ImageLink[0].InnerText;

                XmlNodeList DeleteLink = ResponseDoc.SelectNodes(ActiveService.DeleteLinkXPath);
                if (DeleteLink.Count > 0)
                    DeleteLinkUrl = DeleteLink[0].InnerText;

                InProgress = false;
                OnUploadEnded(new UploaderEndedEventArgs(ImageLinkUrl, DeleteLinkUrl));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception occurred during upload complete processing: {0}", ex.GetBaseException()), string.Format("FormsUploader.UploadValuesCompleted [{0}]", System.Threading.Thread.CurrentThread.Name));

                InProgress = false;
                OnUploadEnded(new UploaderEndedEventArgs(ex.GetBaseException()));
            }

            Trace.WriteLine("Done.", string.Format("FormsUploader.UploadValuesCompleted [{0}]", System.Threading.Thread.CurrentThread.Name));
        }
    }
}