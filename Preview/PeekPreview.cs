using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FMUtils;
using FMUtils.WinApi;
using ProSnap.ActionItems;
using ProSnap.Properties;
using ProSnap.Uploading;

namespace ProSnap
{
    public partial class PeekPreview : Form
    {
        internal ExtendedScreenshot CurrentScreenshot { get; private set; }
        internal bool isSaveDialogOpen { get; set; }

        #region Fields
        Timer FadeCloseCountdown = new Timer();
        Timer MousePositionCheck = new Timer();

        Timer WriteCompleteCheck = new Timer();
        long CurrentStreamLength = 0;
        bool isEditing = false;

        float ImageAspectRatio;

        DragDropThumb DragThumb;

        bool BackwardsEnd, ForwardsEnd;
        int UploadAnimationIndex = -1;
        #endregion

        protected override bool ShowWithoutActivation
        {
            get
            {
                return Configuration.ShowPreviewWithoutActivation;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams baseParams = base.CreateParams;
                baseParams.ExStyle |= (int)Windowing.WS_EX_TOPMOST;

                return baseParams;
            }
        }

        public PeekPreview()
        {
            Trace.WriteLine("Creating preview window...", string.Format("PeekPreview.ctor [{0}]", System.Threading.Thread.CurrentThread.Name));

            InitializeComponent();

            FadeCloseCountdown.Tick += (s, e) =>
            {
                if (Configuration.PreviewDelayTime > 0 && !this.Bounds.Contains(MousePosition))
                {
                    Trace.WriteLine("Interaction keep-alive timer lapsed, closing preview...", string.Format("PeekPreview.Interact_Tick [{0}]", System.Threading.Thread.CurrentThread.Name));
                    FadeClose();
                }
            };

            //Much more reliable than MouseLeave for the form
            MousePositionCheck.Interval = 100;
            MousePositionCheck.Tick += (s, e) =>
            {
                if (Opacity < 1)
                    return;

                var ShouldFadeCloseCountdown = !this.Bounds.Contains(MousePosition); // || !this.Focused;
                if (ShouldFadeCloseCountdown != FadeCloseCountdown.Enabled)
                {
                    Trace.WriteLine(string.Format("FadeCloseCountdown '{0}' -> '{1}'", FadeCloseCountdown.Enabled, ShouldFadeCloseCountdown), string.Format("PeekPreview.MousePositionCheck_Tick [{0}]", System.Threading.Thread.CurrentThread.Name));
                    FadeCloseCountdown.Enabled = ShouldFadeCloseCountdown;
                }
            };

            WriteCompleteCheck.Interval = 1000;
            WriteCompleteCheck.Tick += WriteCompleteCheck_Tick;

            Trace.WriteLine("Done.", string.Format("PeekPreview.ctor [{0}]", System.Threading.Thread.CurrentThread.Name));
        }

        private void LoadScreenshot(ExtendedScreenshot targetWindow)
        {
            Trace.WriteLine("Loading image...", string.Format("PeekPreview.LoadScreenshot [{0}]", System.Threading.Thread.CurrentThread.Name));

            pbPreview.Image = null;

            tsmiRoundCorners.Checked = targetWindow.withBorderRounding;
            tsmiWindowShadow.Checked = targetWindow.withBorderShadow;
            tsmiMouseCursor.Checked = targetWindow.withCursor;

            CurrentScreenshot = targetWindow;
            ReloadPreviewImage();

            ResetFadeCloseCountdown();
            GroomBackForwardIcons();
            GroomHeartIcon();

            pnUpload.BackgroundImage = CurrentScreenshot == null || string.IsNullOrEmpty(CurrentScreenshot.Remote.ImageLink) ? Resources.cloud_upload_32x32_black : Resources.cloud_upload_link_black_32x32;

            if (Configuration.PreviewDelayTime > 0)
                FadeCloseCountdown.Interval = Configuration.PreviewDelayTime * 1000;

            Opacity = 1;

            Trace.WriteLine("Done.", string.Format("PeekPreview.LoadScreenshot [{0}]", System.Threading.Thread.CurrentThread.Name));
        }

