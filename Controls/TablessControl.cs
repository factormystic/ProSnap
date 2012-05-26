using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace ProSnap
{
    //cf. http://stackoverflow.com/a/6954785/1569
    class TablessControl : TabControl
    {
        [DefaultValueAttribute(true)]
        public bool HideTabs
        {
            get
            {
                return _hideTabs;
            }
            set
            {
                _hideTabs = value;
                this.ItemSize = new Size(this.ItemSize.Width, this.ItemSize.Height + 1);
                this.ItemSize = new Size(this.ItemSize.Width, this.ItemSize.Height - 1);
            }
        }
        bool _hideTabs = true;

        protected override void WndProc(ref Message m)
        {
            // Hide tabs by trapping the TCM_ADJUSTRECT message 
            if (m.Msg == 0x1328 && HideTabs) // !DesignMode)
                m.Result = (IntPtr)1;
            else
                base.WndProc(ref m);
        }
    }
}
