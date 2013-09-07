using System.ComponentModel;
using System.Diagnostics;

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
            Trace.WriteLine("Applying HeartAction...", string.Format("HeartAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));

            if (LatestScreenshot == null)
            {
                Trace.WriteLine("Latest Screenshot is null, continuing...", string.Format("DeleteAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));
                return null;
            }

            switch (this.HeartMode)
            {
                case HeartAction.Modes.Toggle: LatestScreenshot.isFlagged = !LatestScreenshot.isFlagged; break;
                case HeartAction.Modes.On: LatestScreenshot.isFlagged = true; break;
                case HeartAction.Modes.Off: LatestScreenshot.isFlagged = false; break;
            }

            Program.Preview.GroomHeartIcon();
            
            return LatestScreenshot;
        }
    }
}
