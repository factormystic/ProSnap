using System.ComponentModel;

namespace ProSnap.ActionItems
{
    class EndScrollingScreenshotAction : IActionItem
    {
        #region IActionItem Members

        [Browsable(false)]
		public ActionTypes ActionType
        {
            get { return ActionTypes.EndScrollingScreenshot; }
        }

        [Browsable(false)]
        public IActionItem Clone()
        {
            return new EndScrollingScreenshotAction();
        }

        #endregion
    }
}
