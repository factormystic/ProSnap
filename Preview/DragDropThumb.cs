using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProSnap
{
    public partial class DragDropThumb : Form
    {
        Image image;

        public DragDropThumb(Image image)
        {
            InitializeComponent();

            this.image = image;
            Height = (int)(((float)Width) / ((float)image.Width / (float)image.Height));

            AllowTransparency = true;
            Opacity = 0.75;

            SetDesktopLocation(Cursor.Position.X - Width / 2, Cursor.Position.Y - Height);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT

                return createParams;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, this.ClientRectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    {// this is WM_NCHITTEST
                        //base.WndProc(ref m);
                        //if ((/*m.LParam.ToInt32() >> 16 and m.LParam.ToInt32() & 0xffff fit in your transparen region*/) 
                        //  && m.Result.ToInt32() == 1) {
                        //    m.Result = new IntPtr(2);   // HTCAPTION
                        m.Result = IntPtr.Zero;
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}