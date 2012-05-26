using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProSnap
{
    //cf. http://stackoverflow.com/a/5919191/1569
    public partial class RegionSelector : Form
    {
        Size? mouseGrabOffset;

        bool ResizingLeft = false;
        bool ResizingWidth = false;
        bool ResizingTop = false;
        bool ResizingHeight = false;

        public Rectangle SnapshotRectangle
        {
            get
            {
                var interior = new Rectangle(this.DesktopBounds.X + 20, this.DesktopBounds.Y + 20, this.Width - 40, this.Height - 40);
                interior.Intersect(DesktopArea);

                return interior;
            }
        }

        private Rectangle DesktopArea
        {
            get
            {
                return Screen.AllScreens.Select(s => s.Bounds).Aggregate((a, b) => Rectangle.Union(a, b));
            }
        }

        public RegionSelector()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                if (this.Cursor == Cursors.Hand)
                    mouseGrabOffset = new Size(e.Location);
                else if (this.Cursor == Cursors.SizeWE && e.X <= 5)
                    ResizingLeft = true;
                else if (this.Cursor == Cursors.SizeWE && e.X >= this.Width - 5)
                    ResizingWidth = true;
                else if (this.Cursor == Cursors.SizeNS && e.Y <= 5)
                    ResizingTop = true;
                else if (this.Cursor == Cursors.SizeNS && e.Y >= this.Height - 5)
                    ResizingHeight = true;

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            mouseGrabOffset = null;

            ResizingLeft = false;
            ResizingWidth = false;
            ResizingTop = false;
            ResizingHeight = false;

            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.X <= 5 || e.X >= this.Width - 5)
                this.Cursor = Cursors.SizeWE;
            else if (e.Y <= 5 || e.Y >= this.Height - 5)
                this.Cursor = Cursors.SizeNS;
            else
                this.Cursor = Cursors.Hand;


            if (mouseGrabOffset.HasValue)
            {
                this.Location = Cursor.Position - mouseGrabOffset.Value;
            }

            if (ResizingLeft)
            {
                var r = this.Right;
                this.Left = Cursor.Position.X;
                this.Width = r - this.Left;
            }

            if (ResizingTop)
            {
                var b = this.Bottom;
                this.Top = Cursor.Position.Y;
                this.Height = b - this.Top;
            }

            if (ResizingWidth)
            {
                this.Width = this.PointToClient(Cursor.Position).X;
            }

            if (ResizingHeight)
            {
                this.Height = this.PointToClient(Cursor.Position).Y;
            }

            //this.ResumeLayout();

            base.OnMouseMove(e);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void RegionSelector_LocationChanged(object sender, System.EventArgs e)
        {
            if (DesktopBounds.Top < DesktopArea.Top)
            {
                llTakeScreenshotTopLeft.Visible = llTakeScreenshotTopRight.Visible = false;
                llTakeScreenshotBottomLeft.Visible = llTakeScreenshotBottomRight.Visible = true;
            }

            if (DesktopBounds.Bottom > DesktopArea.Bottom)
            {
                llTakeScreenshotBottomLeft.Visible = llTakeScreenshotBottomRight.Visible = false;
                llTakeScreenshotTopLeft.Visible = llTakeScreenshotTopRight.Visible = true;
            }

            if (DesktopBounds.Left < DesktopArea.Left)
            {
                llTakeScreenshotTopLeft.Visible = llTakeScreenshotBottomLeft.Visible = false;
                llTakeScreenshotTopRight.Visible = llTakeScreenshotBottomRight.Visible = true;
            }

            if (DesktopBounds.Right > DesktopArea.Right)
            {
                llTakeScreenshotTopRight.Visible = llTakeScreenshotBottomRight.Visible = false;
                llTakeScreenshotTopLeft.Visible = llTakeScreenshotBottomLeft.Visible = true;
            }

            if (llTakeScreenshotTopLeft.Visible && llTakeScreenshotTopRight.Visible)
                llTakeScreenshotTopRight.Visible = false;

            if (llTakeScreenshotBottomLeft.Visible && llTakeScreenshotBottomRight.Visible)
                llTakeScreenshotBottomRight.Visible = false;

            if (llTakeScreenshotTopLeft.Visible && llTakeScreenshotBottomLeft.Visible)
                llTakeScreenshotBottomLeft.Visible = false;

            if (llTakeScreenshotTopRight.Visible && llTakeScreenshotBottomRight.Visible)
                llTakeScreenshotBottomRight.Visible = false;
        }

        private void RegionSelector_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    break;

                case Keys.Enter:
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;
            }
        }
    }
}
