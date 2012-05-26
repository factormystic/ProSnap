using System.ComponentModel;
using System.IO;

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
    }
}
