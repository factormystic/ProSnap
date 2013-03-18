using System;
using System.ComponentModel;

namespace ProSnap.ActionItems
{
    public class ApplyEditsAction : IActionItem, ISaveableActionConfiguration
    {
        public enum ApplicationMode { Automatic, On, Off };

        [DescriptionAttribute("Add an Aero style shadow around the edges.")]
        [DefaultValueAttribute(ApplicationMode.Automatic)]
        public ApplicationMode DefaultBorderShadow { get; set; }

        [DescriptionAttribute("Round off the corners with an alpha channel.")]
        [DefaultValueAttribute(ApplicationMode.Automatic)]
        public ApplicationMode DefaultBorderRounding { get; set; }

        [DescriptionAttribute("Include mouse cursor in the image.")]
        [DefaultValueAttribute(ApplicationMode.Off)]
        public ApplicationMode ShowMouseCursor { get; set; }

        #region IActionItem Members

        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.ApplyEdits; }
        }

        [Browsable(false)]
        public IActionItem Clone()
        {
            return new ApplyEditsAction()
            {
                DefaultBorderShadow = this.DefaultBorderShadow,
                DefaultBorderRounding = this.DefaultBorderRounding,
                ShowMouseCursor = this.ShowMouseCursor
            };
        }

        #endregion

        public ApplyEditsAction()
        {
            DefaultBorderShadow = ApplicationMode.Automatic;
            DefaultBorderRounding = ApplicationMode.Automatic;
            ShowMouseCursor = ApplicationMode.Off;
        }

        #region ISaveableConfiguration Members

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Write()
        {
            throw new NotImplementedException();
        }

        #endregion

        public ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot)
        {
            switch (this.DefaultBorderRounding)
            {
                case ApplyEditsAction.ApplicationMode.Automatic: LatestScreenshot.withBorderRounding = !LatestScreenshot.isMaximized && LatestScreenshot.isRounded && !(FMUtils.WinApi.Helper.OperatingSystem == FMUtils.WinApi.Helper.OperatingSystems.Win8); break;
                case ApplyEditsAction.ApplicationMode.On: LatestScreenshot.withBorderRounding = true; break;
                case ApplyEditsAction.ApplicationMode.Off: LatestScreenshot.withBorderRounding = false; break;
            }

            switch (this.DefaultBorderShadow)
            {
                case ApplyEditsAction.ApplicationMode.Automatic: LatestScreenshot.withBorderShadow = !LatestScreenshot.isMaximized; break;
                case ApplyEditsAction.ApplicationMode.On: LatestScreenshot.withBorderShadow = true; break;
                case ApplyEditsAction.ApplicationMode.Off: LatestScreenshot.withBorderShadow = false; break;
            }

            switch (this.ShowMouseCursor)
            {
                case ApplyEditsAction.ApplicationMode.Automatic: LatestScreenshot.withCursor = LatestScreenshot.CompositionRect.Contains(LatestScreenshot.CursorLocation); break;
                case ApplyEditsAction.ApplicationMode.On: LatestScreenshot.withCursor = true; break;
                case ApplyEditsAction.ApplicationMode.Off: LatestScreenshot.withCursor = false; break;
            }

            return LatestScreenshot;
        }
    }
}
