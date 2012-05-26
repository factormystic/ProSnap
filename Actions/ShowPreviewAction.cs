using System;
using System.ComponentModel;

namespace ProSnap.ActionItems
{
    public class ShowPreviewAction : IActionItem
    {
        public enum PreviewMonitors { Default, SameAsScreenshottedWindow, MonitorOne, MonitorTwo, MonitorThree };

        //[DefaultValueAttribute(PreviewMonitors.Default)]
        //public PreviewMonitors MonitorType { get; set; }

        //[DefaultValueAttribute(5)]
        //public int Delay { get; set; }

        #region IActionItem Members

        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.ShowPreview; }
        }

        [Browsable(false)]
        public IActionItem Clone()
        {
            return new ShowPreviewAction();
        }

        #endregion


        public ShowPreviewAction()
        {
            //MonitorType = PreviewMonitors.Default;
            //Delay = 5;
        }

    }
}
