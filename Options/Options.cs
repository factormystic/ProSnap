using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FMUtils.Screenshot;
using FMUtils.WinApi;
using Microsoft.Win32;
using ProSnap.ActionItems;
using ProSnap.Uploading;

namespace ProSnap.Options
{
    public partial class Options : Form
    {
        bool IgnoreChanges = false;

        List<Button> TabButtons;
        Button SelectedTabButton;

        List<TabPage> TabPages;

        public Options()
        {
            Trace.WriteLine("Creating options window...", string.Format("Options.ctor [{0}]", System.Threading.Thread.CurrentThread.Name));

            InitializeComponent();

            Icon = ProSnap.Properties.Resources.camera_36x36_icon;

            TabButtons = new List<Button>()
            {
                btGeneralTab,
                btShortcutsTab,
                btPreviewTab,
                btUploadingTab,
                btRegisterTab,
                btAboutTab,
            };

            btGeneralTab.Tag = "general";
            btShortcutsTab.Tag = "shortcuts";
            btUploadingTab.Tag = "uploading";
            btRegisterTab.Tag = "registration";
            btAboutTab.Tag = "about";

            TabPages = new List<TabPage>()
            {
                tpPreviewWindow,
                tpShortcuts,
                tpButtons,
                tpUploading,
                tpRegistration,
                tpAbout,
            };

            tpPreviewWindow.Tag = btGeneralTab;
            tpShortcuts.Tag = btShortcutsTab;
            tpButtons.Tag = null;
            tpUploading.Tag = btUploadingTab;
            tpRegistration.Tag = btRegisterTab;
            tpAbout.Tag = btAboutTab;

            //remove work in progress tabs
            tcOptions.TabPages.Remove(tpButtons);

            SelectTabButton(btGeneralTab);

            LoadDesktopThumb();
            LoadFromConfiguration();

            //btManualUpdate.Visible = btCrash.Visible = Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "prosnap-debug"));
            lbVersion.Text = Application.ProductVersion;
            tbLicenseDeclarations.Text = ProSnap.Properties.Resources.licences;

            btToggleStartupTask.Enabled = false;
            btToggleStartupTask.Text = "Set ProSnap to run automatically at startup\nCurrent Status: Checking...";
            Configuration.GetStartupTaskStatus().ContinueWith(t =>
            {
                Groom_btStartupTask(t.Result);
                btToggleStartupTask.Enabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());

            Trace.WriteLine("Done.", string.Format("Options.ctor [{0}]", System.Threading.Thread.CurrentThread.Name));
        }

        private void LoadFromConfiguration()
        {
            IgnoreChanges = true;

            nudDelayTime.Value = Configuration.PreviewDelayTime;
            cbDefaultFileType.SelectedIndex = Configuration.DefaultFilterIndex;

            cbPreviewCenter.Checked = Configuration.PreviewLocation == FMUtils.WinApi.Helper.WindowLocation.Center;
            cbPreviewUpperLeft.Checked = Configuration.PreviewLocation == FMUtils.WinApi.Helper.WindowLocation.UpperLeft;
            cbPreviewUpperRight.Checked = Configuration.PreviewLocation == FMUtils.WinApi.Helper.WindowLocation.UpperRight;
            cbPreviewLowerLeft.Checked = Configuration.PreviewLocation == FMUtils.WinApi.Helper.WindowLocation.LowerLeft;
            cbPreviewLowerRight.Checked = Configuration.PreviewLocation == FMUtils.WinApi.Helper.WindowLocation.LowerRight;

            lvShortcuts.Items.Clear();
            lvShortcuts.Items.AddRange(Configuration.Shortcuts.Select(s => new ListViewItem(new string[] { s.KeyCombo.ToString(), string.Join(", ", s.ActionChain.ActionItems.Select(ai => ai.ActionType.DisplayText())) })
                {
                    Checked = s.Enabled,
                    Tag = s,
                    Group = s.RequirePreviewOpen ? lvShortcuts.Groups["lvgPreview"] : lvShortcuts.Groups["lvgGlobal"]
                }).ToArray());
            if (lvShortcuts.Items.Count > 0)
                lvShortcuts.SelectedIndices.Add(0);

            lvUploadProfiles.Items.Clear();
            lvUploadProfiles.Items.AddRange(Configuration.UploadServices.Select(up => new ListViewItem(new string[] { up.Name, up.EndpointUrl }) { Checked = up.isActive, Selected = up.isActive, Tag = up, }).ToArray());
            if (lvUploadProfiles.Items.Count > 0)
                lvUploadProfiles.SelectedIndices.Add(0);

            rbPreviewLeaveFocus.Checked = Configuration.ShowPreviewWithoutActivation;
            rbPreviewTakeFocus.Checked = !rbPreviewLeaveFocus.Checked;

            IgnoreChanges = false;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Closing options window...", string.Format("Options.btClose_Click [{0}]", System.Threading.Thread.CurrentThread.Name));

            this.Close();
        }

        public static void ShowOrActivate(string tab = "general")
        {
            var open = Application.OpenForms.Cast<Form>().Where(f => f is Options).FirstOrDefault() as Options;
            if (open != null)
            {
                open.BeginInvoke(new MethodInvoker(() =>
                    {
                        open.Activate();

                        var TabButton = open.TabButtons.FirstOrDefault(bt => string.Equals(bt.Tag as string, tab, StringComparison.InvariantCultureIgnoreCase));
                        if (TabButton != null)
                            TabButton.PerformClick();
                    }));
            }
            else
            {
                open = new Options();
                open.Show();

                var TabButton = open.TabButtons.FirstOrDefault(bt => string.Equals(bt.Tag as string, tab, StringComparison.InvariantCultureIgnoreCase));
                if (TabButton != null)
                    TabButton.PerformClick();
            }
        }

        private void llResetOptions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show(this, "Clear current options and restore defaults?", "Reset Options?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Trace.WriteLine("Resetting options to default...", string.Format("Options.btResetOptions_Click [{0}]", System.Threading.Thread.CurrentThread.Name));

                Configuration.LoadDefaults();
                Configuration.Write();

                LoadFromConfiguration();
            }
        }

