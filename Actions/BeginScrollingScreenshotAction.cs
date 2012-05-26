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
    }
}
