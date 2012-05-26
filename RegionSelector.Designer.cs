namespace ProSnap
{
    partial class RegionSelector
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.llTakeScreenshotTopLeft = new System.Windows.Forms.LinkLabel();
            this.llTakeScreenshotBottomRight = new System.Windows.Forms.LinkLabel();
            this.llTakeScreenshotTopRight = new System.Windows.Forms.LinkLabel();
            this.llTakeScreenshotBottomLeft = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Fuchsia;
            this.panel1.Location = new System.Drawing.Point(20, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 290);
            this.panel1.TabIndex = 0;
            // 
            // llTakeScreenshotTopLeft
            // 
            this.llTakeScreenshotTopLeft.AutoSize = true;
            this.llTakeScreenshotTopLeft.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(69)))), ((int)(((byte)(173)))));
            this.llTakeScreenshotTopLeft.Location = new System.Drawing.Point(17, 2);
            this.llTakeScreenshotTopLeft.Name = "llTakeScreenshotTopLeft";
            this.llTakeScreenshotTopLeft.Size = new System.Drawing.Size(93, 15);
            this.llTakeScreenshotTopLeft.TabIndex = 1;
            this.llTakeScreenshotTopLeft.TabStop = true;
            this.llTakeScreenshotTopLeft.Text = "Take Screenshot";
            this.llTakeScreenshotTopLeft.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // llTakeScreenshotBottomRight
            // 
            this.llTakeScreenshotBottomRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llTakeScreenshotBottomRight.AutoSize = true;
            this.llTakeScreenshotBottomRight.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(69)))), ((int)(((byte)(173)))));
            this.llTakeScreenshotBottomRight.Location = new System.Drawing.Point(217, 313);
            this.llTakeScreenshotBottomRight.Name = "llTakeScreenshotBottomRight";
            this.llTakeScreenshotBottomRight.Size = new System.Drawing.Size(93, 15);
            this.llTakeScreenshotBottomRight.TabIndex = 2;
            this.llTakeScreenshotBottomRight.TabStop = true;
            this.llTakeScreenshotBottomRight.Text = "Take Screenshot";
            this.llTakeScreenshotBottomRight.Visible = false;
            // 
            // llTakeScreenshotTopRight
            // 
            this.llTakeScreenshotTopRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llTakeScreenshotTopRight.AutoSize = true;
            this.llTakeScreenshotTopRight.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(69)))), ((int)(((byte)(173)))));
            this.llTakeScreenshotTopRight.Location = new System.Drawing.Point(217, 2);
            this.llTakeScreenshotTopRight.Name = "llTakeScreenshotTopRight";
            this.llTakeScreenshotTopRight.Size = new System.Drawing.Size(93, 15);
            this.llTakeScreenshotTopRight.TabIndex = 3;
            this.llTakeScreenshotTopRight.TabStop = true;
            this.llTakeScreenshotTopRight.Text = "Take Screenshot";
            this.llTakeScreenshotTopRight.Visible = false;
            // 
            // llTakeScreenshotBottomLeft
            // 
            this.llTakeScreenshotBottomLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llTakeScreenshotBottomLeft.AutoSize = true;
            this.llTakeScreenshotBottomLeft.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(69)))), ((int)(((byte)(173)))));
            this.llTakeScreenshotBottomLeft.Location = new System.Drawing.Point(17, 313);
            this.llTakeScreenshotBottomLeft.Name = "llTakeScreenshotBottomLeft";
            this.llTakeScreenshotBottomLeft.Size = new System.Drawing.Size(93, 15);
            this.llTakeScreenshotBottomLeft.TabIndex = 4;
            this.llTakeScreenshotBottomLeft.TabStop = true;
            this.llTakeScreenshotBottomLeft.Text = "Take Screenshot";
            this.llTakeScreenshotBottomLeft.Visible = false;
            // 
            // RegionSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(330, 330);
            this.Controls.Add(this.llTakeScreenshotBottomLeft);
            this.Controls.Add(this.llTakeScreenshotTopRight);
            this.Controls.Add(this.llTakeScreenshotBottomRight);
            this.Controls.Add(this.llTakeScreenshotTopLeft);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(41, 41);
            this.Name = "RegionSelector";
            this.Opacity = 0.75D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.LocationChanged += new System.EventHandler(this.RegionSelector_LocationChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegionSelector_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel llTakeScreenshotTopLeft;
        private System.Windows.Forms.LinkLabel llTakeScreenshotBottomRight;
        private System.Windows.Forms.LinkLabel llTakeScreenshotTopRight;
        private System.Windows.Forms.LinkLabel llTakeScreenshotBottomLeft;
    }
}