        #region General
        private void LoadDesktopThumb()
        {
            Screenshot TaskBar = new Screenshot(Windowing.FindWindow("Shell_TrayWnd", null), ScreenshotMethod.DWM, false);
            string DesktopWallpaperPath = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop").GetValue("Wallpaper") as string;

            Bitmap CompositedDesktop = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics composite = Graphics.FromImage(CompositedDesktop))
            {
                if (string.IsNullOrWhiteSpace(DesktopWallpaperPath))
                {
                    string[] BGColor = (Registry.CurrentUser.OpenSubKey(@"Control Panel\Colors").GetValue("Background", string.Empty) as string).Split(' ');
                    if (BGColor.Length >= 3)
                    {
                        int r, g, b;
                        if (int.TryParse(BGColor[0], out r) && int.TryParse(BGColor[1], out g) && int.TryParse(BGColor[2], out b))
                            composite.FillRectangle(new SolidBrush(Color.FromArgb(r, g, b)), 0, 0, CompositedDesktop.Width, CompositedDesktop.Height);
                    }
                }
                else
                {
                    Image DesktopWallpaper = Image.FromFile(DesktopWallpaperPath);
                    composite.DrawImage(DesktopWallpaper, new Rectangle(0, 0, CompositedDesktop.Width, CompositedDesktop.Height), new Rectangle(0, 0, DesktopWallpaper.Width, DesktopWallpaper.Height), GraphicsUnit.Pixel);
                }

                switch (FMUtils.WinApi.Helper.GetTaskbarEdge())
                {
                    case DockStyle.Top:
                    case DockStyle.Left: composite.DrawImageUnscaled(TaskBar.BaseScreenshotImage, 0, 0); break;
                    case DockStyle.Bottom: composite.DrawImageUnscaled(TaskBar.BaseScreenshotImage, 0, Screen.PrimaryScreen.Bounds.Height - TaskBar.BaseScreenshotImage.Height); break;
                    case DockStyle.Right: composite.DrawImageUnscaled(TaskBar.BaseScreenshotImage, Screen.PrimaryScreen.Bounds.Width - TaskBar.BaseScreenshotImage.Width, 0); break;
                }

                composite.Save();
            }

