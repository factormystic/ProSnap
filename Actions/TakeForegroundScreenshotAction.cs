using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FMUtils.Screenshot;

namespace ProSnap.ActionItems
{
    public class TakeForegroundScreenshotAction : IActionItem
    {
        [DescriptionAttribute("Internal screenshot creation method.")]
        [DefaultValueAttribute(ScreenshotMethod.Auto)]
        public ScreenshotMethod Method { get; set; }

        [DescriptionAttribute("When screenshot is created, make glassy window borders opaque.")]
        [DefaultValueAttribute(true)]
        public bool SolidGlass
        {
            get
            {
                return _solidglass;
            }
            set
            {
                _solidglass = value;
                if (!_solidglass)
                    Method = ScreenshotMethod.GDI;
            }
        }
        bool _solidglass;

        #region IActionItem Members
        
        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.TakeForegroundScreenshot; }
        }

        public IActionItem Clone()
        {
            return new TakeForegroundScreenshotAction()
            {
                Method = this.Method,
                SolidGlass = this.SolidGlass
            };
        }

        #endregion

        public TakeForegroundScreenshotAction()
        {
            Method = ScreenshotMethod.Auto;
            SolidGlass = true;
        }

        public ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot)
        {
            try
            {
                LatestScreenshot = new ExtendedScreenshot(this.Method, this.SolidGlass);
                Program.History.Add(LatestScreenshot);
                Program.Preview.GroomBackForwardIcons();
            }
            catch (Exception e)
            {
                Trace.WriteLine(string.Format("Exception in TakeForegroundScreenshot: {0}", e.GetBaseException()), string.Format("Program.DoActionItem [{0}]", System.Threading.Thread.CurrentThread.Name));

                ReportListener reporter = Trace.Listeners.Cast<TraceListener>().Where(tl => tl is ReportListener).FirstOrDefault() as ReportListener;
                File.WriteAllText(Path.Combine(Configuration.LocalPath, "report.txt"), string.Join("\n", reporter.Messages.Select(m => string.Format("{0} {1}: {2}", m.Timestamp, m.Category, m.Message)).ToArray()));
            }

            return LatestScreenshot;
        }
    }
}
