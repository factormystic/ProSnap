using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

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

        public ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot)
        {
            Trace.WriteLine("Applying HidePreviewAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", System.Threading.Thread.CurrentThread.Name));

            //todo: if LatestScreenshot == Preview Active Screenshot?
            Program.Preview.FadeClose();

            return LatestScreenshot;
        }
    }
}
