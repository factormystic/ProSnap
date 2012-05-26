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
    }
}
