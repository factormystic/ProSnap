using System.ComponentModel;

namespace ProSnap.ActionItems
{
    class HidePreviewAction : IActionItem
    {
        #region IActionItem Members

        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.HidePreview; }
        }

        [Browsable(false)]
        public IActionItem Clone()
        {
            return new HidePreviewAction();
        }

        #endregion
    }
}
