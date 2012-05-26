using System.ComponentModel;

namespace ProSnap.ActionItems
{
    class DeleteAction : IActionItem
    {
        #region IActionItem Members

        [Browsable(false)]
		public ActionTypes ActionType
        {
            get { return ActionTypes.Delete; }
        }

        [Browsable(false)]
        public IActionItem Clone()
        {
            return new DeleteAction();
        }

        #endregion
    }
}
