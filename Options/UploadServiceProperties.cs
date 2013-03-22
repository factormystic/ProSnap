using System.Collections.Specialized;
using System.Windows.Forms;
using ProSnap.Uploading;

namespace ProSnap.Options
{
    public partial class UploadServiceProperties : Form
    {
        IUploadService SelectedUploadService;

        public UploadServiceProperties(IUploadService selectedUploadService)
        {
            InitializeComponent();

            this.MinimumSize = this.Size;
            this.SelectedUploadService = selectedUploadService;

            tbUploadServiceName.Text = SelectedUploadService.Name;
            tbUploadServiceUrl.Text = SelectedUploadService.EndpointUrl;

            foreach (var k in SelectedUploadService.UploadValues.Keys)
            {
                dataGridView1.Rows.Add(new [] { k, selectedUploadService.UploadValues[(string)k] });
            }

            tbImageLinkXPath.Text = SelectedUploadService.ImageLinkXPath;
            tbDeleteLinkXPath.Text = SelectedUploadService.DeleteLinkXPath;
        }

        public IUploadService GetResultUploadService()
        {
            return new MultipartFormDataUploadService(tbUploadServiceName.Text)
            {
                EndpointUrl = tbUploadServiceUrl.Text,
                UploadValues = GetUploadValues(),
                ImageLinkXPath = tbImageLinkXPath.Text,
                DeleteLinkXPath = tbDeleteLinkXPath.Text
            };
        }

        public NameValueCollection GetUploadValues()
        {
            var nvc = new NameValueCollection();
            foreach (DataGridViewRow r in dataGridView1.Rows)
                nvc.Add((string)r.Cells[0].Value, (string)r.Cells[1].Value);
        
            return nvc;
        }
    }
}
