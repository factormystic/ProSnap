using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using FMUtils.WinApi;

namespace ProSnap.ActionItems
{
    class RunAction : IActionItem
    {
        public enum Modes { ShellVerb, FilePath }

        [DescriptionAttribute("Determines how this action should behave when invoked.")]
        [DefaultValueAttribute(Modes.FilePath)]
        public Modes Mode { get; set; }

        [DescriptionAttribute("When using ShellVerb mode, runs the specified verb.")]
        public string ShellVerb { get; set; }

        [DescriptionAttribute("When using FilePath mode, runs the specified application or script. See documentation for available path variables.")]
        public string ApplicationPath { get; set; }

        [DescriptionAttribute("If provided, specifices the command line parameters for the new process. See documentation for available path variables.")]
        public string Parameters { get; set; }

        [DescriptionAttribute("If provided, specifices the working directory for the new process. See documentation for available path variables.")]
        public string WorkingDirectory { get; set; }

        [DescriptionAttribute("If the Application Path is 'cmd.exe', specifies if the command prompt window should be hidden while the command is running.")]
        [DefaultValueAttribute(true)]
        public bool HideCommandPrompt { get; set; }

        #region IActionItem Members

        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.Run; }
        }

        [Browsable(false)]
        public IActionItem Clone()
        {
            return new RunAction()
            {
                Mode = this.Mode,
                ShellVerb = this.ShellVerb,
                ApplicationPath = this.ApplicationPath,
                Parameters = this.Parameters,
                WorkingDirectory = this.WorkingDirectory,
                HideCommandPrompt = this.HideCommandPrompt,
            };
        }

        #endregion

        public RunAction()
        {
            this.Mode = Modes.FilePath;
            this.HideCommandPrompt = true;
        }

        public ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot)
        {
            if (!File.Exists(LatestScreenshot.InternalFileName))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LatestScreenshot.InternalFileName));
                LatestScreenshot.ComposedScreenshotImage.Save(LatestScreenshot.InternalFileName, ImageFormat.Png);
            }

            var parameters = Helper.ExpandParameters(this.Parameters, LatestScreenshot);
            var working = Environment.ExpandEnvironmentVariables(Helper.ExpandParameters(this.WorkingDirectory, LatestScreenshot));

            switch (this.Mode)
            {
                case RunAction.Modes.ShellVerb:
                    {
                        Windowing.ShellExecute(IntPtr.Zero, this.ShellVerb, LatestScreenshot.InternalFileName, parameters, working, Windowing.ShowCommands.SW_NORMAL);
                    } break;
                case RunAction.Modes.FilePath:
                    {
                        var psi = new ProcessStartInfo(Environment.ExpandEnvironmentVariables(Helper.ExpandParameters(this.ApplicationPath, LatestScreenshot)), parameters)
                        {
                            UseShellExecute = false,
                            WorkingDirectory = working,
                            CreateNoWindow = this.HideCommandPrompt,
                        };

                        Process.Start(psi);
                    } break;
            }

            return LatestScreenshot;
        }
    }
}
