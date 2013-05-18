namespace ProSnap.Options
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Global Shortcuts", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Preview Window Shortcuts", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Save", 0);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Upload", 1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Edit", 2);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "Delete"}, 3, System.Drawing.Color.Empty, System.Drawing.SystemColors.Window, null);
            this.btClose = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ilTabIcons = new System.Windows.Forms.ImageList(this.components);
            this.flpTabContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.btGeneralTab = new System.Windows.Forms.Button();
            this.btShortcutsTab = new System.Windows.Forms.Button();
            this.btPreviewTab = new System.Windows.Forms.Button();
            this.btUploadingTab = new System.Windows.Forms.Button();
            this.btRegisterTab = new System.Windows.Forms.Button();
            this.btAboutTab = new System.Windows.Forms.Button();
            this.pnWindowButtons = new System.Windows.Forms.Panel();
            this.llResetOptions = new System.Windows.Forms.LinkLabel();
            this.tcOptions = new ProSnap.TablessControl();
            this.tpPreviewWindow = new System.Windows.Forms.TabPage();
            this.btCrash = new System.Windows.Forms.Button();
            this.cbDefaultFileType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.rbPreviewTakeFocus = new System.Windows.Forms.RadioButton();
            this.rbPreviewLeaveFocus = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudDelayTime = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.pnPreviewLocationChooser = new System.Windows.Forms.Panel();
            this.cbPreviewCenter = new System.Windows.Forms.CheckBox();
            this.cbPreviewLowerRight = new System.Windows.Forms.CheckBox();
            this.cbPreviewUpperRight = new System.Windows.Forms.CheckBox();
            this.cbPreviewLowerLeft = new System.Windows.Forms.CheckBox();
            this.cbPreviewUpperLeft = new System.Windows.Forms.CheckBox();
            this.pbDesktopPreview = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tpShortcuts = new System.Windows.Forms.TabPage();
            this.lvShortcuts = new ProSnap.ListView();
            this.chKeyCombo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chActions = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.flpShortcutActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btAddShortcut = new System.Windows.Forms.Button();
            this.btEditShortcut = new System.Windows.Forms.Button();
            this.btRemoveShortcut = new System.Windows.Forms.Button();
            this.tpButtons = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new ProSnap.ListView();
            this.tpUploading = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.lvUploadProfiles = new ProSnap.ListView();
            this.chUploadService = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.flpUploadServiceButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btAddUploadService = new System.Windows.Forms.Button();
            this.btEditUploadService = new System.Windows.Forms.Button();
            this.btRemoveUploadService = new System.Windows.Forms.Button();
            this.tpRegistration = new System.Windows.Forms.TabPage();
            this.label17 = new System.Windows.Forms.Label();
            this.tbRegistrationKey = new System.Windows.Forms.TextBox();
            this.llRegistrationKey = new System.Windows.Forms.LinkLabel();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tpAbout = new System.Windows.Forms.TabPage();
            this.gbLicenseDeclarations = new System.Windows.Forms.GroupBox();
            this.tbLicenseDeclarations = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbVersion = new System.Windows.Forms.Label();
            this.btManualUpdate = new System.Windows.Forms.Button();
            this.btToggleStartupTask = new System.Windows.Forms.Button();
            this.flpTabContainer.SuspendLayout();
            this.pnWindowButtons.SuspendLayout();
            this.tcOptions.SuspendLayout();
            this.tpPreviewWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayTime)).BeginInit();
            this.pnPreviewLocationChooser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDesktopPreview)).BeginInit();
            this.tpShortcuts.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flpShortcutActions.SuspendLayout();
            this.tpButtons.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpUploading.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flpUploadServiceButtons.SuspendLayout();
            this.tpRegistration.SuspendLayout();
            this.tpAbout.SuspendLayout();
            this.gbLicenseDeclarations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.AutoSize = true;
            this.btClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btClose.Location = new System.Drawing.Point(548, 10);
            this.btClose.Margin = new System.Windows.Forms.Padding(4);
            this.btClose.Name = "btClose";
            this.btClose.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.btClose.Size = new System.Drawing.Size(85, 30);
            this.btClose.TabIndex = 0;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "document_stroke_32x32_black.png");
            this.imageList1.Images.SetKeyName(1, "cloud_upload_32x32_black.png");
            this.imageList1.Images.SetKeyName(2, "image_32x32_black.png");
            this.imageList1.Images.SetKeyName(3, "x_28x28_black.png");
            // 
            // ilTabIcons
            // 
            this.ilTabIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTabIcons.ImageStream")));
            this.ilTabIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTabIcons.Images.SetKeyName(0, "cog_alt_32x32.png");
            this.ilTabIcons.Images.SetKeyName(1, "at_32x32.png");
            this.ilTabIcons.Images.SetKeyName(2, "cloud_upload_32x32.png");
            this.ilTabIcons.Images.SetKeyName(3, "check_32x26.png");
            // 
            // flpTabContainer
            // 
            this.flpTabContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpTabContainer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.flpTabContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpTabContainer.Controls.Add(this.btGeneralTab);
            this.flpTabContainer.Controls.Add(this.btShortcutsTab);
            this.flpTabContainer.Controls.Add(this.btPreviewTab);
            this.flpTabContainer.Controls.Add(this.btUploadingTab);
            this.flpTabContainer.Controls.Add(this.btRegisterTab);
            this.flpTabContainer.Controls.Add(this.btAboutTab);
            this.flpTabContainer.Location = new System.Drawing.Point(-1, -2);
            this.flpTabContainer.Name = "flpTabContainer";
            this.flpTabContainer.Size = new System.Drawing.Size(647, 65);
            this.flpTabContainer.TabIndex = 2;
            // 
            // btGeneralTab
            // 
            this.btGeneralTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btGeneralTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btGeneralTab.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btGeneralTab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btGeneralTab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btGeneralTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btGeneralTab.Image = global::ProSnap.Properties.Resources.cog_alt_32x32;
            this.btGeneralTab.Location = new System.Drawing.Point(6, 0);
            this.btGeneralTab.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btGeneralTab.Name = "btGeneralTab";
            this.btGeneralTab.Size = new System.Drawing.Size(75, 65);
            this.btGeneralTab.TabIndex = 3;
            this.btGeneralTab.Text = "\r\nGeneral";
            this.btGeneralTab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btGeneralTab.UseVisualStyleBackColor = false;
            this.btGeneralTab.Click += new System.EventHandler(this.TabButton_Click);
            // 
            // btShortcutsTab
            // 
            this.btShortcutsTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btShortcutsTab.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btShortcutsTab.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btShortcutsTab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btShortcutsTab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btShortcutsTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btShortcutsTab.Image = global::ProSnap.Properties.Resources.at_32x32;
            this.btShortcutsTab.Location = new System.Drawing.Point(87, 0);
            this.btShortcutsTab.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btShortcutsTab.Name = "btShortcutsTab";
            this.btShortcutsTab.Size = new System.Drawing.Size(80, 65);
            this.btShortcutsTab.TabIndex = 3;
            this.btShortcutsTab.Text = "\r\nShortcuts";
            this.btShortcutsTab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btShortcutsTab.UseVisualStyleBackColor = false;
            this.btShortcutsTab.Click += new System.EventHandler(this.TabButton_Click);
            // 
            // btPreviewTab
            // 
            this.btPreviewTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btPreviewTab.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btPreviewTab.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btPreviewTab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btPreviewTab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btPreviewTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btPreviewTab.Image = global::ProSnap.Properties.Resources.image_32x32_black;
            this.btPreviewTab.Location = new System.Drawing.Point(173, 0);
            this.btPreviewTab.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btPreviewTab.Name = "btPreviewTab";
            this.btPreviewTab.Size = new System.Drawing.Size(75, 65);
            this.btPreviewTab.TabIndex = 4;
            this.btPreviewTab.Text = "\r\nPreview";
            this.btPreviewTab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btPreviewTab.UseVisualStyleBackColor = false;
            this.btPreviewTab.Visible = false;
            this.btPreviewTab.Click += new System.EventHandler(this.TabButton_Click);
            // 
            // btUploadingTab
            // 
            this.btUploadingTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btUploadingTab.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btUploadingTab.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btUploadingTab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btUploadingTab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btUploadingTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btUploadingTab.Image = global::ProSnap.Properties.Resources.cloud_upload_32x32_black;
            this.btUploadingTab.Location = new System.Drawing.Point(254, 0);
            this.btUploadingTab.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btUploadingTab.Name = "btUploadingTab";
            this.btUploadingTab.Size = new System.Drawing.Size(75, 65);
            this.btUploadingTab.TabIndex = 3;
            this.btUploadingTab.Text = "\r\nUpload";
            this.btUploadingTab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btUploadingTab.UseVisualStyleBackColor = false;
            this.btUploadingTab.Click += new System.EventHandler(this.TabButton_Click);
            // 
            // btRegisterTab
            // 
            this.btRegisterTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btRegisterTab.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btRegisterTab.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btRegisterTab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btRegisterTab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btRegisterTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRegisterTab.Image = global::ProSnap.Properties.Resources.check_32x26;
            this.btRegisterTab.Location = new System.Drawing.Point(335, 0);
            this.btRegisterTab.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btRegisterTab.Name = "btRegisterTab";
            this.btRegisterTab.Size = new System.Drawing.Size(75, 65);
            this.btRegisterTab.TabIndex = 3;
            this.btRegisterTab.Text = "\r\nRegister";
            this.btRegisterTab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btRegisterTab.UseVisualStyleBackColor = false;
            this.btRegisterTab.Visible = false;
            this.btRegisterTab.Click += new System.EventHandler(this.TabButton_Click);
            // 
            // btAboutTab
            // 
            this.btAboutTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btAboutTab.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btAboutTab.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btAboutTab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btAboutTab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btAboutTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAboutTab.Image = global::ProSnap.Properties.Resources.camera_36x36;
            this.btAboutTab.Location = new System.Drawing.Point(416, 0);
            this.btAboutTab.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btAboutTab.Name = "btAboutTab";
            this.btAboutTab.Size = new System.Drawing.Size(75, 65);
            this.btAboutTab.TabIndex = 6;
            this.btAboutTab.Text = "\r\n About";
            this.btAboutTab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btAboutTab.UseVisualStyleBackColor = false;
            this.btAboutTab.Click += new System.EventHandler(this.TabButton_Click);
            // 
            // pnWindowButtons
            // 
            this.pnWindowButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnWindowButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnWindowButtons.Controls.Add(this.btCrash);
            this.pnWindowButtons.Controls.Add(this.llResetOptions);
            this.pnWindowButtons.Controls.Add(this.btClose);
            this.pnWindowButtons.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnWindowButtons.Location = new System.Drawing.Point(-1, 780);
            this.pnWindowButtons.Margin = new System.Windows.Forms.Padding(4);
            this.pnWindowButtons.Name = "pnWindowButtons";
            this.pnWindowButtons.Size = new System.Drawing.Size(647, 50);
            this.pnWindowButtons.TabIndex = 3;
            // 
            // llResetOptions
            // 
            this.llResetOptions.AutoSize = true;
            this.llResetOptions.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(69)))), ((int)(((byte)(173)))));
            this.llResetOptions.Location = new System.Drawing.Point(4, 15);
            this.llResetOptions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llResetOptions.Name = "llResetOptions";
            this.llResetOptions.Size = new System.Drawing.Size(128, 20);
            this.llResetOptions.TabIndex = 36;
            this.llResetOptions.TabStop = true;
            this.llResetOptions.Text = "Reset all options...";
            this.llResetOptions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llResetOptions_LinkClicked);
            // 
            // tcOptions
            // 
            this.tcOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcOptions.Controls.Add(this.tpPreviewWindow);
            this.tcOptions.Controls.Add(this.tpShortcuts);
            this.tcOptions.Controls.Add(this.tpButtons);
            this.tcOptions.Controls.Add(this.tpUploading);
            this.tcOptions.Controls.Add(this.tpRegistration);
            this.tcOptions.Controls.Add(this.tpAbout);
            this.tcOptions.ImageList = this.ilTabIcons;
            this.tcOptions.ItemSize = new System.Drawing.Size(100, 20);
            this.tcOptions.Location = new System.Drawing.Point(1, 65);
            this.tcOptions.Margin = new System.Windows.Forms.Padding(0);
            this.tcOptions.Name = "tcOptions";
            this.tcOptions.SelectedIndex = 0;
            this.tcOptions.Size = new System.Drawing.Size(643, 714);
            this.tcOptions.TabIndex = 1;
            this.tcOptions.SelectedIndexChanged += new System.EventHandler(this.tcOptions_SelectedIndexChanged);
            // 
            // tpPreviewWindow
            // 
            this.tpPreviewWindow.AutoScroll = true;
            this.tpPreviewWindow.Controls.Add(this.btToggleStartupTask);
            this.tpPreviewWindow.Controls.Add(this.cbDefaultFileType);
            this.tpPreviewWindow.Controls.Add(this.label8);
            this.tpPreviewWindow.Controls.Add(this.label10);
            this.tpPreviewWindow.Controls.Add(this.rbPreviewTakeFocus);
            this.tpPreviewWindow.Controls.Add(this.rbPreviewLeaveFocus);
            this.tpPreviewWindow.Controls.Add(this.label9);
            this.tpPreviewWindow.Controls.Add(this.label7);
            this.tpPreviewWindow.Controls.Add(this.nudDelayTime);
            this.tpPreviewWindow.Controls.Add(this.label5);
            this.tpPreviewWindow.Controls.Add(this.pnPreviewLocationChooser);
            this.tpPreviewWindow.Controls.Add(this.label3);
            this.tpPreviewWindow.Controls.Add(this.label4);
            this.tpPreviewWindow.Location = new System.Drawing.Point(0, 0);
            this.tpPreviewWindow.Name = "tpPreviewWindow";
            this.tpPreviewWindow.Padding = new System.Windows.Forms.Padding(3);
            this.tpPreviewWindow.Size = new System.Drawing.Size(643, 714);
            this.tpPreviewWindow.TabIndex = 1;
            this.tpPreviewWindow.Text = "General";
            this.tpPreviewWindow.UseVisualStyleBackColor = true;
            // 
            // btCrash
            // 
            this.btCrash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCrash.AutoSize = true;
            this.btCrash.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btCrash.Location = new System.Drawing.Point(433, 10);
            this.btCrash.Margin = new System.Windows.Forms.Padding(4);
            this.btCrash.Name = "btCrash";
            this.btCrash.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btCrash.Size = new System.Drawing.Size(107, 30);
            this.btCrash.TabIndex = 31;
            this.btCrash.Text = "Test Crash...";
            this.btCrash.UseVisualStyleBackColor = true;
            this.btCrash.Visible = false;
            // 
            // cbDefaultFileType
            // 
            this.cbDefaultFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDefaultFileType.FormattingEnabled = true;
            this.cbDefaultFileType.Items.AddRange(new object[] {
            "PNG File (*.png)",
            "Bitmap (*.bmp)",
            "Jpeg (*.jpg)",
            "Gif (*.gif)"});
            this.cbDefaultFileType.Location = new System.Drawing.Point(35, 569);
            this.cbDefaultFileType.Margin = new System.Windows.Forms.Padding(4);
            this.cbDefaultFileType.Name = "cbDefaultFileType";
            this.cbDefaultFileType.Size = new System.Drawing.Size(224, 28);
            this.cbDefaultFileType.TabIndex = 23;
            this.cbDefaultFileType.SelectedIndexChanged += new System.EventHandler(this.cbDefaultFileType_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(29, 545);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(376, 20);
            this.label8.TabIndex = 22;
            this.label8.Text = "(This will also set the file type for screenshot drag && drop)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 525);
            this.label10.Margin = new System.Windows.Forms.Padding(6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(174, 20);
            this.label10.TabIndex = 21;
            this.label10.Text = "Default Save-as file type:";
            // 
            // rbPreviewTakeFocus
            // 
            this.rbPreviewTakeFocus.AutoSize = true;
            this.rbPreviewTakeFocus.Location = new System.Drawing.Point(33, 491);
            this.rbPreviewTakeFocus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.rbPreviewTakeFocus.Name = "rbPreviewTakeFocus";
            this.rbPreviewTakeFocus.Size = new System.Drawing.Size(166, 24);
            this.rbPreviewTakeFocus.TabIndex = 2;
            this.rbPreviewTakeFocus.Text = "Show, and take focus";
            this.rbPreviewTakeFocus.UseVisualStyleBackColor = true;
            this.rbPreviewTakeFocus.CheckedChanged += new System.EventHandler(this.rbPreviewFocus_CheckedChanged);
            // 
            // rbPreviewLeaveFocus
            // 
            this.rbPreviewLeaveFocus.AutoSize = true;
            this.rbPreviewLeaveFocus.Checked = true;
            this.rbPreviewLeaveFocus.Location = new System.Drawing.Point(33, 468);
            this.rbPreviewLeaveFocus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.rbPreviewLeaveFocus.Name = "rbPreviewLeaveFocus";
            this.rbPreviewLeaveFocus.Size = new System.Drawing.Size(201, 24);
            this.rbPreviewLeaveFocus.TabIndex = 1;
            this.rbPreviewLeaveFocus.TabStop = true;
            this.rbPreviewLeaveFocus.Text = "Show, but don\'t take focus";
            this.rbPreviewLeaveFocus.UseVisualStyleBackColor = true;
            this.rbPreviewLeaveFocus.CheckedChanged += new System.EventHandler(this.rbPreviewFocus_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(29, 387);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(322, 20);
            this.label9.TabIndex = 20;
            this.label9.Text = "(Setting 0 will keep the window open indefinitely)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 448);
            this.label7.Margin = new System.Windows.Forms.Padding(6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(175, 20);
            this.label7.TabIndex = 17;
            this.label7.Text = "Preview activation mode:";
            // 
            // nudDelayTime
            // 
            this.nudDelayTime.Location = new System.Drawing.Point(34, 410);
            this.nudDelayTime.Margin = new System.Windows.Forms.Padding(4);
            this.nudDelayTime.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudDelayTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudDelayTime.Name = "nudDelayTime";
            this.nudDelayTime.Size = new System.Drawing.Size(61, 27);
            this.nudDelayTime.TabIndex = 0;
            this.nudDelayTime.ValueChanged += new System.EventHandler(this.nudDelayTime_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 411);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "seconds";
            // 
            // pnPreviewLocationChooser
            // 
            this.pnPreviewLocationChooser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnPreviewLocationChooser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnPreviewLocationChooser.Controls.Add(this.cbPreviewCenter);
            this.pnPreviewLocationChooser.Controls.Add(this.cbPreviewLowerRight);
            this.pnPreviewLocationChooser.Controls.Add(this.cbPreviewUpperRight);
            this.pnPreviewLocationChooser.Controls.Add(this.cbPreviewLowerLeft);
            this.pnPreviewLocationChooser.Controls.Add(this.cbPreviewUpperLeft);
            this.pnPreviewLocationChooser.Controls.Add(this.pbDesktopPreview);
            this.pnPreviewLocationChooser.Location = new System.Drawing.Point(47, 38);
            this.pnPreviewLocationChooser.Margin = new System.Windows.Forms.Padding(4);
            this.pnPreviewLocationChooser.Name = "pnPreviewLocationChooser";
            this.pnPreviewLocationChooser.Size = new System.Drawing.Size(350, 307);
            this.pnPreviewLocationChooser.TabIndex = 15;
            // 
            // cbPreviewCenter
            // 
            this.cbPreviewCenter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbPreviewCenter.AutoCheck = false;
            this.cbPreviewCenter.Location = new System.Drawing.Point(165, 145);
            this.cbPreviewCenter.Margin = new System.Windows.Forms.Padding(0);
            this.cbPreviewCenter.Name = "cbPreviewCenter";
            this.cbPreviewCenter.Size = new System.Drawing.Size(16, 16);
            this.cbPreviewCenter.TabIndex = 2;
            this.cbPreviewCenter.UseVisualStyleBackColor = true;
            this.cbPreviewCenter.CheckedChanged += new System.EventHandler(this.cbPreview_CheckedChanged);
            this.cbPreviewCenter.Click += new System.EventHandler(this.cbPreviewLocationCheckbox_Click);
            // 
            // cbPreviewLowerRight
            // 
            this.cbPreviewLowerRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPreviewLowerRight.AutoCheck = false;
            this.cbPreviewLowerRight.Checked = true;
            this.cbPreviewLowerRight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPreviewLowerRight.Location = new System.Drawing.Point(319, 276);
            this.cbPreviewLowerRight.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.cbPreviewLowerRight.Name = "cbPreviewLowerRight";
            this.cbPreviewLowerRight.Size = new System.Drawing.Size(16, 16);
            this.cbPreviewLowerRight.TabIndex = 0;
            this.cbPreviewLowerRight.UseVisualStyleBackColor = true;
            this.cbPreviewLowerRight.CheckedChanged += new System.EventHandler(this.cbPreview_CheckedChanged);
            this.cbPreviewLowerRight.Click += new System.EventHandler(this.cbPreviewLocationCheckbox_Click);
            // 
            // cbPreviewUpperRight
            // 
            this.cbPreviewUpperRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPreviewUpperRight.AutoCheck = false;
            this.cbPreviewUpperRight.Location = new System.Drawing.Point(319, 12);
            this.cbPreviewUpperRight.Margin = new System.Windows.Forms.Padding(0, 12, 12, 0);
            this.cbPreviewUpperRight.Name = "cbPreviewUpperRight";
            this.cbPreviewUpperRight.Size = new System.Drawing.Size(16, 16);
            this.cbPreviewUpperRight.TabIndex = 1;
            this.cbPreviewUpperRight.UseVisualStyleBackColor = true;
            this.cbPreviewUpperRight.CheckedChanged += new System.EventHandler(this.cbPreview_CheckedChanged);
            this.cbPreviewUpperRight.Click += new System.EventHandler(this.cbPreviewLocationCheckbox_Click);
            // 
            // cbPreviewLowerLeft
            // 
            this.cbPreviewLowerLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbPreviewLowerLeft.AutoCheck = false;
            this.cbPreviewLowerLeft.Location = new System.Drawing.Point(12, 276);
            this.cbPreviewLowerLeft.Margin = new System.Windows.Forms.Padding(12, 0, 0, 12);
            this.cbPreviewLowerLeft.Name = "cbPreviewLowerLeft";
            this.cbPreviewLowerLeft.Size = new System.Drawing.Size(16, 16);
            this.cbPreviewLowerLeft.TabIndex = 5;
            this.cbPreviewLowerLeft.UseVisualStyleBackColor = true;
            this.cbPreviewLowerLeft.CheckedChanged += new System.EventHandler(this.cbPreview_CheckedChanged);
            this.cbPreviewLowerLeft.Click += new System.EventHandler(this.cbPreviewLocationCheckbox_Click);
            // 
            // cbPreviewUpperLeft
            // 
            this.cbPreviewUpperLeft.AutoCheck = false;
            this.cbPreviewUpperLeft.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbPreviewUpperLeft.Location = new System.Drawing.Point(12, 12);
            this.cbPreviewUpperLeft.Margin = new System.Windows.Forms.Padding(12, 12, 0, 0);
            this.cbPreviewUpperLeft.Name = "cbPreviewUpperLeft";
            this.cbPreviewUpperLeft.Size = new System.Drawing.Size(16, 16);
            this.cbPreviewUpperLeft.TabIndex = 3;
            this.cbPreviewUpperLeft.UseVisualStyleBackColor = true;
            this.cbPreviewUpperLeft.CheckedChanged += new System.EventHandler(this.cbPreview_CheckedChanged);
            this.cbPreviewUpperLeft.Click += new System.EventHandler(this.cbPreviewLocationCheckbox_Click);
            // 
            // pbDesktopPreview
            // 
            this.pbDesktopPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDesktopPreview.Location = new System.Drawing.Point(0, 0);
            this.pbDesktopPreview.Margin = new System.Windows.Forms.Padding(4);
            this.pbDesktopPreview.Name = "pbDesktopPreview";
            this.pbDesktopPreview.Size = new System.Drawing.Size(348, 305);
            this.pbDesktopPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDesktopPreview.TabIndex = 15;
            this.pbDesktopPreview.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Preview pop-up location:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 367);
            this.label4.Margin = new System.Windows.Forms.Padding(6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Preview auto-close delay:";
            // 
            // tpShortcuts
            // 
            this.tpShortcuts.AutoScroll = true;
            this.tpShortcuts.Controls.Add(this.lvShortcuts);
            this.tpShortcuts.Controls.Add(this.groupBox2);
            this.tpShortcuts.Controls.Add(this.flpShortcutActions);
            this.tpShortcuts.Location = new System.Drawing.Point(0, 0);
            this.tpShortcuts.Margin = new System.Windows.Forms.Padding(4);
            this.tpShortcuts.Name = "tpShortcuts";
            this.tpShortcuts.Padding = new System.Windows.Forms.Padding(4);
            this.tpShortcuts.Size = new System.Drawing.Size(643, 714);
            this.tpShortcuts.TabIndex = 3;
            this.tpShortcuts.Text = "Shortcut Keys";
            this.tpShortcuts.UseVisualStyleBackColor = true;
            // 
            // lvShortcuts
            // 
            this.lvShortcuts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvShortcuts.CheckBoxes = true;
            this.lvShortcuts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chKeyCombo,
            this.chActions});
            this.lvShortcuts.FullRowSelect = true;
            listViewGroup3.Header = "Global Shortcuts";
            listViewGroup3.Name = "lvgGlobal";
            listViewGroup4.Header = "Preview Window Shortcuts";
            listViewGroup4.Name = "lvgPreview";
            this.lvShortcuts.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
            this.lvShortcuts.HideSelection = false;
            this.lvShortcuts.Location = new System.Drawing.Point(4, 74);
            this.lvShortcuts.Margin = new System.Windows.Forms.Padding(4);
            this.lvShortcuts.MultiSelect = false;
            this.lvShortcuts.Name = "lvShortcuts";
            this.lvShortcuts.Size = new System.Drawing.Size(635, 602);
            this.lvShortcuts.TabIndex = 0;
            this.lvShortcuts.UseCompatibleStateImageBehavior = false;
            this.lvShortcuts.View = System.Windows.Forms.View.Details;
            this.lvShortcuts.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvShortcuts_ItemChecked);
            this.lvShortcuts.SelectedIndexChanged += new System.EventHandler(this.lvShortcuts_SelectedIndexChanged);
            // 
            // chKeyCombo
            // 
            this.chKeyCombo.Text = "Key Combo";
            this.chKeyCombo.Width = 225;
            // 
            // chActions
            // 
            this.chActions.Text = "Actions";
            this.chActions.Width = 337;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(4, -5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.groupBox2.Size = new System.Drawing.Size(635, 72);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(4, 20);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(627, 52);
            this.label11.TabIndex = 25;
            this.label11.Text = "These are the keyboard shortcuts ProSnap is configured to respond to. Un-checking" +
    " a shortcut will temporarily disable it.";
            // 
            // flpShortcutActions
            // 
            this.flpShortcutActions.AutoSize = true;
            this.flpShortcutActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpShortcutActions.Controls.Add(this.btAddShortcut);
            this.flpShortcutActions.Controls.Add(this.btEditShortcut);
            this.flpShortcutActions.Controls.Add(this.btRemoveShortcut);
            this.flpShortcutActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpShortcutActions.Location = new System.Drawing.Point(4, 676);
            this.flpShortcutActions.Margin = new System.Windows.Forms.Padding(4);
            this.flpShortcutActions.Name = "flpShortcutActions";
            this.flpShortcutActions.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.flpShortcutActions.Size = new System.Drawing.Size(635, 34);
            this.flpShortcutActions.TabIndex = 24;
            // 
            // btAddShortcut
            // 
            this.btAddShortcut.AutoSize = true;
            this.btAddShortcut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btAddShortcut.Location = new System.Drawing.Point(4, 4);
            this.btAddShortcut.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.btAddShortcut.Name = "btAddShortcut";
            this.btAddShortcut.Padding = new System.Windows.Forms.Padding(18, 0, 18, 0);
            this.btAddShortcut.Size = new System.Drawing.Size(149, 30);
            this.btAddShortcut.TabIndex = 0;
            this.btAddShortcut.Text = "Add shortcut...";
            this.btAddShortcut.UseVisualStyleBackColor = true;
            this.btAddShortcut.Click += new System.EventHandler(this.btAddShortcut_Click);
            // 
            // btEditShortcut
            // 
            this.btEditShortcut.AutoSize = true;
            this.btEditShortcut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btEditShortcut.Location = new System.Drawing.Point(157, 4);
            this.btEditShortcut.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.btEditShortcut.Name = "btEditShortcut";
            this.btEditShortcut.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btEditShortcut.Size = new System.Drawing.Size(133, 30);
            this.btEditShortcut.TabIndex = 1;
            this.btEditShortcut.Text = "Edit selected...";
            this.btEditShortcut.UseVisualStyleBackColor = true;
            this.btEditShortcut.Click += new System.EventHandler(this.btEditShortcut_Click);
            // 
            // btRemoveShortcut
            // 
            this.btRemoveShortcut.AutoSize = true;
            this.btRemoveShortcut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btRemoveShortcut.Location = new System.Drawing.Point(294, 4);
            this.btRemoveShortcut.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.btRemoveShortcut.Name = "btRemoveShortcut";
            this.btRemoveShortcut.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btRemoveShortcut.Size = new System.Drawing.Size(152, 30);
            this.btRemoveShortcut.TabIndex = 2;
            this.btRemoveShortcut.Text = "Remove selected";
            this.btRemoveShortcut.UseVisualStyleBackColor = true;
            this.btRemoveShortcut.Click += new System.EventHandler(this.btRemoveShortcut_Click);
            // 
            // tpButtons
            // 
            this.tpButtons.AutoScroll = true;
            this.tpButtons.Controls.Add(this.groupBox1);
            this.tpButtons.Controls.Add(this.listView1);
            this.tpButtons.Location = new System.Drawing.Point(0, 0);
            this.tpButtons.Margin = new System.Windows.Forms.Padding(4);
            this.tpButtons.Name = "tpButtons";
            this.tpButtons.Padding = new System.Windows.Forms.Padding(4);
            this.tpButtons.Size = new System.Drawing.Size(643, 714);
            this.tpButtons.TabIndex = 0;
            this.tpButtons.Text = "Button Options";
            this.tpButtons.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.listBox2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 78);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(718, 625);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Button Properties";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(14, 126);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(212, 284);
            this.listBox1.TabIndex = 12;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(320, 126);
            this.listBox2.Margin = new System.Windows.Forms.Padding(4);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(212, 284);
            this.listBox2.TabIndex = 11;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(264, 266);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(31, 29);
            this.button3.TabIndex = 10;
            this.button3.Text = ">";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(264, 230);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(31, 29);
            this.button2.TabIndex = 9;
            this.button2.Text = "<";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 98);
            this.label2.Margin = new System.Windows.Forms.Padding(6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Click actions:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 30);
            this.label6.Margin = new System.Windows.Forms.Padding(6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "Name:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(344, 59);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "Choose icon...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 59);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(212, 27);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(340, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Icon:";
            // 
            // listView1
            // 
            this.listView1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            listViewItem5.StateImageIndex = 0;
            listViewItem8.StateImageIndex = 0;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8});
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(174, 4);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Scrollable = false;
            this.listView1.Size = new System.Drawing.Size(342, 62);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // tpUploading
            // 
            this.tpUploading.Controls.Add(this.button4);
            this.tpUploading.Controls.Add(this.lvUploadProfiles);
            this.tpUploading.Controls.Add(this.groupBox3);
            this.tpUploading.Controls.Add(this.flpUploadServiceButtons);
            this.tpUploading.Location = new System.Drawing.Point(0, 0);
            this.tpUploading.Name = "tpUploading";
            this.tpUploading.Padding = new System.Windows.Forms.Padding(4);
            this.tpUploading.Size = new System.Drawing.Size(643, 714);
            this.tpUploading.TabIndex = 4;
            this.tpUploading.Text = "Upload Services";
            this.tpUploading.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.AutoSize = true;
            this.button4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button4.Location = new System.Drawing.Point(8, 843);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.button4.Size = new System.Drawing.Size(251, 30);
            this.button4.TabIndex = 37;
            this.button4.Text = "Reset upload services to default...";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // lvUploadProfiles
            // 
            this.lvUploadProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvUploadProfiles.CheckBoxes = true;
            this.lvUploadProfiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chUploadService});
            this.lvUploadProfiles.FullRowSelect = true;
            this.lvUploadProfiles.HideSelection = false;
            this.lvUploadProfiles.LabelEdit = true;
            this.lvUploadProfiles.Location = new System.Drawing.Point(4, 74);
            this.lvUploadProfiles.Margin = new System.Windows.Forms.Padding(4);
            this.lvUploadProfiles.MultiSelect = false;
            this.lvUploadProfiles.Name = "lvUploadProfiles";
            this.lvUploadProfiles.Size = new System.Drawing.Size(635, 602);
            this.lvUploadProfiles.TabIndex = 0;
            this.lvUploadProfiles.UseCompatibleStateImageBehavior = false;
            this.lvUploadProfiles.View = System.Windows.Forms.View.Details;
            this.lvUploadProfiles.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvUploadProfiles_ItemChecked);
            this.lvUploadProfiles.SelectedIndexChanged += new System.EventHandler(this.lvUploadProfiles_SelectedIndexChanged);
            // 
            // chUploadService
            // 
            this.chUploadService.Text = "Service Name";
            this.chUploadService.Width = 393;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(4, -5);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(635, 70);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(4, 20);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(627, 46);
            this.label12.TabIndex = 25;
            this.label12.Text = "These are the upload services ProSnap can send screenshots to. The checked servic" +
    "e is the default one that will be used when the upload button is clicked.";
            // 
            // flpUploadServiceButtons
            // 
            this.flpUploadServiceButtons.AutoSize = true;
            this.flpUploadServiceButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpUploadServiceButtons.Controls.Add(this.btAddUploadService);
            this.flpUploadServiceButtons.Controls.Add(this.btEditUploadService);
            this.flpUploadServiceButtons.Controls.Add(this.btRemoveUploadService);
            this.flpUploadServiceButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpUploadServiceButtons.Location = new System.Drawing.Point(4, 676);
            this.flpUploadServiceButtons.Margin = new System.Windows.Forms.Padding(4);
            this.flpUploadServiceButtons.Name = "flpUploadServiceButtons";
            this.flpUploadServiceButtons.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.flpUploadServiceButtons.Size = new System.Drawing.Size(635, 34);
            this.flpUploadServiceButtons.TabIndex = 27;
            // 
            // btAddUploadService
            // 
            this.btAddUploadService.AutoSize = true;
            this.btAddUploadService.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btAddUploadService.Location = new System.Drawing.Point(4, 4);
            this.btAddUploadService.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.btAddUploadService.Name = "btAddUploadService";
            this.btAddUploadService.Padding = new System.Windows.Forms.Padding(18, 0, 18, 0);
            this.btAddUploadService.Size = new System.Drawing.Size(192, 30);
            this.btAddUploadService.TabIndex = 0;
            this.btAddUploadService.Text = "Add upload service...";
            this.btAddUploadService.UseVisualStyleBackColor = true;
            this.btAddUploadService.Click += new System.EventHandler(this.btAddUploadService_Click);
            // 
            // btEditUploadService
            // 
            this.btEditUploadService.AutoSize = true;
            this.btEditUploadService.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btEditUploadService.Location = new System.Drawing.Point(200, 4);
            this.btEditUploadService.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.btEditUploadService.Name = "btEditUploadService";
            this.btEditUploadService.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btEditUploadService.Size = new System.Drawing.Size(133, 30);
            this.btEditUploadService.TabIndex = 1;
            this.btEditUploadService.Text = "Edit selected...";
            this.btEditUploadService.UseVisualStyleBackColor = true;
            this.btEditUploadService.Click += new System.EventHandler(this.btEditUploadService_Click);
            // 
            // btRemoveUploadService
            // 
            this.btRemoveUploadService.AutoSize = true;
            this.btRemoveUploadService.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btRemoveUploadService.Location = new System.Drawing.Point(337, 4);
            this.btRemoveUploadService.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.btRemoveUploadService.Name = "btRemoveUploadService";
            this.btRemoveUploadService.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btRemoveUploadService.Size = new System.Drawing.Size(152, 30);
            this.btRemoveUploadService.TabIndex = 2;
            this.btRemoveUploadService.Text = "Remove selected";
            this.btRemoveUploadService.UseVisualStyleBackColor = true;
            this.btRemoveUploadService.Click += new System.EventHandler(this.btRemoveUploadService_Click);
            // 
            // tpRegistration
            // 
            this.tpRegistration.AutoScroll = true;
            this.tpRegistration.Controls.Add(this.label17);
            this.tpRegistration.Controls.Add(this.tbRegistrationKey);
            this.tpRegistration.Controls.Add(this.llRegistrationKey);
            this.tpRegistration.Controls.Add(this.label16);
            this.tpRegistration.Controls.Add(this.label15);
            this.tpRegistration.Controls.Add(this.label14);
            this.tpRegistration.Location = new System.Drawing.Point(0, 0);
            this.tpRegistration.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegistration.Name = "tpRegistration";
            this.tpRegistration.Size = new System.Drawing.Size(643, 714);
            this.tpRegistration.TabIndex = 2;
            this.tpRegistration.Text = "Registration";
            this.tpRegistration.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(30, 69);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(334, 20);
            this.label17.TabIndex = 6;
            this.label17.Text = "● Feel good about supporting app development!";
            // 
            // tbRegistrationKey
            // 
            this.tbRegistrationKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRegistrationKey.Location = new System.Drawing.Point(34, 149);
            this.tbRegistrationKey.Margin = new System.Windows.Forms.Padding(4);
            this.tbRegistrationKey.Multiline = true;
            this.tbRegistrationKey.Name = "tbRegistrationKey";
            this.tbRegistrationKey.Size = new System.Drawing.Size(525, 165);
            this.tbRegistrationKey.TabIndex = 0;
            // 
            // llRegistrationKey
            // 
            this.llRegistrationKey.AutoSize = true;
            this.llRegistrationKey.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(69)))), ((int)(((byte)(173)))));
            this.llRegistrationKey.Location = new System.Drawing.Point(30, 126);
            this.llRegistrationKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llRegistrationKey.Name = "llRegistrationKey";
            this.llRegistrationKey.Size = new System.Drawing.Size(360, 20);
            this.llRegistrationKey.TabIndex = 4;
            this.llRegistrationKey.TabStop = true;
            this.llRegistrationKey.Text = "Go here to get a registration key, then paste it below:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(30, 50);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(472, 20);
            this.label16.TabIndex = 3;
            this.label16.Text = "● Upload images to OAuth-enabled services (such as Imgur accounts)!";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(30, 88);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(318, 20);
            this.label15.TabIndex = 2;
            this.label15.Text = "● Register however many computers you want!";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(10, 4);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(466, 32);
            this.label14.TabIndex = 1;
            this.label14.Text = "Buy a registration key, get bonus features!";
            // 
            // tpAbout
            // 
            this.tpAbout.Controls.Add(this.gbLicenseDeclarations);
            this.tpAbout.Controls.Add(this.label13);
            this.tpAbout.Controls.Add(this.linkLabel2);
            this.tpAbout.Controls.Add(this.pictureBox1);
            this.tpAbout.Controls.Add(this.label19);
            this.tpAbout.Controls.Add(this.groupBox5);
            this.tpAbout.Location = new System.Drawing.Point(0, 0);
            this.tpAbout.Name = "tpAbout";
            this.tpAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tpAbout.Size = new System.Drawing.Size(643, 714);
            this.tpAbout.TabIndex = 6;
            this.tpAbout.Text = "tabPage1";
            this.tpAbout.UseVisualStyleBackColor = true;
            // 
            // gbLicenseDeclarations
            // 
            this.gbLicenseDeclarations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLicenseDeclarations.Controls.Add(this.tbLicenseDeclarations);
            this.gbLicenseDeclarations.Location = new System.Drawing.Point(11, 282);
            this.gbLicenseDeclarations.Name = "gbLicenseDeclarations";
            this.gbLicenseDeclarations.Size = new System.Drawing.Size(621, 426);
            this.gbLicenseDeclarations.TabIndex = 38;
            this.gbLicenseDeclarations.TabStop = false;
            this.gbLicenseDeclarations.Text = "Third Party License Declarations";
            // 
            // tbLicenseDeclarations
            // 
            this.tbLicenseDeclarations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLicenseDeclarations.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbLicenseDeclarations.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLicenseDeclarations.Location = new System.Drawing.Point(10, 26);
            this.tbLicenseDeclarations.Multiline = true;
            this.tbLicenseDeclarations.Name = "tbLicenseDeclarations";
            this.tbLicenseDeclarations.ReadOnly = true;
            this.tbLicenseDeclarations.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLicenseDeclarations.Size = new System.Drawing.Size(605, 394);
            this.tbLicenseDeclarations.TabIndex = 37;
            this.tbLicenseDeclarations.WordWrap = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 80);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(310, 20);
            this.label13.TabIndex = 36;
            this.label13.Text = "Have feedback, a comment, or a suggestion? ";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(69)))), ((int)(((byte)(173)))));
            this.linkLabel2.Location = new System.Drawing.Point(14, 100);
            this.linkLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(205, 20);
            this.linkLabel2.TabIndex = 35;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Visit the application website...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ProSnap.Properties.Resources.camera_36x36;
            this.pictureBox1.Location = new System.Drawing.Point(18, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(36, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 30F);
            this.label19.Location = new System.Drawing.Point(60, 3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(212, 67);
            this.label19.TabIndex = 33;
            this.label19.Text = "ProSnap";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.lbVersion);
            this.groupBox5.Controls.Add(this.btManualUpdate);
            this.groupBox5.Location = new System.Drawing.Point(11, 148);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(621, 91);
            this.groupBox5.TabIndex = 32;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Current Version";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(6, 23);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(48, 20);
            this.lbVersion.TabIndex = 0;
            this.lbVersion.Text = "v1234";
            // 
            // btManualUpdate
            // 
            this.btManualUpdate.AutoSize = true;
            this.btManualUpdate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btManualUpdate.Location = new System.Drawing.Point(7, 47);
            this.btManualUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btManualUpdate.Name = "btManualUpdate";
            this.btManualUpdate.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btManualUpdate.Size = new System.Drawing.Size(153, 30);
            this.btManualUpdate.TabIndex = 31;
            this.btManualUpdate.Text = "Check for update...";
            this.btManualUpdate.UseVisualStyleBackColor = true;
            this.btManualUpdate.Click += new System.EventHandler(this.btManualUpdate_Click);
            // 
            // btToggleStartupTask
            // 
            this.btToggleStartupTask.Image = global::ProSnap.Properties.Resources.x_28x28_red;
            this.btToggleStartupTask.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btToggleStartupTask.Location = new System.Drawing.Point(13, 615);
            this.btToggleStartupTask.Name = "btToggleStartupTask";
            this.btToggleStartupTask.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btToggleStartupTask.Size = new System.Drawing.Size(413, 60);
            this.btToggleStartupTask.TabIndex = 30;
            this.btToggleStartupTask.Text = "Set ProSnap to run automatically at startup\r\nCurrent Status: Not Set";
            this.btToggleStartupTask.UseVisualStyleBackColor = true;
            this.btToggleStartupTask.Click += new System.EventHandler(this.btToggleStartupTask_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btClose;
            this.ClientSize = new System.Drawing.Size(645, 828);
            this.Controls.Add(this.pnWindowButtons);
            this.Controls.Add(this.tcOptions);
            this.Controls.Add(this.flpTabContainer);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProSnap Options";
            this.flpTabContainer.ResumeLayout(false);
            this.pnWindowButtons.ResumeLayout(false);
            this.pnWindowButtons.PerformLayout();
            this.tcOptions.ResumeLayout(false);
            this.tpPreviewWindow.ResumeLayout(false);
            this.tpPreviewWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayTime)).EndInit();
            this.pnPreviewLocationChooser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDesktopPreview)).EndInit();
            this.tpShortcuts.ResumeLayout(false);
            this.tpShortcuts.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.flpShortcutActions.ResumeLayout(false);
            this.flpShortcutActions.PerformLayout();
            this.tpButtons.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpUploading.ResumeLayout(false);
            this.tpUploading.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.flpUploadServiceButtons.ResumeLayout(false);
            this.flpUploadServiceButtons.PerformLayout();
            this.tpRegistration.ResumeLayout(false);
            this.tpRegistration.PerformLayout();
            this.tpAbout.ResumeLayout(false);
            this.tpAbout.PerformLayout();
            this.gbLicenseDeclarations.ResumeLayout(false);
            this.gbLicenseDeclarations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private TablessControl tcOptions;
        private System.Windows.Forms.TabPage tpPreviewWindow;
        private System.Windows.Forms.TabPage tpRegistration;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudDelayTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tpButtons;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel pnPreviewLocationChooser;
        private System.Windows.Forms.CheckBox cbPreviewCenter;
        private System.Windows.Forms.CheckBox cbPreviewLowerRight;
        private System.Windows.Forms.CheckBox cbPreviewUpperRight;
        private System.Windows.Forms.CheckBox cbPreviewLowerLeft;
        private System.Windows.Forms.CheckBox cbPreviewUpperLeft;
        private System.Windows.Forms.PictureBox pbDesktopPreview;
        private System.Windows.Forms.TabPage tpShortcuts;
        private System.Windows.Forms.Button btRemoveShortcut;
        private System.Windows.Forms.Button btAddShortcut;
        private System.Windows.Forms.ColumnHeader chKeyCombo;
        private System.Windows.Forms.ColumnHeader chActions;
        private System.Windows.Forms.FlowLayoutPanel flpShortcutActions;
        private System.Windows.Forms.Button btEditShortcut;
        private ProSnap.ListView listView1;
        private ProSnap.ListView lvShortcuts;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpUploading;
        private System.Windows.Forms.FlowLayoutPanel flpUploadServiceButtons;
        private System.Windows.Forms.Button btAddUploadService;
        private System.Windows.Forms.Button btRemoveUploadService;
        private ProSnap.ListView lvUploadProfiles;
        private System.Windows.Forms.ColumnHeader chUploadService;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.LinkLabel llRegistrationKey;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbRegistrationKey;
        private System.Windows.Forms.RadioButton rbPreviewTakeFocus;
        private System.Windows.Forms.RadioButton rbPreviewLeaveFocus;
        private System.Windows.Forms.ImageList ilTabIcons;
        private System.Windows.Forms.FlowLayoutPanel flpTabContainer;
        private System.Windows.Forms.Button btGeneralTab;
        private System.Windows.Forms.Button btShortcutsTab;
        private System.Windows.Forms.Button btUploadingTab;
        private System.Windows.Forms.Button btRegisterTab;
        private System.Windows.Forms.Button btEditUploadService;
        private System.Windows.Forms.Panel pnWindowButtons;
        private System.Windows.Forms.ComboBox cbDefaultFileType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.LinkLabel llResetOptions;
        private System.Windows.Forms.Button btCrash;
        private System.Windows.Forms.Button btPreviewTab;
        private System.Windows.Forms.Button btAboutTab;
        private System.Windows.Forms.TabPage tpAbout;
        private System.Windows.Forms.Button btManualUpdate;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox gbLicenseDeclarations;
        private System.Windows.Forms.TextBox tbLicenseDeclarations;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btToggleStartupTask;
    }
}