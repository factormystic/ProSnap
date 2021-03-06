﻿using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProSnap.ActionItems
{
    class UploadAction : IActionItem
    {
        public enum Modes { UseSelectedDefault };

        [DescriptionAttribute("Determines which upload provider should be used to upload the screenshot.")]
        [DefaultValueAttribute(Modes.UseSelectedDefault)]
        public Modes UploadMode { get; set; }

        #region IActionItem Members

        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.Upload; }
        }

        [Browsable(false)]
        public IActionItem Clone()
        {
            return new UploadAction();
        }

        #endregion

        public ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot)
        {
            Trace.WriteLine("Applying UploadAction...", string.Format("UploadAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));

            if (LatestScreenshot == null)
            {
                Trace.WriteLine("Latest Screenshot is null, continuing...", string.Format("UploadAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));
                return null;
            }

            var ActiveService = Configuration.UploadServices.FirstOrDefault(u => u.isActive);
            if (ActiveService == null)
            {
                Trace.WriteLine("No active upload service has been configured", string.Format("UploadAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));

                MessageBox.Show(Program.Preview, "You must configure an upload service before uploading any screenshots.", "Upload a screenshot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            ActiveService.UploadStarted += Program.Preview.UploadStarted;
            ActiveService.UploadProgress += Program.Preview.UploadProgress;
            ActiveService.UploadEnded += Program.Preview.UploadEnded;

            Task.WaitAll(ActiveService.Upload(LatestScreenshot).ContinueWith(t =>
            {
                ActiveService.UploadStarted -= Program.Preview.UploadStarted;
                ActiveService.UploadProgress -= Program.Preview.UploadProgress;
                ActiveService.UploadEnded -= Program.Preview.UploadEnded;

                return LatestScreenshot;
            }));

            return LatestScreenshot;
        }
    }
}
