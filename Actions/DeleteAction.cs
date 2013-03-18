using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

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

        public ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot)
        {
            Trace.WriteLine("Applying DeleteAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

            //todo: if LatestScreenshot == Preview Active Screenshot?
            Program.Preview.Delete();

            return LatestScreenshot;
        }
    }
}
