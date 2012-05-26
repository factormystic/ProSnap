using System.ComponentModel;
using System.Drawing;

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
    }
}

