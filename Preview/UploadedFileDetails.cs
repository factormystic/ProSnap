using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Utilities.Screenshot;

namespace ProSnap
{
    public partial class UploadedFileDetails : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        private static extern bool DeleteObject(IntPtr hObject);

        Double MaxOpacity;

        ExtendedScreenshot Metadata;

        Timer MousePositionCheck = new Timer();

        public UploadedFileDetails(ExtendedScreenshot metadata)
        {
            InitializeComponent();

            this.BackColor = Color.Black;
            this.Paint += new PaintEventHandler(UploadedFileDetails_Paint);
            this.Load += new EventHandler(UploadedFileDetails_Load);

            this.Metadata = metadata;

            //Much more reliable than MouseLeave for the form
            MousePositionCheck.Tick += (s, e) =>
                {
                    Rectangle b = this.Bounds;
                    b.Inflate(10,10);
                    if (!b.Contains(MousePosition))
                    {
                        MousePositionCheck.Enabled = false;
                        FadeClose(1500);
                    }
                    //else
                    //    Opacity = MaxOpacity;
                };
            MousePositionCheck.Interval = 100;
            MousePositionCheck.Enabled = true;
        }

        void UploadedFileDetails_Load(object sender, EventArgs e)
        {
            if (Metadata.Remote.ImageLink == null)
            {
                btCopyLink.Visible = false;
                btCopyDeleteLink.Visible = false;

                lbInfo.Visible = true;
            }
        }

        public void ShowFormAt(Form owner, Point p, double maxOpacity)
        {
            this.MaxOpacity = maxOpacity;
            this.Opacity = 0;
            this.Show(owner);
            this.Bounds = new Rectangle(new Point(owner.Left + (owner.Width - owner.ClientRectangle.Width) / 2 + (p.X - this.Width / 2), owner.Top + (owner.Height - owner.ClientRectangle.Height) / 2 + (p.Y - this.Height + 5)), this.Size);

            Timer showt = new Timer();
            showt.Tick += (e, o) =>
                {
                    if (this.Opacity >= MaxOpacity)
                        showt.Enabled = false;
                    else
                        this.Opacity += 0.1;
                };
            showt.Interval = 10;
            showt.Enabled = true;
        }

        void UploadedFileDetails_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.High;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int tail_round = 15;
            int main_bottom = this.Height - tail_round;

            Region RoundedRect = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, main_bottom + 1, 25, 25));

            Region raw_bottom = new Region(new Rectangle(this.Width / 2 - 20, main_bottom, 40, 50));
            Region TailClipL = Region.FromHrgn(CreateRoundRectRgn(0, main_bottom + 1, this.Width / 2, 200, tail_round, tail_round));
            Region TailClipR = Region.FromHrgn(CreateRoundRectRgn(this.Width / 2 - 1, main_bottom + 1, this.Width, 200, tail_round, tail_round));
            TailClipL.Union(TailClipR);
            raw_bottom.Exclude(TailClipL);
            //raw_bottom.Exclude(TailClipR);

            RoundedRect.Union(raw_bottom);

            //this.Region = RoundedRect;

            GraphicsPath gp = new GraphicsPath();
            int margin = 2;
            int radius = 10;
            int tailmargin = 12;

            gp.AddArc(margin, margin, radius, radius, -90, -90);
            gp.AddArc(margin, this.Height - radius - margin * 2-tailmargin, radius, radius, -180, -90);
            
            //tail
            gp.AddArc(this.Width / 2 - tailmargin, (this.Height - radius - margin * 2 - tailmargin) + radius, tailmargin, tailmargin, -90, 90);
            gp.AddArc(this.Width / 2, (this.Height - radius - margin * 2 - tailmargin) + radius, tailmargin, tailmargin, 180, 90);
            
            
            gp.AddArc(this.Width - radius - margin * 2, this.Height - radius - margin * 2-tailmargin, radius, radius, -270, -90);
            gp.AddArc(this.Width - radius - margin * 2, margin, radius, radius, -360, -90);

            gp.CloseAllFigures();

            e.Graphics.CopyFromScreen(this.Left, this.Top, 0, 0, new System.Drawing.Size(this.Width, this.Height));


            //e.Graphics.DrawLines(new Pen(Brushes.Green), new Point[] { new Point(0, 0), new Point(0, this.Height-1), new Point(this.Width-1, this.Height-1), new Point(this.Width-1, 0) });


            //e.Graphics.DrawPath(new Pen(Color.Red, 2f), gp);
            e.Graphics.FillPath(Brushes.Black, gp);

            //DeleteObject(ptr);
        }

        private void btCopyLink_Click(object sender, EventArgs e)
        {
            //Clipboard access needs STA, when we launch from a history item we're MTA
            System.Threading.Thread HookAction = new System.Threading.Thread(new System.Threading.ThreadStart(() => Clipboard.SetText(Metadata.Remote.ImageLink)));
            HookAction.SetApartmentState(System.Threading.ApartmentState.STA);
            HookAction.Start();

            btCopyLink.Text = "Copied";
            FadeClose(1500);
        }

        private void btCopyDeleteLink_Click(object sender, EventArgs e)
        {
            //Clipboard access needs STA, when we launch from a history item we're MTA
            System.Threading.Thread HookAction = new System.Threading.Thread(new System.Threading.ThreadStart(() => Clipboard.SetText(Metadata.Remote.DeleteLink)));
            HookAction.SetApartmentState(System.Threading.ApartmentState.STA);
            HookAction.Start();
            
            btCopyDeleteLink.Text = "Copied";
            FadeClose(1500);
        }

        private void FadeClose(int pause)
        {
            Timer delay = new Timer();
            delay.Tick += (o_p, e_p) =>
            {
                delay.Enabled = false;

                Timer fade = new Timer();
                fade.Tick += (o, a) =>
                {
                    if (this.Opacity == 0)
                    {
                        fade.Enabled = false;
                        this.Close();
                    }
                    else
                        this.Opacity -= 0.1;
                };
                fade.Interval = 10;
                fade.Enabled = true;
            };
            delay.Interval = (int)Math.Max(1, pause);
            delay.Enabled = true;
        }
    }
}