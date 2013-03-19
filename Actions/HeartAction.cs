using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace ProSnap.ActionItems
{
    class HeartAction : IActionItem
    {
        public enum Modes { Toggle, On, Off };

        [DescriptionAttribute("Determines how this action should behave when invoked.")]
        [DefaultValueAttribute(Modes.Toggle)]
        public Modes HeartMode { get; set; }

        #region IActionItem Members

        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.Heart; }
        }

        [Browsable(false)]
        public IActionItem Clone()
        {
            return new HeartAction()
            {
                HeartMode = this.HeartMode
            };
        }

        #endregion

        public HeartAction()
        {
            this.HeartMode = Modes.Toggle;
        }

        public ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot)
        {
            Trace.WriteLine("Applying HeartAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", System.Threading.Thread.CurrentThread.Name));

            switch (this.HeartMode)
            {
                case HeartAction.Modes.Toggle: LatestScreenshot.isFlagged = !LatestScreenshot.isFlagged; break;
                case HeartAction.Modes.On: LatestScreenshot.isFlagged = true; break;
                case HeartAction.Modes.Off: LatestScreenshot.isFlagged = false; break;
            }

            //todo: if LatestScreenshot == Preview Active Screenshot?
            Program.Preview.UpdateHeart();
            
            return LatestScreenshot;
        }
    }
}
