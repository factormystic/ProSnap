namespace ProSnap
{
    partial class UploadedFileDetails
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
            this.btCopyLink = new System.Windows.Forms.Button();
            this.btCopyDeleteLink = new System.Windows.Forms.Button();
            this.lbInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btCopyLink
            // 
            this.btCopyLink.Location = new System.Drawing.Point(12, 12);
            this.btCopyLink.Name = "btCopyLink";
            this.btCopyLink.Size = new System.Drawing.Size(135, 23);
            this.btCopyLink.TabIndex = 0;
            this.btCopyLink.Text = "Copy image link";
            this.btCopyLink.UseVisualStyleBackColor = true;
            this.btCopyLink.Click += new System.EventHandler(this.btCopyLink_Click);
            // 
            // btCopyDeleteLink
            // 
            this.btCopyDeleteLink.Location = new System.Drawing.Point(12, 41);
            this.btCopyDeleteLink.Name = "btCopyDeleteLink";
            this.btCopyDeleteLink.Size = new System.Drawing.Size(135, 23);
            this.btCopyDeleteLink.TabIndex = 2;
            this.btCopyDeleteLink.Text = "Copy delete link";
            this.btCopyDeleteLink.UseVisualStyleBackColor = true;
            this.btCopyDeleteLink.Click += new System.EventHandler(this.btCopyDeleteLink_Click);
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.ForeColor = System.Drawing.Color.White;
            this.lbInfo.Location = new System.Drawing.Point(14, 23);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(131, 30);
            this.lbInfo.TabIndex = 3;
            this.lbInfo.Text = "This screenshot has not\r\nbeen uploaded yet.";
            this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbInfo.Visible = false;
            // 
            // UploadedFileDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(159, 90);
            this.ControlBox = false;
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.btCopyDeleteLink);
            this.Controls.Add(this.btCopyLink);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UploadedFileDetails";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btCopyLink;
        private System.Windows.Forms.Button btCopyDeleteLink;
        private System.Windows.Forms.Label lbInfo;
    }
}