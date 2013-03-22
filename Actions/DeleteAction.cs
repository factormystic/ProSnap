using System.ComponentModel;
using System.Diagnostics;
using System.IO;

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
            Trace.WriteLine("Applying DeleteAction...", string.Format("DeleteAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));

            if (LatestScreenshot == null)
            {
                Trace.WriteLine("Latest Screenshot is null, continuing...", string.Format("DeleteAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));
                return null;
            }

            Trace.WriteLine("Deleting current image...", string.Format("DeleteAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));

            var i = Program.History.IndexOf(LatestScreenshot);
            Program.History.Remove(LatestScreenshot);

            if (File.Exists(LatestScreenshot.InternalFileName))
                File.Delete(LatestScreenshot.InternalFileName);

            if (Program.History.Count == 0)
            {
                Trace.WriteLine("This image was the last image, closing preview...", string.Format("DeleteAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));

                Program.Preview.FadeClose();
                return null;
            }
            else
            {
                Trace.WriteLine("Previewing previous image...", string.Format("DeleteAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));

                i += i < Program.History.Count ? 0 : -1;
                Program.Preview.Show(Program.History[i]);
                return Program.History[i];
            }
        }
    }
}
