using System.Linq;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace ProSnap.ActionItems
{
    public class SaveAction : IActionItem
    {
        [DescriptionAttribute("File path to save the image. See documentation for available path variables.")]
        public string FilePath { get; set; }

        [DescriptionAttribute("Determines if a save dialog prompt should be used to ask for file path.")]
        public bool Prompt { get; set; }

        #region IActionItem Members

        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.Save; }
        }

        [Browsable(false)]
        public IActionItem Clone()
        {
            return new SaveAction()
            {
                FilePath = this.FilePath,
                Prompt = this.Prompt
            };
        }

        #endregion

        public SaveAction()
        {
            FilePath = Path.Combine(Configuration.DefaultSaveDirectory, Configuration.DefaultFileName);
            Prompt = false;
        }

        public ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot)
        {
            Trace.WriteLine("Applying SaveAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

            if (this.Prompt)
            {
                Trace.WriteLine(string.Format("Prompting for save: {0}", this.Prompt), string.Format("Program.DoActionItem Save [{0}]", Thread.CurrentThread.Name));

                Task.WaitAll(Task.Factory.StartNew(() =>
                {
                    var t = new TaskCompletionSource<object>();

                    Program.Preview.SaveComplete += (ss, se) => t.SetResult(null);
                    Program.Preview.Save();

                    return t.Task;
                }));

                return LatestScreenshot;
            }

            string FileName = Environment.ExpandEnvironmentVariables(Helper.ExpandParameters(this.FilePath, LatestScreenshot));
            if (string.IsNullOrEmpty(FileName))
                MessageBox.Show(string.Format("There was a problem saving your screenshot:\r\n\r\n{0}", "No file name has been provided."), "ProSnap", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

            try
            {
                var dir = Path.GetDirectoryName(FileName);
                if (!Directory.Exists(dir))
                {
                    Trace.WriteLine("Attempting to create directory...", string.Format("Program.DoActionItem Save [{0}]", Thread.CurrentThread.Name));
                    Directory.CreateDirectory(dir);
                }

                LatestScreenshot.ComposedScreenshotImage.Save(FileName, Helper.ExtToImageFormat(Path.GetExtension(FileName)));
                LatestScreenshot.SavedFileName = FileName;
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("There was a problem saving your screenshot:\r\n\r\n{0}", e.GetBaseException().Message), "ProSnap", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            return LatestScreenshot;
        }
    }
}
