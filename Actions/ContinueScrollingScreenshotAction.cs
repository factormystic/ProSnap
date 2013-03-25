using System.ComponentModel;

namespace ProSnap.ActionItems
{
    class ContinueScrollingScreenshotAction : IActionItem
    {
        #region IActionItem Members

        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.ContinueScrollingScreenshot; }
        }

        [Browsable(false)]
        public IActionItem Clone()
        {
            return new ContinueScrollingScreenshotAction();
        }

        #endregion

        public ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot)
        {
            if (Program.isTakingScrollingScreenshot)
                Program.timelapse.Add(new ExtendedScreenshot());

            return LatestScreenshot;
        }
    }
}
