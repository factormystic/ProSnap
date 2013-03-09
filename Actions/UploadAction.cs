using System.ComponentModel;

namespace ProSnap.ActionItems
{
    class UploadAction : IActionItem
    {
        #region IActionItem Members

        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.Upload; }
        }

        [Browsable(false)]
        public IActionItem Clone()
        {
            return new UploadAction();
        }

        #endregion
    }
}