            pbDesktopPreview.Image = CompositedDesktop;
            pnPreviewLocationChooser.Width = (int)((float)pbDesktopPreview.Height * ((float)CompositedDesktop.Width / (float)CompositedDesktop.Height)) + 1;
            pnPreviewLocationChooser.Left = (int)((float)this.Width / 2.0 - (float)pnPreviewLocationChooser.Width / 2.0);
        }

        private FMUtils.WinApi.Helper.WindowLocation ChecksToEdge()
        {
            if (cbPreviewUpperLeft.Checked)
                return FMUtils.WinApi.Helper.WindowLocation.UpperLeft;

            if (cbPreviewUpperRight.Checked)
                return FMUtils.WinApi.Helper.WindowLocation.UpperRight;

            if (cbPreviewLowerLeft.Checked)
                return FMUtils.WinApi.Helper.WindowLocation.LowerLeft;

            if (cbPreviewLowerRight.Checked)
                return FMUtils.WinApi.Helper.WindowLocation.LowerRight;

            return FMUtils.WinApi.Helper.WindowLocation.Center;
        }

        private void cbPreviewLocationCheckbox_Click(object sender, EventArgs e)
        {
            if (IgnoreChanges)
                return;

            CheckBox cbClicked = (sender as CheckBox);
            if (!cbClicked.Checked)
            {
                cbPreviewCenter.Checked = false;
                cbPreviewUpperRight.Checked = false;
                cbPreviewUpperLeft.Checked = false;
                cbPreviewLowerRight.Checked = false;
                cbPreviewLowerLeft.Checked = false;

                cbClicked.Checked = true;
            }
        }

        private void cbPreview_CheckedChanged(object sender, EventArgs e)
        {
            if (IgnoreChanges)
                return;

            Configuration.PreviewLocation = ChecksToEdge();
            Configuration.ButtonPanelLocation = Configuration.PreviewLocation == FMUtils.WinApi.Helper.WindowLocation.UpperLeft || Configuration.PreviewLocation == FMUtils.WinApi.Helper.WindowLocation.UpperRight ? DockStyle.Top : DockStyle.Bottom;
        }

        private void nudDelayTime_ValueChanged(object sender, EventArgs e)
        {
            if (IgnoreChanges)
                return;

            Configuration.PreviewDelayTime = (int)nudDelayTime.Value;
        }

        private void rbPreviewFocus_CheckedChanged(object sender, EventArgs e)
        {
            if (IgnoreChanges)
                return;

            Configuration.ShowPreviewWithoutActivation = rbPreviewLeaveFocus.Checked;
        }

