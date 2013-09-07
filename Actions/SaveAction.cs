using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            Trace.WriteLine("Applying SaveAction...", string.Format("SaveAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));

            if (LatestScreenshot == null)
            {
                Trace.WriteLine("Latest Screenshot is null, continuing...", string.Format("SaveAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));
                return null;
            }

            if (this.Prompt)
            {
                Trace.WriteLine(string.Format("Prompting for save: {0}", this.Prompt), string.Format("SaveAction.Invoke Save [{0}]", System.Threading.Thread.CurrentThread.Name));

                var save = new Action(() =>
                {
                    Program.Preview.isSaveDialogOpen = true;

                    var SaveDialog = new SaveFileDialog()
                    {
                        Filter = Configuration.FileDialogFilter,
                        FilterIndex = Configuration.DefaultFilterIndex + 1,
                    };

                    if (string.IsNullOrEmpty(LatestScreenshot.SavedFileName))
                    {
                        SaveDialog.FileName = Environment.ExpandEnvironmentVariables(Helper.ExpandParameters(Configuration.DefaultFileName, LatestScreenshot));
                    }
                    else
                    {
                        SaveDialog.FileName = Path.GetFileName(LatestScreenshot.SavedFileName);
                        SaveDialog.InitialDirectory = Path.GetDirectoryName(LatestScreenshot.SavedFileName);
                    }

                    if (SaveDialog.ShowDialog(Program.Preview) == DialogResult.OK)
                    {
                        LatestScreenshot.ComposedScreenshotImage.Save(SaveDialog.FileName, Helper.ExtToImageFormat(Path.GetExtension(SaveDialog.FileName)));
                        LatestScreenshot.SavedFileName = SaveDialog.FileName;
                    }

                    Program.Preview.isSaveDialogOpen = false;
                });

                if (Program.Preview.InvokeRequired)
                {
                    var t = new TaskCompletionSource<object>();
                    Program.Preview.BeginInvoke(new MethodInvoker(() =>
                    {
                        save();
                        t.SetResult(null);
                    }));

                    Task.WaitAll(t.Task);
                }
                else
                {
                    save();
                }

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
                    Trace.WriteLine("Attempting to create directory...", string.Format("Program.DoActionItem Save [{0}]", System.Threading.Thread.CurrentThread.Name));
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
