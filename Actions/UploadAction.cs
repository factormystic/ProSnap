using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        public ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot)
        {
            Trace.WriteLine("Applying UploadAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", System.Threading.Thread.CurrentThread.Name));

            var ActiveService = Configuration.UploadServices.FirstOrDefault(u => u.isActive);
            if (ActiveService == null)
                return LatestScreenshot;

            Task.WaitAll(ActiveService.Upload(LatestScreenshot).ContinueWith(t =>
            {
                if (t.Result.IsSuccess)
                {
                    LatestScreenshot.Remote.ImageLink = t.Result.ImageLinkUrl;
                    LatestScreenshot.Remote.DeleteLink = t.Result.DeleteLinkUrl;
                }

                return LatestScreenshot;
            }));

            return LatestScreenshot;
        }
    }
}
