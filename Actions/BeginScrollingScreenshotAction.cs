using System.ComponentModel;

namespace ProSnap.ActionItems
{
    class BeginScrollingScreenshotAction : IActionItem
    {
        #region IActionItem Members

        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.BeginScrollingScreenshot; }
        }

        [Browsable(false)]
        public IActionItem Clone()
        {
            return new BeginScrollingScreenshotAction();
        }

        #endregion

        public ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot)
        {
            Program.isTakingScrollingScreenshot = true;

            Program.timelapse.Clear();
            Program.timelapse.Add(new ExtendedScreenshot());

            return LatestScreenshot;
        }
    }
}