        private void cbDefaultFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IgnoreChanges)
                return;

            Configuration.DefaultFilterIndex = cbDefaultFileType.SelectedIndex;
        }

        private void btToggleStartupTask_Click(object sender, EventArgs e)
        {
            btToggleStartupTask.Enabled = false;

            Configuration.ToggleStartupTask().ContinueWith(t =>
            {
                btToggleStartupTask.Enabled = true;

                if (t.IsFaulted)
                {
                    MessageBox.Show(this, t.Exception.GetBaseException().Message, "ProSnap Scheduled Task Installation", MessageBoxButtons.OK);
                    return;
                }

                Groom_btStartupTask(t.Result);

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void Groom_btStartupTask(bool installed)
        {
            if (installed)
            {
                btToggleStartupTask.Text = "Set ProSnap to run automatically at startup\nCurrent Status: Active";
                btToggleStartupTask.Image = ProSnap.Properties.Resources.check_green_32x26;
            }
            else
            {
                btToggleStartupTask.Text = "Set ProSnap to run automatically at startup\nCurrent Status: Not set";
                btToggleStartupTask.Image = ProSnap.Properties.Resources.x_28x28_red;
            }
        }
        #endregion

        #region Shortcuts
        private void btAddShortcut_Click(object sender, EventArgs e)
        {
            var sc = new ShortcutItemConfiguration();
            if (sc.ShowDialog(this) == DialogResult.OK)
            {
                var s = sc.GetShortcut();
                lvShortcuts.Items.Add(new ListViewItem(new string[] { s.KeyCombo.ToString(), string.Join(", ", s.ActionChain.ActionItems.Select(ai => ai.ActionType.DisplayText())) })
                {
                    Checked = s.Enabled,
                    Tag = s,
                    Group = s.RequirePreviewOpen ? lvShortcuts.Groups["lvgPreview"] : lvShortcuts.Groups["lvgGlobal"]
                });
                Configuration.Shortcuts.Add(s);

                Configuration.Write();
            }
        }

        private void btEditShortcut_Click(object sender, EventArgs e)
        {
            if (lvShortcuts.SelectedIndices.Count > 0)
            {
                var shortcut = Configuration.Shortcuts[lvShortcuts.SelectedIndices[0]];
                var sc = new ShortcutItemConfiguration(shortcut);

                if (sc.ShowDialog(this) == DialogResult.OK)
                {
                    var s = sc.GetShortcut();
                    Configuration.Shortcuts[lvShortcuts.SelectedIndices[0]] = s;

                    lvShortcuts.SelectedItems[0].Group = s.RequirePreviewOpen ? lvShortcuts.Groups["lvgPreview"] : lvShortcuts.Groups["lvgGlobal"];
                    lvShortcuts.SelectedItems[0].Tag = s;
                    lvShortcuts.SelectedItems[0].SubItems[0].Text = s.KeyCombo.ToString();
                    lvShortcuts.SelectedItems[0].SubItems[1].Text = string.Join(", ", s.ActionChain.ActionItems.Select(ai => ai.ActionType.DisplayText()));

                    Configuration.Write();
                }
            }
        }

        private void btRemoveShortcut_Click(object sender, EventArgs e)
        {
            if (lvShortcuts.SelectedIndices.Count > 0)
            {
                Configuration.Shortcuts.Remove(lvShortcuts.SelectedItems[0].Tag as ShortcutItem);
                lvShortcuts.Items.Remove(lvShortcuts.SelectedItems[0]);

                Configuration.Write();
            }
        }

        private void lvShortcuts_SelectedIndexChanged(object sender, EventArgs e)
        {
            btEditShortcut.Enabled = btRemoveShortcut.Enabled = lvShortcuts.SelectedItems.Count > 0;
        }

        private void lvShortcuts_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (IgnoreChanges)
                return;

            (e.Item.Tag as ShortcutItem).Enabled = e.Item.Checked;
        }
        #endregion

        #region Upload
        private void lvUploadProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            btEditUploadService.Enabled = btRemoveUploadService.Enabled = lvUploadProfiles.SelectedItems.Count > 0;

            if (lvUploadProfiles.SelectedItems.Count > 0)
            {

                //tbUploadServiceName.Text = lvUploadProfiles.SelectedItems[0].SubItems[0].Text;
                //tbUploadServiceUrl.Text = lvUploadProfiles.SelectedItems[0].SubItems[1].Text;

                //lvKeyValues.Items.Clear();

                //var CurrentUploadValues = (lvUploadProfiles.SelectedItems[0].Tag as UploadService).UploadValues;
                //foreach (string k in CurrentUploadValues)
                //{
                //    lvKeyValues.Items.Add(new ListViewItem(new string[] { k, (string)CurrentUploadValues[k] }));
                //}

                //btRemoveUploadService.Enabled = true;
                //gbPostValues.Enabled = true;
            }
            else
            {
                //btRemoveUploadService.Enabled = false;
                //tbUploadServiceName.Clear();
                //tbUploadServiceUrl.Clear();

                //lvKeyValues.Items.Clear();
                //tbKey.Clear();
                //tbValue.Clear();

                //gbPostValues.Enabled = false;
            }
        }

        private void lvUploadProfiles_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (IgnoreChanges)
                return;

            var CurrentUploadService = e.Item.Tag as IUploadService;
            if (e.Item.Checked)
            {
                CurrentUploadService.isActive = true;

                foreach (ListViewItem lviUploadService in lvUploadProfiles.CheckedItems)
                    lviUploadService.Checked = lviUploadService == e.Item;
            }
            else
            {
                CurrentUploadService.isActive = false;
            }
        }

        private void btAddUploadService_Click(object sender, EventArgs e)
        {
            var usp = new UploadServiceProperties(MultipartFormDataUploadService.Create());
            if (usp.ShowDialog() == DialogResult.OK)
            {
                var NewUploadService = usp.GetResultUploadService();
                lvUploadProfiles.Items.Add(new ListViewItem(new string[] { NewUploadService.Name, NewUploadService.EndpointUrl }) { Tag = NewUploadService });
                lvUploadProfiles.SelectedIndices.Add(lvUploadProfiles.Items.Count - 1);
            }
        }

        private void btEditUploadService_Click(object sender, EventArgs e)
        {
            if (lvUploadProfiles.SelectedItems.Count > 0)
            {
                var up = new UploadServiceProperties(lvUploadProfiles.SelectedItems[0].Tag as IUploadService);
                if (up.ShowDialog(this) == DialogResult.OK)
                {
                    var UpdatedUploadService = up.GetResultUploadService();
                    lvUploadProfiles.SelectedItems[0].Tag = UpdatedUploadService;
                    lvUploadProfiles.SelectedItems[0].SubItems[0].Text = UpdatedUploadService.Name;
                }
            }
        }

        private void btRemoveUploadService_Click(object sender, EventArgs e)
        {
            if (lvUploadProfiles.SelectedItems.Count > 0)
            {
                var RemovingUploadService = lvUploadProfiles.SelectedItems[0].Tag as IUploadService;
                Configuration.UploadServices.Remove(RemovingUploadService);
                lvUploadProfiles.Items.Remove(lvUploadProfiles.SelectedItems[0]);
            }
        }
        #endregion

        #region Registration
        #endregion

        #region Diagnostics
        private void btManualUpdate_Click(object sender, EventArgs e)
        {
            if (Configuration.UpdateRestartRequired)
            {
                Application.Restart();
                return;
            }

            btManualUpdate.Enabled = false;
            btManualUpdate.Text = "Checking for update...";

            ProSnap.Update.CheckForUpdate(() =>
            {
                if (Configuration.UpdateRestartRequired)
                {
                    btManualUpdate.Enabled = true;
                    btManualUpdate.Text = "Relaunch for update";
                }
                else
                {
                    btManualUpdate.Text = "No update available";
                }
            });
        }

        private void btCrash_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Throwing test exception...", string.Format("Options.btCrash_Click [{0}]", System.Threading.Thread.CurrentThread.Name));
            throw new Exception("Nothing went wrong, this is a test.");
        }

        private void llOpenLogFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Trace.WriteLine("Opening application folder...", string.Format("Options.btOpenLogFolder_Click [{0}]", System.Threading.Thread.CurrentThread.Name));
            Process.Start(Configuration.LocalPath);
        }
        #endregion

        #region "Tabs"
        private void SelectTabButton(Button bt)
        {
            bt.FlatAppearance.BorderColor = SystemColors.Highlight;
            bt.BackColor = Color.FromArgb(185, 209, 234);

            SelectedTabButton = bt;
        }

        private void DeselectTabButton(Button bt)
        {
            bt.FlatAppearance.BorderColor = SystemColors.ControlLightLight;
            bt.BackColor = flpTabContainer.BackColor;
        }

        private void SwitchToTabButton(Button bt)
        {
            SelectTabButton(bt);
            TabButtons.Except(new[] { bt }).Realize(DeselectTabButton);

            //switch the button first, so SelectedTabButton gets set and tcOptions_SelectedIndexChanged doesn't trigger twice
            tcOptions.SelectedTab = TabPages.FirstOrDefault(tp => tp.Tag == bt) ?? tpPreviewWindow;
        }

        private void TabButton_Click(object sender, EventArgs e)
        {
            Trace.WriteLine(string.Format("Switching to {0} tab...", (sender as Button).Tag), string.Format("Options.TabButton_Click [{0}]", System.Threading.Thread.CurrentThread.Name));
            SwitchToTabButton(sender as Button);
        }

        private void tcOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //for ctrl+arrow navigation
            var TabButton = tcOptions.SelectedTab.Tag as Button;
            if (TabButton != null && TabButton != SelectedTabButton)
                TabButton.PerformClick();
        }
        #endregion
    }
}
