using System;
using ProSnap.ActionItems;

namespace ProSnap
{
    public class PreviewEventArgs : EventArgs
    {
        public IActionItem ActionItem { get; set; }
        public object Result { get; set; }
    }
}
