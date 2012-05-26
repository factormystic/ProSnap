using System.ComponentModel;
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
    }
}