        private void ReloadPreviewImage()
        {
            Trace.WriteLine("Reloading image...", string.Format("PeekPreview.ReloadPreviewImage [{0}]", System.Threading.Thread.CurrentThread.Name));

            this.SuspendLayout();

            pbPreview.SizeMode = PictureBoxSizeMode.Zoom;

            //using (MemoryStream stream = LatestScreenshot.GetEditedScreenshotPNGImageThumbnailStream())
            using (MemoryStream stream = CurrentScreenshot.EditedScreenshotPNGImageStream())
                pbPreview.Image = Bitmap.FromStream(stream);

            ImageAspectRatio = (float)pbPreview.Image.Width / (float)pbPreview.Image.Height;

            this.Width = Screen.PrimaryScreen.WorkingArea.Width / 4;
            float TargetPictureBoxHeight = Math.Min(Math.Min((float)this.Width / ImageAspectRatio, (float)pbPreview.Image.Height), Screen.PrimaryScreen.WorkingArea.Height - 20);

            this.Height = (int)Math.Min(TargetPictureBoxHeight + pnButtons.Height + (this.Height - this.ClientRectangle.Height), Screen.PrimaryScreen.WorkingArea.Height / 2);
            this.Width = (int)Math.Min((float)this.Height * ImageAspectRatio, this.Width);

            pbPreview.Width = this.ClientRectangle.Width;
            pbPreview.Height = this.ClientRectangle.Height - pnButtons.Height;
            pbPreview.Top = 0;
            pbPreview.Left = 0;

            int AdjustedWidth = (int)(this.Height * ImageAspectRatio);
            if (pbPreview.Height > pbPreview.Image.Height && AdjustedWidth < this.Width)
            {
                this.Height = pbPreview.Image.Height;
                this.Width = AdjustedWidth;
            }

            SetWindowPosition();

            this.ResumeLayout(true);

            Trace.WriteLine("Done.", string.Format("PeekPreview.ReloadPreviewImage [{0}]", System.Threading.Thread.CurrentThread.Name));
        }

        public void Show(ExtendedScreenshot targetWindow = null)
        {
            Trace.WriteLine("Showing preview...", string.Format("PeekPreview.Show [{0}]", System.Threading.Thread.CurrentThread.Name));

            if (this.InvokeRequired)
            {
                Trace.WriteLine("Self invoking...", string.Format("PeekPreview.Show [{0}]", System.Threading.Thread.CurrentThread.Name));

                this.BeginInvoke(new MethodInvoker(() => this.Show(targetWindow)));
                return;
            }

            targetWindow = targetWindow ?? this.CurrentScreenshot ?? Program.History.LastOrDefault();
            if (targetWindow == null)
            {
                Trace.WriteLine("No screenshot to show", string.Format("PeekPreview.Show [{0}]", System.Threading.Thread.CurrentThread.Name));
                return;
            }

            this.LoadScreenshot(targetWindow);
            base.Show();
        }

        public void FadeClose()
        {
            Trace.WriteLine("Close attempt...", string.Format("PeekPreview.FadeClose [{0}]", System.Threading.Thread.CurrentThread.Name));

            if (this.InvokeRequired)
            {
                Trace.WriteLine("Self invoking...", string.Format("PeekPreview.FadeClose [{0}]", System.Threading.Thread.CurrentThread.Name));

                this.BeginInvoke(new MethodInvoker(() => FadeClose()));
                return;
            }
            
            //Program.Uploader.InProgress ||
            if ((isSaveDialogOpen || Configuration.PreviewDelayTime == 0) && this.Opacity == 1)
            {
                Trace.WriteLine("Close attempt blocked", string.Format("PeekPreview.FadeClose [{0}]", System.Threading.Thread.CurrentThread.Name));
                return;
            }

            Trace.WriteLine("Close attempt confirmed, starting fade out timer...", string.Format("PeekPreview.FadeClose [{0}]", System.Threading.Thread.CurrentThread.Name));

            FadeCloseCountdown.Enabled = false;

            int SlideFactor = 0;
            if (Configuration.PreviewLocation == FMUtils.WinApi.Helper.WindowLocation.UpperLeft || Configuration.PreviewLocation == FMUtils.WinApi.Helper.WindowLocation.UpperRight)
                SlideFactor = -1;
            else if (Configuration.PreviewLocation == FMUtils.WinApi.Helper.WindowLocation.LowerLeft || Configuration.PreviewLocation == FMUtils.WinApi.Helper.WindowLocation.LowerRight)
                SlideFactor = 1;

            Timer FadeOut = new Timer();
            FadeOut.Tick += (s_fo, e_fo) =>
            {
                if (Opacity > 0)
                {
                    this.Opacity -= 0.08;
                    this.Top += SlideFactor;
                }
                else
                {
                    Trace.WriteLine("Fade out timer complete.", string.Format("PeekPreview.FadeClose [{0}]", System.Threading.Thread.CurrentThread.Name));

                    FadeOut.Enabled = false;
                    this.Hide();
                }
            };
            FadeOut.Interval = 15;
            FadeOut.Enabled = true;
        }

        private void ResetFadeCloseCountdown()
        {
            FadeCloseCountdown.Stop();
            FadeCloseCountdown.Start();
        }

