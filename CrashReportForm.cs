using System.Diagnostics;
using System.Windows.Forms;

namespace ProSnap
{
    public partial class CrashReportForm : Form
    {
        public CrashReportForm()
        {
            InitializeComponent();
        }

        private void allCrashFeedback_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://factormystic.net/prosnap/feedback");
        }
    }
}
