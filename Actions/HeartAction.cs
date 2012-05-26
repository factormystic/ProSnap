using System.ComponentModel;

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
    }
}