        private void SetupWindowArea()
        {
            Trace.WriteLine(string.Format("Window frame configuration ({0})", FMUtils.WinApi.Helper.VisualStyle), string.Format("PeekPreview.SetupWindowArea [{0}]", System.Threading.Thread.CurrentThread.Name));

            if (FMUtils.WinApi.Helper.VisualStyle == FMUtils.WinApi.Helper.VisualStyles.Aero)
            {
                this.BackColor = Color.Black;

                //if the caption was previously set to an empty string (for Basic) but now we're back to Aero, zero it again
                this.Text = "";

                DWM.Margins GlassMargins;
                GlassMargins.Top = -1;
                GlassMargins.Left = -1;
                GlassMargins.Right = -1;
                GlassMargins.Bottom = -1;

                DWM.DwmExtendFrameIntoClientArea(this.Handle, ref GlassMargins);
            }
            else
            {
                this.BackColor = SystemColors.GradientActiveCaption;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;

                //need this to get correctly colored non-client area, at the cost of a top title bar margin
                this.Text = " ";
            }
        }

        private void SetWindowPosition()
        {
            switch (Configuration.PreviewLocation)
            {
                case FMUtils.WinApi.Helper.WindowLocation.LowerRight:
                    {
                        Left = Screen.PrimaryScreen.WorkingArea.Right - Width - 10;
                        Top = Screen.PrimaryScreen.WorkingArea.Bottom - Height - 10;
                    } break;
                case FMUtils.WinApi.Helper.WindowLocation.LowerLeft:
                    {
                        Left = Screen.PrimaryScreen.WorkingArea.Left + 10;
                        Top = Screen.PrimaryScreen.WorkingArea.Bottom - Height - 10;
                    } break;
                case FMUtils.WinApi.Helper.WindowLocation.UpperLeft:
                    {
                        Left = Screen.PrimaryScreen.WorkingArea.Left + 10;
                        Top = Screen.PrimaryScreen.WorkingArea.Top + 10;
                    } break;
                case FMUtils.WinApi.Helper.WindowLocation.UpperRight:
                    {
                        Top = Screen.PrimaryScreen.WorkingArea.Top + 10;
                        Left = Screen.PrimaryScreen.WorkingArea.Right - Width - 10;
                    } break;
                case FMUtils.WinApi.Helper.WindowLocation.Center:
                    {
                        Left = Screen.PrimaryScreen.WorkingArea.Width / 2 - Width / 2;
                        Top = Screen.PrimaryScreen.WorkingArea.Height / 2 - Height / 2;
                    } break;
            }

            if (Configuration.ButtonPanelLocation == DockStyle.Bottom || Configuration.ButtonPanelLocation == DockStyle.Top)
                pnButtons.Dock = Configuration.ButtonPanelLocation;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Windowing.WM_DESKTOPCOMPOSITIONCHANGED)
            {
                //RecreateHandle();
                SetupWindowArea();
                if (this.Visible)
                    SetWindowPosition();
            }
            else if (Opacity == 0)
            {
                //Don't respond to events if invisible, so this is faux-Hide(). We can't Hide() because then we cant invoke, which we need to do whenever we need to talk to this form from a thread
                base.WndProc(ref m);
            }
            else if (m.Msg == (int)Windowing.WM_NCHITTEST)
            {
                m.Result = (IntPtr)(-1);
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        #region Preview
        private void pbPreview_Click(object sender, EventArgs e)
        {
            var mea = e as MouseEventArgs;
            if (mea != null && mea.Button == MouseButtons.Right && File.Exists(CurrentScreenshot.SavedFileName))
            {
                Trace.WriteLine("Right clicked preview...", string.Format("PeekPreview.pbPreview_Click [{0}]", System.Threading.Thread.CurrentThread.Name));
                new GongSolutions.Shell.ShellContextMenu(new GongSolutions.Shell.ShellItem(CurrentScreenshot.SavedFileName)).ShowContextMenu(pbPreview, mea.Location, FMUtils.WinApi.Helper.isShiftKeyDown() ? GongSolutions.Shell.Interop.CMF.EXTENDEDVERBS : GongSolutions.Shell.Interop.CMF.EXPLORE);
            }
        }

        private void pbPreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Trace.WriteLine("Starting drag operation...", string.Format("PeekPreview.pbPreview_MouseDown [{0}]", System.Threading.Thread.CurrentThread.Name));

                DragThumb = new DragDropThumb(pbPreview.Image);
                DragThumb.Show();

                string TargetDirectory = Path.GetDirectoryName(CurrentScreenshot.InternalFileName);
                if (!Directory.Exists(TargetDirectory))
                    Directory.CreateDirectory(TargetDirectory);

                var DropFileType = new[] { ImageFormat.Png, ImageFormat.Bmp, ImageFormat.Jpeg, ImageFormat.Gif }[Configuration.DefaultFilterIndex];
                var DropFileName = CurrentScreenshot.InternalFileName.Substring(0, CurrentScreenshot.InternalFileName.LastIndexOf('.')) + "." + DropFileType.ToString().ToLower();

                CurrentScreenshot.ComposedScreenshotImage.Save(DropFileName, DropFileType);

                var r = pbPreview.DoDragDrop(new DataObject(DataFormats.FileDrop, new string[] { DropFileName }), DragDropEffects.Copy);

                DragThumb.Close();
                Trace.WriteLine("Drag operation complete.", string.Format("PeekPreview.pbPreview_MouseDown [{0}]", System.Threading.Thread.CurrentThread.Name));
            }
        }

