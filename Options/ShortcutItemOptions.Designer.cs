namespace ProSnap.Options
{
    partial class ShortcutItemConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortcutItemConfiguration));
            this.gbActions = new System.Windows.Forms.GroupBox();
            this.btMoveActionDown = new System.Windows.Forms.Button();
            this.btMoveActionUp = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btAddAction = new System.Windows.Forms.Button();
            this.btEditAction = new System.Windows.Forms.Button();
            this.btRemoveAction = new System.Windows.Forms.Button();
            this.lvActions = new ProSnap.ListView();
            this.chIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chActionName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btCancel = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.tbShortcut = new System.Windows.Forms.TextBox();
            this.gbKeyCombo = new System.Windows.Forms.GroupBox();
            this.cbRequirePreview = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbActions.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.gbKeyCombo.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbActions
            // 
            this.gbActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbActions.Controls.Add(this.btMoveActionDown);
            this.gbActions.Controls.Add(this.btMoveActionUp);
            this.gbActions.Controls.Add(this.flowLayoutPanel3);
            this.gbActions.Controls.Add(this.lvActions);
            this.gbActions.Location = new System.Drawing.Point(15, 134);
            this.gbActions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbActions.Name = "gbActions";
            this.gbActions.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbActions.Size = new System.Drawing.Size(364, 312);
            this.gbActions.TabIndex = 37;
            this.gbActions.TabStop = false;
            this.gbActions.Text = "Action Chain";
            // 
            // btMoveActionDown
            // 
            this.btMoveActionDown.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btMoveActionDown.Image = global::ProSnap.Properties.Resources.arrow_down;
            this.btMoveActionDown.Location = new System.Drawing.Point(329, 150);
            this.btMoveActionDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btMoveActionDown.Name = "btMoveActionDown";
            this.btMoveActionDown.Size = new System.Drawing.Size(28, 31);
            this.btMoveActionDown.TabIndex = 45;
            this.btMoveActionDown.UseVisualStyleBackColor = true;
            this.btMoveActionDown.Click += new System.EventHandler(this.btMoveActionDown_Click);
            // 
            // btMoveActionUp
            // 
            this.btMoveActionUp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btMoveActionUp.Image = global::ProSnap.Properties.Resources.arrow_up;
            this.btMoveActionUp.Location = new System.Drawing.Point(329, 111);
            this.btMoveActionUp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btMoveActionUp.Name = "btMoveActionUp";
            this.btMoveActionUp.Size = new System.Drawing.Size(28, 31);
            this.btMoveActionUp.TabIndex = 44;
            this.btMoveActionUp.UseVisualStyleBackColor = true;
            this.btMoveActionUp.Click += new System.EventHandler(this.btMoveActionUp_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this.btAddAction);
            this.flowLayoutPanel3.Controls.Add(this.btEditAction);
            this.flowLayoutPanel3.Controls.Add(this.btRemoveAction);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(4, 270);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(356, 38);
            this.flowLayoutPanel3.TabIndex = 31;
            // 
            // btAddAction
            // 
            this.btAddAction.AutoSize = true;
            this.btAddAction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btAddAction.Location = new System.Drawing.Point(4, 4);
            this.btAddAction.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btAddAction.Name = "btAddAction";
            this.btAddAction.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.btAddAction.Size = new System.Drawing.Size(96, 30);
            this.btAddAction.TabIndex = 29;
            this.btAddAction.Text = "Add...";
            this.btAddAction.UseVisualStyleBackColor = true;
            this.btAddAction.Click += new System.EventHandler(this.btAddAction_Click);
            // 
            // btEditAction
            // 
            this.btEditAction.AutoSize = true;
            this.btEditAction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btEditAction.Location = new System.Drawing.Point(108, 4);
            this.btEditAction.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btEditAction.Name = "btEditAction";
            this.btEditAction.Padding = new System.Windows.Forms.Padding(21, 0, 21, 0);
            this.btEditAction.Size = new System.Drawing.Size(96, 30);
            this.btEditAction.TabIndex = 31;
            this.btEditAction.Text = "Edit...";
            this.btEditAction.UseVisualStyleBackColor = true;
            this.btEditAction.Click += new System.EventHandler(this.btEditAction_Click);
            // 
            // btRemoveAction
            // 
            this.btRemoveAction.AutoSize = true;
            this.btRemoveAction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btRemoveAction.Location = new System.Drawing.Point(212, 4);
            this.btRemoveAction.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btRemoveAction.Name = "btRemoveAction";
            this.btRemoveAction.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.btRemoveAction.Size = new System.Drawing.Size(97, 30);
            this.btRemoveAction.TabIndex = 30;
            this.btRemoveAction.Text = "Remove";
            this.btRemoveAction.UseVisualStyleBackColor = true;
            this.btRemoveAction.Click += new System.EventHandler(this.btRemoveAction_Click);
            // 
            // lvActions
            // 
            this.lvActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chIndex,
            this.chActionName});
            this.lvActions.FullRowSelect = true;
            this.lvActions.HideSelection = false;
            this.lvActions.Location = new System.Drawing.Point(9, 31);
            this.lvActions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvActions.MultiSelect = false;
            this.lvActions.Name = "lvActions";
            this.lvActions.Size = new System.Drawing.Size(312, 230);
            this.lvActions.TabIndex = 28;
            this.lvActions.UseCompatibleStateImageBehavior = false;
            this.lvActions.View = System.Windows.Forms.View.Details;
            // 
            // chIndex
            // 
            this.chIndex.Text = "#";
            this.chIndex.Width = 23;
            // 
            // chActionName
            // 
            this.chActionName.Text = "Action";
            this.chActionName.Width = 256;
            // 
            // btCancel
            // 
            this.btCancel.AutoSize = true;
            this.btCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(281, 10);
            this.btCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btCancel.Name = "btCancel";
            this.btCancel.Padding = new System.Windows.Forms.Padding(16, 0, 18, 0);
            this.btCancel.Size = new System.Drawing.Size(97, 30);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btSave
            // 
            this.btSave.AutoSize = true;
            this.btSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btSave.Location = new System.Drawing.Point(174, 10);
            this.btSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btSave.Name = "btSave";
            this.btSave.Padding = new System.Windows.Forms.Padding(25, 0, 24, 0);
            this.btSave.Size = new System.Drawing.Size(99, 30);
            this.btSave.TabIndex = 0;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            // 
            // tbShortcut
            // 
            this.tbShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbShortcut.Location = new System.Drawing.Point(11, 28);
            this.tbShortcut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbShortcut.Name = "tbShortcut";
            this.tbShortcut.Size = new System.Drawing.Size(133, 27);
            this.tbShortcut.TabIndex = 5;
            this.tbShortcut.Enter += new System.EventHandler(this.tbShortcut_Enter);
            this.tbShortcut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbShortcut_KeyDown);
            this.tbShortcut.Leave += new System.EventHandler(this.tbShortcut_Leave);
            // 
            // gbKeyCombo
            // 
            this.gbKeyCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbKeyCombo.Controls.Add(this.cbRequirePreview);
            this.gbKeyCombo.Controls.Add(this.label1);
            this.gbKeyCombo.Controls.Add(this.tbShortcut);
            this.gbKeyCombo.Location = new System.Drawing.Point(15, 15);
            this.gbKeyCombo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbKeyCombo.Name = "gbKeyCombo";
            this.gbKeyCombo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbKeyCombo.Size = new System.Drawing.Size(364, 111);
            this.gbKeyCombo.TabIndex = 40;
            this.gbKeyCombo.TabStop = false;
            this.gbKeyCombo.Text = "Key Combo";
            // 
            // cbRequirePreview
            // 
            this.cbRequirePreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbRequirePreview.AutoSize = true;
            this.cbRequirePreview.Location = new System.Drawing.Point(11, 80);
            this.cbRequirePreview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbRequirePreview.Name = "cbRequirePreview";
            this.cbRequirePreview.Size = new System.Drawing.Size(261, 24);
            this.cbRequirePreview.TabIndex = 7;
            this.cbRequirePreview.Text = "Only active when preview has focus";
            this.cbRequirePreview.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(152, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 40);
            this.label1.TabIndex = 6;
            this.label1.Text = "Press the key combo that will\r\nbegin this action chain.";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.btCancel);
            this.flowLayoutPanel1.Controls.Add(this.btSave);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(-1, 455);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(396, 51);
            this.flowLayoutPanel1.TabIndex = 38;
            // 
            // ShortcutItemConfiguration
            // 
            this.AcceptButton = this.btSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(394, 505);
            this.Controls.Add(this.gbKeyCombo);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.gbActions);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShortcutItemConfiguration";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shortcut Item";
            this.gbActions.ResumeLayout(false);
            this.gbActions.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.gbKeyCombo.ResumeLayout(false);
            this.gbKeyCombo.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbActions;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button btAddAction;
        private System.Windows.Forms.Button btRemoveAction;
        private ListView lvActions;
        private System.Windows.Forms.ColumnHeader chActionName;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        internal System.Windows.Forms.TextBox tbShortcut;
        private System.Windows.Forms.ColumnHeader chIndex;
        private System.Windows.Forms.GroupBox gbKeyCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btMoveActionDown;
        private System.Windows.Forms.Button btMoveActionUp;
        private System.Windows.Forms.Button btEditAction;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox cbRequirePreview;
    }
}