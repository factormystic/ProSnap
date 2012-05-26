using System;
using System.Linq;
using System.Windows.Forms;
using ProSnap.ActionItems;
using FMUtils.KeyboardHook;

namespace ProSnap.Options
{
    public partial class ShortcutItemConfiguration : Form
    {
        ShortcutItem Shortcut = ShortcutItem.Empty();
        KeyCombo CurrentKeyCombo;
        Hook ShortcutHook;

        public ShortcutItemConfiguration()
        {
            InitializeComponent();

            this.MinimumSize = this.Size;

            this.ShortcutHook = new Hook("Shortcut Capture Hook");
            this.ShortcutHook.KeyDownEvent += e =>
                {
                    if (tbShortcut.Focused)
                    {
                        CurrentKeyCombo = KeyCombo.FromKeyboardHookEventArgs(e);
                        UpdateShortcutDisplay();
                    }
                };

            lvActions.Select();
        }

        public ShortcutItemConfiguration(ShortcutItem shortcut)
            : this()
        {
            this.Shortcut = shortcut;

            cbRequirePreview.Checked = shortcut.RequirePreviewOpen;

            UpdateShortcutDisplay();
            UpdateActionChain();
        }

        private void UpdateActionChain()
        {
            lvActions.Items.Clear();
            lvActions.Items.AddRange(Shortcut.ActionChain.ActionItems.Select((ai, i) => new ListViewItem(new string[] { (i + 1).ToString(), ai.ActionType.DisplayText() }) { Tag = ai }).ToArray());

            if (lvActions.Items.Count > 0)
            {
                lvActions.Items[0].Selected = true;
            }
        }

        private void UpdateShortcutDisplay()
        {
            if (tbShortcut.InvokeRequired)
                this.BeginInvoke(new MethodInvoker(() => UpdateShortcutDisplay()));

            tbShortcut.Text = (CurrentKeyCombo ?? Shortcut.KeyCombo).ToString();
        }

        public ShortcutItem GetShortcut()
        {
            return new ShortcutItem(true, cbRequirePreview.Checked, CurrentKeyCombo ?? Shortcut.KeyCombo, GetActionChain());
        }

        private ProgramActionChain GetActionChain()
        {
            var pac = new ProgramActionChain(string.Empty);
            pac.ActionItems.AddRange(lvActions.Items.Cast<ListViewItem>().Select(lvi => lvi.Tag as IActionItem));

            return pac;
        }

        private void tbShortcut_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void tbShortcut_Enter(object sender, EventArgs e)
        {
            Configuration.IgnoreAllKeyHooks = true;
            ShortcutHook.isPaused = false;
            label1.Enabled = true;
        }

        private void tbShortcut_Leave(object sender, EventArgs e)
        {
            Configuration.IgnoreAllKeyHooks = false;
            ShortcutHook.isPaused = true;
            label1.Enabled = false;
        }

        private void btAddAction_Click(object sender, EventArgs e)
        {
            var sap = new ShortcutActionProperties(ActionTypes.None.ToInstance());
            if (sap.ShowDialog(this) == DialogResult.OK)
            {
                lvActions.Items.Add(new ListViewItem(new string[] { (lvActions.Items.Count + 1).ToString(), sap.ResultActionItem.ActionType.DisplayText() }) { Tag = sap.ResultActionItem });
                lvActions.Items[lvActions.Items.Count - 1].Selected = true;
            }
        }

        private void btRemoveAction_Click(object sender, EventArgs e)
        {
            if (lvActions.SelectedItems.Count > 0)
            {
                var oldIndex = lvActions.SelectedItems[0].Index;
                lvActions.Items.Remove(lvActions.SelectedItems[0]);

                if (lvActions.Items.Count > oldIndex)
                {
                    lvActions.Items[oldIndex].Selected = true;
                }
                else if (lvActions.Items.Count > 0)
                {
                    lvActions.Items[oldIndex - 1].Selected = true;
                }

                GroomActionIndicies();
            }
        }

        private void btMoveActionUp_Click(object sender, EventArgs e)
        {
            if (lvActions.SelectedItems.Count > 0)
            {
                var selected = lvActions.SelectedItems[0];
                var i = selected.Index;

                if (i > 0)
                {
                    var a = selected.Tag as IActionItem;

                    lvActions.Items.RemoveAt(i);
                    lvActions.Items.Insert(i - 1, new ListViewItem(new string[] { i.ToString(), a.ActionType.DisplayText() }) { Tag = a });
                    lvActions.Items[i - 1].Selected = true;

                    GroomActionIndicies();
                }
            }
        }

        private void btMoveActionDown_Click(object sender, EventArgs e)
        {
            if (lvActions.SelectedItems.Count > 0)
            {
                var selected = lvActions.SelectedItems[0];
                var i = selected.Index;

                if (i < lvActions.Items.Count - 1)
                {
                    var a = selected.Tag as IActionItem;

                    lvActions.Items.RemoveAt(i);
                    lvActions.Items.Insert(i + 1, new ListViewItem(new string[] { i.ToString(), a.ActionType.DisplayText() }) { Tag = a });
                    lvActions.Items[i + 1].Selected = true;

                    GroomActionIndicies();
                }
            }
        }

        private void GroomActionIndicies()
        {
            foreach (ListViewItem i in lvActions.Items)
            {
                i.Text = (lvActions.Items.IndexOf(i) + 1).ToString();
            }
        }

        private void GroomActionLabels()
        {
            foreach (ListViewItem i in lvActions.Items)
            {
                i.SubItems[1].Text = (i.Tag as IActionItem).ActionType.DisplayText();
            }
        }

        private void btEditAction_Click(object sender, EventArgs e)
        {
            if (lvActions.SelectedItems.Count > 0)
            {
                var sap = new ShortcutActionProperties(lvActions.SelectedItems[0].Tag as IActionItem);
                if (sap.ShowDialog(this) == DialogResult.OK)
                {
                    lvActions.SelectedItems[0].Tag = sap.ResultActionItem;
                    GroomActionLabels();
                }
            }
        }
    }
}