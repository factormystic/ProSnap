using System.Collections.Specialized;

namespace ProSnap.Uploading
{
    public class UploadService
    {
        public string Name { get; private set; }

        public string EndpointUrl { get; set; }
        public NameValueCollection UploadValues;

        public string ImageLinkXPath { get; set; }
        public string DeleteLinkXPath { get; set; }

        public bool isActive { get; set; }

        public UploadService(string name)
        {
            this.Name = name;
            this.UploadValues = new NameValueCollection();
        }

        internal static UploadService Create()
        {
            return new UploadService("New Upload Service") { EndpointUrl = "Url To Service" };
        }
    }
}
