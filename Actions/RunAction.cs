using System.ComponentModel;

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
                WorkingDirectory = this.WorkingDirectory
            };
        }

        #endregion

        public RunAction()
        {
            this.Mode = Modes.FilePath;
        }
    }
}
