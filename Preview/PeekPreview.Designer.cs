namespace ProSnap
{
    partial class PeekPreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PeekPreview));
            this.cmsEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditingOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOpenInEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRoundCorners = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWindowShadow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMouseCursor = new System.Windows.Forms.ToolStripMenuItem();
            this.pnButtons = new System.Windows.Forms.Panel();
            this.pnHeart = new System.Windows.Forms.Panel();
            this.pnBackward = new System.Windows.Forms.Panel();
            this.pnForward = new System.Windows.Forms.Panel();
            this.pnDelete = new System.Windows.Forms.Panel();
            this.pnEdit = new System.Windows.Forms.Panel();
            this.pnUpload = new System.Windows.Forms.Panel();
            this.pnSave = new System.Windows.Forms.Panel();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.ilUploadAnimationBlack = new System.Windows.Forms.ImageList(this.components);
            this.ilUploadAnimationWhite = new System.Windows.Forms.ImageList(this.components);
            this.cmsUpload = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiUploadingOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiUploadImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyImageLink = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyDeleteLink = new System.Windows.Forms.ToolStripMenuItem();
            this.pnPreview = new System.Windows.Forms.Panel();
            this.cmsEdit.SuspendLayout();
            this.pnButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.cmsUpload.SuspendLayout();
            this.pnPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsEdit
            // 
            this.cmsEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditingOptions,
            this.toolStripSeparator2,
            this.tsmiOpenInEditor,
            this.tsmiRoundCorners,
            this.tsmiWindowShadow,
            this.tsmiMouseCursor});
            this.cmsEdit.Name = "contextMenuStrip1";
            this.cmsEdit.ShowCheckMargin = true;
            this.cmsEdit.ShowImageMargin = false;
            this.cmsEdit.Size = new System.Drawing.Size(207, 120);
            this.cmsEdit.MouseLeave += new System.EventHandler(this.cmsEdit_MouseLeave);
            // 
            // tsmiEditingOptions
            // 
            this.tsmiEditingOptions.Name = "tsmiEditingOptions";
            this.tsmiEditingOptions.Size = new System.Drawing.Size(206, 22);
            this.tsmiEditingOptions.Text = "Editing Options";
            this.tsmiEditingOptions.Click += new System.EventHandler(this.tsmiEditingOptions_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(203, 6);
            // 
            // tsmiOpenInEditor
            // 
            this.tsmiOpenInEditor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmiOpenInEditor.Name = "tsmiOpenInEditor";
            this.tsmiOpenInEditor.Size = new System.Drawing.Size(206, 22);
            this.tsmiOpenInEditor.Text = "Open in default editor...";
            this.tsmiOpenInEditor.Click += new System.EventHandler(this.tsmiOpenInEditor_Click);
            // 
            // tsmiRoundCorners
            // 
            this.tsmiRoundCorners.CheckOnClick = true;
            this.tsmiRoundCorners.Name = "tsmiRoundCorners";
            this.tsmiRoundCorners.Size = new System.Drawing.Size(206, 22);
            this.tsmiRoundCorners.Text = "Round Corners";
            this.tsmiRoundCorners.CheckedChanged += new System.EventHandler(this.tsmiRoundCorners_CheckedChanged);
            // 
            // tsmiWindowShadow
            // 
            this.tsmiWindowShadow.CheckOnClick = true;
            this.tsmiWindowShadow.Name = "tsmiWindowShadow";
            this.tsmiWindowShadow.Size = new System.Drawing.Size(206, 22);
            this.tsmiWindowShadow.Text = "Window Shadow";
            this.tsmiWindowShadow.CheckedChanged += new System.EventHandler(this.tsmiWindowShadow_CheckedChanged);
            // 
            // tsmiMouseCursor
            // 
            this.tsmiMouseCursor.CheckOnClick = true;
            this.tsmiMouseCursor.Name = "tsmiMouseCursor";
            this.tsmiMouseCursor.Size = new System.Drawing.Size(206, 22);
            this.tsmiMouseCursor.Text = "Mouse Cursor";
            this.tsmiMouseCursor.CheckedChanged += new System.EventHandler(this.tsmiMouseCursor_CheckedChanged);
            // 
            // pnButtons
            // 
            this.pnButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnButtons.Controls.Add(this.pnHeart);
            this.pnButtons.Controls.Add(this.pnBackward);
            this.pnButtons.Controls.Add(this.pnForward);
            this.pnButtons.Controls.Add(this.pnDelete);
            this.pnButtons.Controls.Add(this.pnEdit);
            this.pnButtons.Controls.Add(this.pnUpload);
            this.pnButtons.Controls.Add(this.pnSave);
            this.pnButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnButtons.Location = new System.Drawing.Point(0, 442);
            this.pnButtons.Margin = new System.Windows.Forms.Padding(0);
            this.pnButtons.Name = "pnButtons";
            this.pnButtons.Size = new System.Drawing.Size(591, 35);
            this.pnButtons.TabIndex = 7;
            // 
            // pnHeart
            // 
            this.pnHeart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pnHeart.BackColor = System.Drawing.Color.Transparent;
            this.pnHeart.BackgroundImage = global::ProSnap.Properties.Resources.heart_stroke_black_32x28;
            this.pnHeart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnHeart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnHeart.ForeColor = System.Drawing.Color.Transparent;
            this.pnHeart.Location = new System.Drawing.Point(3, 9);
            this.pnHeart.Name = "pnHeart";
            this.pnHeart.Size = new System.Drawing.Size(21, 21);
            this.pnHeart.TabIndex = 9;
            this.pnHeart.Click += new System.EventHandler(this.pnHeart_Click);
            this.pnHeart.MouseEnter += new System.EventHandler(this.pnHeart_MouseEnter);
            this.pnHeart.MouseLeave += new System.EventHandler(this.pnHeart_MouseLeave);
            // 
            // pnBackward
            // 
            this.pnBackward.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pnBackward.BackColor = System.Drawing.Color.Transparent;
            this.pnBackward.BackgroundImage = global::ProSnap.Properties.Resources.backward_18x32_backward;
            this.pnBackward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnBackward.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnBackward.ForeColor = System.Drawing.Color.Transparent;
            this.pnBackward.Location = new System.Drawing.Point(552, 9);
            this.pnBackward.Name = "pnBackward";
            this.pnBackward.Size = new System.Drawing.Size(21, 21);
            this.pnBackward.TabIndex = 12;
            this.pnBackward.Click += new System.EventHandler(this.pnBackward_Click);
            this.pnBackward.MouseEnter += new System.EventHandler(this.pnBackward_MouseEnter);
            this.pnBackward.MouseLeave += new System.EventHandler(this.pnBackward_MouseLeave);
            // 
            // pnForward
            // 
            this.pnForward.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pnForward.BackColor = System.Drawing.Color.Transparent;
            this.pnForward.BackgroundImage = global::ProSnap.Properties.Resources.forward_18x32_black;
            this.pnForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnForward.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnForward.ForeColor = System.Drawing.Color.Transparent;
            this.pnForward.Location = new System.Drawing.Point(572, 9);
            this.pnForward.Name = "pnForward";
            this.pnForward.Size = new System.Drawing.Size(21, 21);
            this.pnForward.TabIndex = 11;
            this.pnForward.Click += new System.EventHandler(this.pnForward_Click);
            this.pnForward.MouseEnter += new System.EventHandler(this.pnForward_MouseEnter);
            this.pnForward.MouseLeave += new System.EventHandler(this.pnForward_MouseLeave);
            // 
            // pnDelete
            // 
            this.pnDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnDelete.BackColor = System.Drawing.Color.Transparent;
            this.pnDelete.BackgroundImage = global::ProSnap.Properties.Resources.x_28x28_black;
            this.pnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnDelete.ForeColor = System.Drawing.Color.Transparent;
            this.pnDelete.Location = new System.Drawing.Point(342, 5);
            this.pnDelete.Name = "pnDelete";
            this.pnDelete.Size = new System.Drawing.Size(25, 25);
            this.pnDelete.TabIndex = 11;
            this.pnDelete.Click += new System.EventHandler(this.pnDelete_Click);
            this.pnDelete.MouseEnter += new System.EventHandler(this.pnDelete_MouseEnter);
            this.pnDelete.MouseLeave += new System.EventHandler(this.pnDelete_MouseLeave);
            // 
            // pnEdit
            // 
            this.pnEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnEdit.BackColor = System.Drawing.Color.Transparent;
            this.pnEdit.BackgroundImage = global::ProSnap.Properties.Resources.image_32x32_black;
            this.pnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnEdit.ForeColor = System.Drawing.Color.Transparent;
            this.pnEdit.Location = new System.Drawing.Point(302, 1);
            this.pnEdit.Name = "pnEdit";
            this.pnEdit.Size = new System.Drawing.Size(34, 34);
            this.pnEdit.TabIndex = 10;
            this.pnEdit.Click += new System.EventHandler(this.pnEdit_Click);
            this.pnEdit.MouseEnter += new System.EventHandler(this.pnEdit_MouseEnter);
            this.pnEdit.MouseLeave += new System.EventHandler(this.pnEdit_MouseLeave);
            this.pnEdit.MouseHover += new System.EventHandler(this.pnEdit_MouseHover);
            // 
            // pnUpload
            // 
            this.pnUpload.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnUpload.BackColor = System.Drawing.Color.Transparent;
            this.pnUpload.BackgroundImage = global::ProSnap.Properties.Resources.cloud_upload_32x32_black;
            this.pnUpload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnUpload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnUpload.ForeColor = System.Drawing.Color.Transparent;
            this.pnUpload.Location = new System.Drawing.Point(262, 0);
            this.pnUpload.Name = "pnUpload";
            this.pnUpload.Size = new System.Drawing.Size(34, 34);
            this.pnUpload.TabIndex = 9;
            this.pnUpload.Click += new System.EventHandler(this.pnUpload_Click);
            this.pnUpload.MouseEnter += new System.EventHandler(this.pnUpload_MouseEnter);
            this.pnUpload.MouseLeave += new System.EventHandler(this.pnUpload_MouseLeave);
            this.pnUpload.MouseHover += new System.EventHandler(this.pnUpload_MouseHover);
            // 
            // pnSave
            // 
            this.pnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnSave.BackColor = System.Drawing.Color.Transparent;
            this.pnSave.BackgroundImage = global::ProSnap.Properties.Resources.document_stroke_32x32_black;
            this.pnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnSave.ForeColor = System.Drawing.Color.Transparent;
            this.pnSave.Location = new System.Drawing.Point(222, 1);
            this.pnSave.Name = "pnSave";
            this.pnSave.Size = new System.Drawing.Size(34, 34);
            this.pnSave.TabIndex = 8;
            this.pnSave.Click += new System.EventHandler(this.pnSave_Click);
            this.pnSave.MouseEnter += new System.EventHandler(this.pnSave_MouseEnter);
            this.pnSave.MouseLeave += new System.EventHandler(this.pnSave_MouseLeave);
            // 
            // pbPreview
            // 
            this.pbPreview.BackColor = System.Drawing.Color.Transparent;
            this.pbPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPreview.Location = new System.Drawing.Point(122, 77);
            this.pbPreview.Margin = new System.Windows.Forms.Padding(0);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(336, 280);
            this.pbPreview.TabIndex = 1;
            this.pbPreview.TabStop = false;
            this.pbPreview.Click += new System.EventHandler(this.pbPreview_Click);
            this.pbPreview.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.pbPreview_QueryContinueDrag);
            this.pbPreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbPreview_MouseDown);
            // 
            // ilUploadAnimationBlack
            // 
            this.ilUploadAnimationBlack.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilUploadAnimationBlack.ImageStream")));
            this.ilUploadAnimationBlack.TransparentColor = System.Drawing.Color.Transparent;
            this.ilUploadAnimationBlack.Images.SetKeyName(0, "cloud_upload_10_32x32.png");
            this.ilUploadAnimationBlack.Images.SetKeyName(1, "cloud_upload_20_32x32.png");
            this.ilUploadAnimationBlack.Images.SetKeyName(2, "cloud_upload_30_32x32.png");
            this.ilUploadAnimationBlack.Images.SetKeyName(3, "cloud_upload_40_32x32.png");
            this.ilUploadAnimationBlack.Images.SetKeyName(4, "cloud_upload_50_32x32.png");
            this.ilUploadAnimationBlack.Images.SetKeyName(5, "cloud_upload_60_32x32.png");
            this.ilUploadAnimationBlack.Images.SetKeyName(6, "cloud_upload_70_32x32.png");
            this.ilUploadAnimationBlack.Images.SetKeyName(7, "cloud_upload_80_32x32.png");
            this.ilUploadAnimationBlack.Images.SetKeyName(8, "cloud_upload_90_32x32.png");
            this.ilUploadAnimationBlack.Images.SetKeyName(9, "cloud_upload_100_32x32.png");
            // 
            // ilUploadAnimationWhite
            // 
            this.ilUploadAnimationWhite.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilUploadAnimationWhite.ImageStream")));
            this.ilUploadAnimationWhite.TransparentColor = System.Drawing.Color.Transparent;
            this.ilUploadAnimationWhite.Images.SetKeyName(0, "cloud_upload_white_10_32x32.png");
            this.ilUploadAnimationWhite.Images.SetKeyName(1, "cloud_upload_white_20_32x32.png");
            this.ilUploadAnimationWhite.Images.SetKeyName(2, "cloud_upload_white_30_32x32.png");
            this.ilUploadAnimationWhite.Images.SetKeyName(3, "cloud_upload_white_40_32x32.png");
            this.ilUploadAnimationWhite.Images.SetKeyName(4, "cloud_upload_white_50_32x32.png");
            this.ilUploadAnimationWhite.Images.SetKeyName(5, "cloud_upload_white_60_32x32.png");
            this.ilUploadAnimationWhite.Images.SetKeyName(6, "cloud_upload_white_70_32x32.png");
            this.ilUploadAnimationWhite.Images.SetKeyName(7, "cloud_upload_white_80_32x32.png");
            this.ilUploadAnimationWhite.Images.SetKeyName(8, "cloud_upload_white_90_32x32.png");
            this.ilUploadAnimationWhite.Images.SetKeyName(9, "cloud_upload_white_100_32x32.png");
            // 
            // cmsUpload
            // 
            this.cmsUpload.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUploadingOptions,
            this.toolStripSeparator1,
            this.tsmiUploadImage,
            this.tsmiCopyImageLink,
            this.tsmiCopyDeleteLink});
            this.cmsUpload.Name = "contextMenuStrip1";
            this.cmsUpload.Size = new System.Drawing.Size(175, 98);
            this.cmsUpload.MouseLeave += new System.EventHandler(this.cmsUpload_MouseLeave);
            // 
            // tsmiUploadingOptions
            // 
            this.tsmiUploadingOptions.Name = "tsmiUploadingOptions";
            this.tsmiUploadingOptions.Size = new System.Drawing.Size(174, 22);
            this.tsmiUploadingOptions.Text = "Uploading Options";
            this.tsmiUploadingOptions.Click += new System.EventHandler(this.tsmiUploadingOptions_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(171, 6);
            // 
            // tsmiUploadImage
            // 
            this.tsmiUploadImage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmiUploadImage.Name = "tsmiUploadImage";
            this.tsmiUploadImage.Size = new System.Drawing.Size(174, 22);
            this.tsmiUploadImage.Text = "Upload image";
            this.tsmiUploadImage.Click += new System.EventHandler(this.tsmiUploadImage_Click);
            // 
            // tsmiCopyImageLink
            // 
            this.tsmiCopyImageLink.Enabled = false;
            this.tsmiCopyImageLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmiCopyImageLink.Name = "tsmiCopyImageLink";
            this.tsmiCopyImageLink.Size = new System.Drawing.Size(174, 22);
            this.tsmiCopyImageLink.Text = "Copy image link";
            this.tsmiCopyImageLink.Click += new System.EventHandler(this.tsmiCopyImageLink_Click);
            // 
            // tsmiCopyDeleteLink
            // 
            this.tsmiCopyDeleteLink.Enabled = false;
            this.tsmiCopyDeleteLink.Name = "tsmiCopyDeleteLink";
            this.tsmiCopyDeleteLink.Size = new System.Drawing.Size(174, 22);
            this.tsmiCopyDeleteLink.Text = "Copy deletion link";
            this.tsmiCopyDeleteLink.Click += new System.EventHandler(this.tsmiCopyDeleteLink_Click);
            // 
            // pnPreview
            // 
            this.pnPreview.Controls.Add(this.pbPreview);
            this.pnPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnPreview.Location = new System.Drawing.Point(0, 0);
            this.pnPreview.Name = "pnPreview";
            this.pnPreview.Size = new System.Drawing.Size(591, 442);
            this.pnPreview.TabIndex = 8;
            // 
            // PeekPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(591, 477);
            this.ControlBox = false;
            this.Controls.Add(this.pnPreview);
            this.Controls.Add(this.pnButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(280, 180);
            this.Name = "PeekPreview";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Activated += new System.EventHandler(this.PeekPreview_Activated);
            this.Deactivate += new System.EventHandler(this.PeekPreview_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PeekPreview_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PeekPreview_FormClosed);
            this.Load += new System.EventHandler(this.PeekPreview_Load);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PeekPreview_PreviewKeyDown);
            this.cmsEdit.ResumeLayout(false);
            this.pnButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.cmsUpload.ResumeLayout(false);
            this.pnPreview.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cmsEdit;
        private System.Windows.Forms.Panel pnButtons;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.ToolStripMenuItem tsmiRoundCorners;
        private System.Windows.Forms.ToolStripMenuItem tsmiWindowShadow;
        private System.Windows.Forms.Panel pnSave;
        private System.Windows.Forms.Panel pnUpload;
        private System.Windows.Forms.Panel pnDelete;
        private System.Windows.Forms.Panel pnEdit;
        private System.Windows.Forms.Panel pnBackward;
        private System.Windows.Forms.Panel pnForward;
        private System.Windows.Forms.ToolStripMenuItem tsmiMouseCursor;
        private System.Windows.Forms.ImageList ilUploadAnimationBlack;
        private System.Windows.Forms.ImageList ilUploadAnimationWhite;
        private System.Windows.Forms.ContextMenuStrip cmsUpload;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyImageLink;
        private System.Windows.Forms.ToolStripMenuItem tsmiUploadingOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyDeleteLink;
        private System.Windows.Forms.Panel pnHeart;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditingOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel pnPreview;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenInEditor;
        private System.Windows.Forms.ToolStripMenuItem tsmiUploadImage;
    }
}