        private void pbPreview_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            //Trace.WriteLine("QueryContinueDrag...", "PeekPreview.pbPreview_QueryContinueDrag");

            if (e.Action == DragAction.Continue)
            {
                DragThumb.SetDesktopLocation(Cursor.Position.X - DragThumb.Width / 2, Cursor.Position.Y - DragThumb.Height);
            }
            //else
            //{
            //    DragThumb.Close();
            //}
        }
        #endregion

        #region Heart
        private void pnHeart_Click(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                CurrentScreenshot.isFlagged = !CurrentScreenshot.isFlagged;
                GroomHeartIcon();
            }
        }

        private void pnHeart_MouseEnter(object sender, EventArgs e)
        {
            pnHeart.BackgroundImage = CurrentScreenshot != null && CurrentScreenshot.isFlagged ? Resources.heart_fill_white_32x38 : Resources.heart_stroke_white_32x28;
        }

        private void pnHeart_MouseLeave(object sender, EventArgs e)
        {
            pnHeart.BackgroundImage = CurrentScreenshot != null && CurrentScreenshot.isFlagged ? Resources.heart_fill_black_32x38 : Resources.heart_stroke_black_32x28;
        }
        #endregion

        #region Save
        private void pnSave_Click(object sender, EventArgs e)
        {
            switch ((e as MouseEventArgs).Button)
            {
                case MouseButtons.Left:
                    new SaveAction() { Prompt = true }.Invoke(this.CurrentScreenshot);
                    break;

                case MouseButtons.Right:
                    if (File.Exists(CurrentScreenshot.SavedFileName))
                    {
                        Trace.WriteLine("Opening file context menu...", string.Format("PeekPreview.btSave_Click [{0}]", System.Threading.Thread.CurrentThread.Name));
                        new GongSolutions.Shell.ShellContextMenu(new GongSolutions.Shell.ShellItem(CurrentScreenshot.SavedFileName)).ShowContextMenu(pnSave, (e as MouseEventArgs).Location, FMUtils.WinApi.Helper.isShiftKeyDown() ? GongSolutions.Shell.Interop.CMF.EXTENDEDVERBS : GongSolutions.Shell.Interop.CMF.EXPLORE);
                    }
                    break;
            }
        }

        private void pnSave_MouseEnter(object sender, EventArgs e)
        {
            pnSave.BackgroundImage = Resources.document_stroke_32x32_white;
        }

        private void pnSave_MouseLeave(object sender, EventArgs e)
        {
            pnSave.BackgroundImage = Resources.document_stroke_32x32_black;
        }
        #endregion

        #region Upload
        public void pnUpload_Click(object sender, EventArgs e)
        {
            switch ((e as MouseEventArgs).Button)
            {
                case MouseButtons.Left:
                    if (!string.IsNullOrEmpty(CurrentScreenshot.Remote.ImageLink))
                    {
                        Trace.WriteLine("Already have a link for this screenshot, copying to clipboard", string.Format("PeekPreview.UploadLatestScreenshot [{0}]", System.Threading.Thread.CurrentThread.Name));

                        new ClipboardAction(":url").Invoke(CurrentScreenshot);
                        return;
                    }

                    Task.Factory.StartNew(() => new UploadAction().Invoke(this.CurrentScreenshot))
                        .ContinueWith(t =>
                        {
                            if (t.Result == null)
                            {
                                MessageBox.Show(this, string.Format("An error occurred while uploading this screenshot:\n{0}", t.Exception.GetBaseException()), "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                new ClipboardAction(":url").Invoke(CurrentScreenshot);
                            }

                            GroomUploadMenuItemStyles();
                        }, TaskScheduler.FromCurrentSynchronizationContext());
                    break;
            }
        }

        private void pnUpload_MouseEnter(object sender, EventArgs e)
        {
            pnUpload.BackgroundImage = UploadAnimationIndex == -1 ? (CurrentScreenshot != null && !string.IsNullOrEmpty(CurrentScreenshot.Remote.ImageLink) ? Resources.cloud_upload_link_white_32x32 : Resources.cloud_upload_32x32_white) : ilUploadAnimationWhite.Images[UploadAnimationIndex];
        }

        private void pnUpload_MouseLeave(object sender, EventArgs e)
        {
            pnUpload.BackgroundImage = UploadAnimationIndex == -1 ? (CurrentScreenshot != null && !string.IsNullOrEmpty(CurrentScreenshot.Remote.ImageLink) ? Resources.cloud_upload_link_black_32x32 : Resources.cloud_upload_32x32_black) : ilUploadAnimationBlack.Images[UploadAnimationIndex];

            var bounds = cmsUpload.Bounds;
            bounds.Inflate(10, 10);

            if (!bounds.Contains(Cursor.Position))
                cmsUpload.Hide();
        }

        private void pnUpload_MouseHover(object sender, EventArgs e)
        {
            if (cmsUpload.InvokeRequired)
            {
                Trace.WriteLine("cmsEdit Invoke Required", string.Format("PeekPreview.cmsEdit_Invoker [{0}]", System.Threading.Thread.CurrentThread.Name));

                this.BeginInvoke(new MethodInvoker(() => pnUpload_MouseHover(sender, e)));
                return;
            }

            if (!this.Focused)
                this.Focus();

            GroomUploadMenuItemStyles();

            cmsUpload.Show(pnUpload, 0, Configuration.ButtonPanelLocation == DockStyle.Top ? pnUpload.Bottom : pnUpload.Top - cmsUpload.Height);
        }

        private void cmsUpload_MouseLeave(object sender, EventArgs e)
        {
            cmsUpload.Hide();
        }

        private void tsmiUploadingOptions_Click(object sender, EventArgs e)
        {
            Options.Options.ShowOrActivate("Uploading");
        }

        private void tsmiCopyImageLink_Click(object sender, EventArgs e)
        {
            new ClipboardAction(":url").Invoke(CurrentScreenshot);
        }

        private void tsmiCopyDeleteLink_Click(object sender, EventArgs e)
        {
            new ClipboardAction(":delete").Invoke(CurrentScreenshot);
        }

        private void tsmiUploadImage_Click(object sender, EventArgs e)
        {
            Configuration.UploadServices.FirstOrDefault(P => P.isActive).Upload(this.CurrentScreenshot);
        }

        private void GroomUploadMenuItemStyles()
        {
            var ActiveService = Configuration.UploadServices.FirstOrDefault(us => us.isActive);
            tsmiUploadImage.Text = ActiveService == null ? "Upload screenshot" : "Upload to " + ActiveService.Name;

            tsmiCopyImageLink.Enabled = !string.IsNullOrEmpty(CurrentScreenshot.Remote.ImageLink);
            tsmiCopyDeleteLink.Enabled = !string.IsNullOrEmpty(CurrentScreenshot.Remote.DeleteLink);

            if (tsmiCopyImageLink.Enabled)
            {
                tsmiCopyImageLink.Font = new Font(tsmiCopyImageLink.Font, FontStyle.Bold);
                tsmiUploadImage.Font = new Font(tsmiUploadImage.Font, FontStyle.Regular);
            }
            else
            {
                tsmiCopyImageLink.Font = new Font(tsmiCopyImageLink.Font, FontStyle.Regular);
                tsmiUploadImage.Font = new Font(tsmiUploadImage.Font, FontStyle.Bold);
            }
        }
        #endregion

        #region Edit
        private void pnEdit_Click(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                OpenInEditor();
            }
        }

        private void pnEdit_MouseEnter(object sender, EventArgs e)
        {
            pnEdit.BackgroundImage = Resources.image_32x32_white;
        }

        private void pnEdit_MouseLeave(object sender, EventArgs e)
        {
            pnEdit.BackgroundImage = Resources.image_32x32_black;

            var bounds = cmsEdit.Bounds;
            var pt = Cursor.Position;

            bounds.Inflate(10, 10);
            if (!bounds.Contains(pt))
                cmsEdit.Hide();
        }

        private void pnEdit_MouseHover(object sender, EventArgs e)
        {
            if (cmsEdit.InvokeRequired)
            {
                Trace.WriteLine("cmsEdit Invoke Required", string.Format("PeekPreview.cmsEdit_Invoker [{0}]", System.Threading.Thread.CurrentThread.Name));

                this.BeginInvoke(new MethodInvoker(() => pnEdit_MouseHover(sender, e)));
                return;
            }

            if (!this.Focused) this.Focus();

            cmsEdit.Show(pnEdit, 0, Configuration.ButtonPanelLocation == DockStyle.Top ? pnEdit.Bottom : pnEdit.Top - cmsEdit.Height);
        }

        private void cmsEdit_MouseLeave(object sender, EventArgs e)
        {
            cmsEdit.Hide();
        }

        private void tsmiEditingOptions_Click(object sender, EventArgs e)
        {
            Options.Options.ShowOrActivate("shortcuts");
        }

        private void tsmiOpenInEditor_Click(object sender, EventArgs e)
        {
            OpenInEditor();
        }

        private void tsmiWindowShadow_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPreview.Image != null)
            {
                CurrentScreenshot.withBorderShadow = tsmiWindowShadow.Checked;
                ReloadPreviewImage();
            }
        }

        private void tsmiRoundCorners_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPreview.Image != null)
            {
                CurrentScreenshot.withBorderRounding = tsmiRoundCorners.Checked;
                ReloadPreviewImage();
            }
        }

        private void tsmiMouseCursor_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPreview.Image != null)
            {
                CurrentScreenshot.withCursor = tsmiMouseCursor.Checked;
                ReloadPreviewImage();
            }
        }

        private void OpenInEditor()
        {
            isEditing = true;

            if (!File.Exists(CurrentScreenshot.InternalFileName))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(CurrentScreenshot.InternalFileName));
                CurrentScreenshot.ComposedScreenshotImage.Save(CurrentScreenshot.InternalFileName, ImageFormat.Png);
            }

            Windowing.ShellExecute(IntPtr.Zero, "edit", CurrentScreenshot.InternalFileName, string.Empty, string.Empty, Windowing.ShowCommands.SW_NORMAL);

            var fsw = new FileSystemWatcher(Path.GetDirectoryName(CurrentScreenshot.InternalFileName), "*" + Path.GetExtension(CurrentScreenshot.InternalFileName));
            fsw.Changed += (s_fsw, e_args) => this.BeginInvoke(new Action(() => WriteCompleteCheck.Enabled = true));

            var async = new BackgroundWorker();
            async.DoWork += (o, a) => fsw.WaitForChanged(WatcherChangeTypes.Changed);
            async.RunWorkerAsync();
        }

        private void WriteCompleteCheck_Tick(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream(CurrentScreenshot.InternalFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Trace.WriteLine("Checking written file size...", string.Format("PeekPreview.WriteCompleteCheck_Tick [{0}]", System.Threading.Thread.CurrentThread.Name));

                if (fs.Length > 0 && CurrentStreamLength == fs.Length)
                {
                    Trace.WriteLine("Write completed, loading file...", string.Format("PeekPreview.WriteCompleteCheck_Tick [{0}]", System.Threading.Thread.CurrentThread.Name));

                    WriteCompleteCheck.Enabled = false;
                    using (var LatestScreenshotImageData = new MemoryStream())
                    {
                        fs.CopyTo(LatestScreenshotImageData);
                        CurrentScreenshot.SetOverrideImageData(new Bitmap(LatestScreenshotImageData));
                    }

                    ReloadPreviewImage();

                    Opacity = 1;
                    FadeCloseCountdown.Enabled = true;
                }
                else
                {
                    //Yes, this will never complete on the first tick. This is by design. We want two consecutive same-sized ticks before reloading the image. We want to make sure we have the whole thing, and even if Length > 0 that's no guarantee.
                    CurrentStreamLength = fs.Length;
                }
            }
        }
        #endregion

        #region Delete
        private void pnDelete_Click(object sender, EventArgs e)
        {
            switch ((e as MouseEventArgs).Button)
            {
                case MouseButtons.Left:
                    this.CurrentScreenshot = ActionTypes.Delete.ToInstance().Invoke(CurrentScreenshot);
                    break;
            }
        }

        private void pnDelete_MouseEnter(object sender, EventArgs e)
        {
            pnDelete.BackgroundImage = Resources.x_28x28_red;
        }

        private void pnDelete_MouseLeave(object sender, EventArgs e)
        {
            pnDelete.BackgroundImage = Resources.x_28x28_black;
        }
        #endregion

        #region Backward/Forward
        private void pnBackward_Click(object sender, EventArgs e)
        {
            var prev = Program.History.IndexOf(CurrentScreenshot) - 1;
            if (prev >= 0)
                LoadScreenshot(Program.History[prev]);

            GroomBackForwardIcons();
        }

        private void pnBackward_MouseEnter(object sender, EventArgs e)
        {
            pnBackward.BackgroundImage = BackwardsEnd ? Resources.backward_empty_18x32_white : Resources.backward_18x32_white;
        }

        private void pnBackward_MouseLeave(object sender, EventArgs e)
        {
            pnBackward.BackgroundImage = BackwardsEnd ? Resources.backward_empty_18x32_black : Resources.backward_18x32_backward;
        }

        private void pnForward_Click(object sender, EventArgs e)
        {
            var next = Program.History.IndexOf(CurrentScreenshot) + 1;
            if (next < Program.History.Count)
                LoadScreenshot(Program.History[next]);

            GroomBackForwardIcons();
        }

        private void pnForward_MouseEnter(object sender, EventArgs e)
        {
            pnForward.BackgroundImage = ForwardsEnd ? Resources.forward_empty_18x32_white : Resources.forward_18x32_white;
        }

        private void pnForward_MouseLeave(object sender, EventArgs e)
        {
            pnForward.BackgroundImage = ForwardsEnd ? Resources.forward_empty_18x32_black : Resources.forward_18x32_black;
        }
        #endregion

        #region Window Events
        private void PeekPreview_Load(object sender, System.EventArgs e)
        {
            Trace.WriteLine("Initial load", string.Format("PeekPreview.PeekPreview_Load [{0}]", System.Threading.Thread.CurrentThread.Name));

            SetWindowPosition();
            SetupWindowArea();

            ////ShowModal needs a parent window, which we don't have, so Windows uses the desktop window instead.
            ////This means it's created below everything else. SetForegroundWindow bumps it to the top.
            //Windowing.BringWindowToTop(this.Handle);
            //Windowing.SetForegroundWindow(this.Handle);
        }

        private void PeekPreview_Activated(object sender, EventArgs e)
        {
            Trace.WriteLine("Resetting activity timer...", string.Format("PeekPreview.PeekPreview_Activated [{0}]", System.Threading.Thread.CurrentThread.Name));

            FadeCloseCountdown.Enabled = false;
            MousePositionCheck.Enabled = true;

            if (FMUtils.WinApi.Helper.VisualStyle != FMUtils.WinApi.Helper.VisualStyles.Aero)
                this.BackColor = SystemColors.GradientActiveCaption;
        }

        private void PeekPreview_Deactivate(object sender, EventArgs e)
        {
            if (FMUtils.WinApi.Helper.VisualStyle != FMUtils.WinApi.Helper.VisualStyles.Aero)
                this.BackColor = SystemColors.GradientInactiveCaption;
        }

        private void PeekPreview_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Opacity == 1)
            {
                Trace.WriteLine("Preview window visible", string.Format("PeekPreview.PeekPreview_VisibleChanged [{0}]", System.Threading.Thread.CurrentThread.Name));

                if (Configuration.PreviewDelayTime > 0)
                    FadeCloseCountdown.Interval = Configuration.PreviewDelayTime * 1000;

                MousePositionCheck.Enabled = true;
                FadeCloseCountdown.Enabled = false;

                SetWindowPosition();
            }
            else
            {
                Trace.WriteLine("Preview window hidden", string.Format("PeekPreview.PeekPreview_VisibleChanged [{0}]", System.Threading.Thread.CurrentThread.Name));

                FadeCloseCountdown.Enabled = false;
                MousePositionCheck.Enabled = false;
            }
        }

        private void PeekPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            Trace.WriteLine(string.Format("Closing preview for reason: '{0}'...", e.CloseReason), string.Format("PeekPreview.PeekPreview_FormClosing [{0}]", System.Threading.Thread.CurrentThread.Name));

            if (e.CloseReason == CloseReason.ApplicationExitCall || e.CloseReason == CloseReason.WindowsShutDown)
            {
                e.Cancel = false;
            }
            else if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                FadeClose();
            }
            else
            {
                e.Cancel = true;
            }

            Trace.WriteLine(string.Format("Closing preview cancelled: '{0}'", e.Cancel), string.Format("PeekPreview.PeekPreview_FormClosing [{0}]", System.Threading.Thread.CurrentThread.Name));
        }

        private void PeekPreview_FormClosed(object sender, FormClosedEventArgs e)
        {
            Trace.WriteLine(string.Format("Preview closed: '{0}'", e.CloseReason), string.Format("PeekPreview.PeekPreview_FormClosed [{0}]", System.Threading.Thread.CurrentThread.Name));

            MousePositionCheck.Enabled = false;
            FadeCloseCountdown.Enabled = false;
            WriteCompleteCheck.Enabled = false;
        }

        private void PeekPreview_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //todo: Migrate to actions as well?

            switch (e.KeyCode)
            {
                case Keys.Left:
                    pnBackward_Click(sender, new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
                    break;

                case Keys.Right:
                    pnForward_Click(sender, new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
                    break;
            }
        }
        #endregion

        #region External actions
        internal void GroomBackForwardIcons()
        {
            if (this.InvokeRequired)
            {
                Trace.WriteLine("Self invoking...", string.Format("PeekPreview.GroomBackForwardIcons [{0}]", System.Threading.Thread.CurrentThread.Name));

                this.BeginInvoke(new MethodInvoker(() => GroomBackForwardIcons()));
                return;
            }

            var i = Program.History.IndexOf(CurrentScreenshot);
            BackwardsEnd = i == 0;
            ForwardsEnd = Program.History.Count - 1 == i;

            pnForward.BackgroundImage = pnForward.isMouseOver() ? (ForwardsEnd ? Resources.forward_empty_18x32_white : Resources.forward_18x32_white) : (ForwardsEnd ? Resources.forward_empty_18x32_black : Resources.forward_18x32_black);
            pnBackward.BackgroundImage = pnBackward.isMouseOver() ? (BackwardsEnd ? Resources.backward_empty_18x32_white : Resources.backward_18x32_white) : (BackwardsEnd ? Resources.backward_empty_18x32_black : Resources.backward_18x32_backward);
        }

        internal void GroomHeartIcon()
        {
            if (this.InvokeRequired)
            {
                Trace.WriteLine("Self invoking...", string.Format("PeekPreview.GroomHeartIcon [{0}]", System.Threading.Thread.CurrentThread.Name));

                this.BeginInvoke(new MethodInvoker(() => GroomHeartIcon()));
                return;
            }
            
            var flag = CurrentScreenshot != null && CurrentScreenshot.isFlagged;
            var p = Cursor.Position;
            var hover = this.Visible && pnHeart.Bounds.Contains(pnHeart.PointToClient(p));

            if (flag)
                pnHeart.BackgroundImage = hover ? Resources.heart_fill_white_32x38 : Resources.heart_fill_black_32x38;
            else
                pnHeart.BackgroundImage = hover ? Resources.heart_stroke_white_32x28 : Resources.heart_stroke_black_32x28;
        }

        internal void UploadStarted(object sender, UploaderProgressEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Trace.WriteLine("Self invoking...", string.Format("PeekPreview.UploadStarted [{0}]", System.Threading.Thread.CurrentThread.Name));

                this.BeginInvoke(new MethodInvoker(() => UploadStarted(sender, e)));
                return;
            }

            if (e.Screenshot != this.CurrentScreenshot)
                return;

            UploadAnimationIndex = 0;
            pnUpload.BackgroundImage = pnUpload.isMouseOver() ? ilUploadAnimationWhite.Images[UploadAnimationIndex] : ilUploadAnimationBlack.Images[UploadAnimationIndex];
        }

        internal void UploadProgress(object sender, UploaderProgressEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Trace.WriteLine("Self invoking...", string.Format("PeekPreview.UploadProgress [{0}]", System.Threading.Thread.CurrentThread.Name));

                this.BeginInvoke(new MethodInvoker(() => UploadProgress(sender, e)));
                return;
            }

            if (e.Screenshot != this.CurrentScreenshot)
                return;

            var i = ((int)e.Percent) / (ilUploadAnimationBlack.Images.Count - 1);
            if (i != UploadAnimationIndex)
            {
                if (i >= 0 && i < ilUploadAnimationBlack.Images.Count)
                {
                    Trace.WriteLine(string.Format("Uploading: '{0}'", i), string.Format("PeekPreview.UploadProgress [{0}]", System.Threading.Thread.CurrentThread.Name));

                    UploadAnimationIndex = i;
                    pnUpload.BackgroundImage = pnUpload.isMouseOver() ? ilUploadAnimationWhite.Images[UploadAnimationIndex] : ilUploadAnimationBlack.Images[UploadAnimationIndex];
                }
            }
        }

        internal void UploadEnded(object sender, UploaderEndedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Trace.WriteLine("Self invoking...", string.Format("PeekPreview.UploadEnded [{0}]", System.Threading.Thread.CurrentThread.Name));

                this.BeginInvoke(new MethodInvoker(() => UploadEnded(sender, e)));
                return;
            }
            
            if (e.Screenshot != this.CurrentScreenshot)
                return;

            UploadAnimationIndex = -1;
            pnUpload.BackgroundImage = pnUpload.isMouseOver() ? Resources.cloud_upload_link_white_32x32 : Resources.cloud_upload_link_black_32x32;

            ResetFadeCloseCountdown();
        }
        #endregion
    }
}
