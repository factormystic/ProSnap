using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProSnap.ActionItems
{
    public class TakeRegionScreenshotAction : IActionItem
    {
        [DescriptionAttribute("Specifies the screen coordinates for the screenshot. Alternately, you can use the region selector.")]
        public Rectangle Region { get; set; }

        [DescriptionAttribute("Use a sizable region selector to create the screenshot.")]
        [DefaultValueAttribute(true)]
        public bool UseRegionSelector { get; set; }

        #region IActionItem Members

        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.TakeRegionScreenshot; }
        }

        public IActionItem Clone()
        {
            return new TakeRegionScreenshotAction()
            {
                Region = this.Region,
                UseRegionSelector = this.UseRegionSelector
            };
        }

        #endregion

        public TakeRegionScreenshotAction()
        {
            Region = Rectangle.Empty;
            UseRegionSelector = true;
        }

        public ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot)
        {
            if (!this.UseRegionSelector)
            {
                if (this.Region.IsEmpty)
                    return LatestScreenshot;

                LatestScreenshot = new ExtendedScreenshot(this.Region);
                Program.History.Add(LatestScreenshot);

                return LatestScreenshot;
            }

            Trace.WriteLine("Opening region selector...", string.Format("TakeRegionScreenshotAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));

            var t = new TaskCompletionSource<object>();

            var fceh = new FormClosedEventHandler((s, e) =>
            {
                Trace.WriteLine("Closed region selector.", string.Format("TakeRegionScreenshotAction.Invoke.FormClosed [{0}]", System.Threading.Thread.CurrentThread.Name));

                if (Program.Selector.SnapshotRectangle.IsEmpty)
                    return;

                LatestScreenshot = new ExtendedScreenshot(Program.Selector.SnapshotRectangle);
                Program.History.Add(LatestScreenshot);

                t.SetResult(null);
            });

            Program.Selector.FormClosed += fceh;
            Program.Selector.BeginInvoke(new MethodInvoker(() => Program.Selector.Show()));

            Task.WaitAll(t.Task);

            Program.Selector.FormClosed -= fceh;

            return LatestScreenshot;
        }

    }
}
