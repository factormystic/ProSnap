namespace ProSnap
{
    partial class CrashReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrashReportForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbActionName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.allCrashFeedback = new System.Windows.Forms.LinkLabel();
            this.CancelPanel = new System.Windows.Forms.Panel();
            this.btConfirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.CancelPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ProSnap.Properties.Resources.works_on_my_machine_starburst;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(123, 109);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 72;
            this.pictureBox1.TabStop = false;
            // 
            // lbActionName
            // 
            this.lbActionName.AutoSize = true;
            this.lbActionName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbActionName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbActionName.Location = new System.Drawing.Point(144, 12);
            this.lbActionName.Margin = new System.Windows.Forms.Padding(60, 0, 60, 0);
            this.lbActionName.Name = "lbActionName";
            this.lbActionName.Size = new System.Drawing.Size(263, 30);
            this.lbActionName.TabIndex = 78;
            this.lbActionName.Text = "Unfortunately, your machine is not my machine,\r\nand the program has crashed.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(144, 106);
            this.label3.Margin = new System.Windows.Forms.Padding(60, 20, 60, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 15);
            this.label3.TabIndex = 82;
            this.label3.Text = "The program will now be closed.";
            // 
            // allCrashFeedback
            // 
            this.allCrashFeedback.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.allCrashFeedback.AutoSize = true;
            this.allCrashFeedback.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allCrashFeedback.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.allCrashFeedback.Location = new System.Drawing.Point(144, 52);
            this.allCrashFeedback.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.allCrashFeedback.Name = "allCrashFeedback";
            this.allCrashFeedback.Size = new System.Drawing.Size(259, 15);
            this.allCrashFeedback.TabIndex = 81;
            this.allCrashFeedback.TabStop = true;
            this.allCrashFeedback.Text = "Provide additional information about this crash.";
            this.allCrashFeedback.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.allCrashFeedback.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.allCrashFeedback_LinkClicked);
            // 
            // CancelPanel
            // 
            this.CancelPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelPanel.BackColor = System.Drawing.SystemColors.Control;
            this.CancelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CancelPanel.Controls.Add(this.btConfirm);
            this.CancelPanel.Location = new System.Drawing.Point(-1, 135);
            this.CancelPanel.Name = "CancelPanel";
            this.CancelPanel.Size = new System.Drawing.Size(424, 38);
            this.CancelPanel.TabIndex = 77;
            // 
            // btConfirm
            // 
            this.btConfirm.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btConfirm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btConfirm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btConfirm.Location = new System.Drawing.Point(338, 7);
            this.btConfirm.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.btConfirm.Name = "btConfirm";
            this.btConfirm.Size = new System.Drawing.Size(75, 23);
            this.btConfirm.TabIndex = 40;
            this.btConfirm.Text = "OK";
            this.btConfirm.UseVisualStyleBackColor = true;
            // 
            // CrashReportForm
            // 
            this.AcceptButton = this.btConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(422, 172);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.allCrashFeedback);
            this.Controls.Add(this.lbActionName);
            this.Controls.Add(this.CancelPanel);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CrashReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProSnap";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.CancelPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel CancelPanel;
        private System.Windows.Forms.Button btConfirm;
        private System.Windows.Forms.Label lbActionName;
        private System.Windows.Forms.LinkLabel allCrashFeedback;
        private System.Windows.Forms.Label label3;
    }
}