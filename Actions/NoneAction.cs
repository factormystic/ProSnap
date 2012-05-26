using System.ComponentModel;

namespace ProSnap.ActionItems
{
    public class NoneAction : IActionItem
    {
        #region IActionItem Members

        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.None; }
        }

        [Browsable(false)]
        public IActionItem Clone()
        {
            return new NoneAction();
        }

        #endregion
    }
}
