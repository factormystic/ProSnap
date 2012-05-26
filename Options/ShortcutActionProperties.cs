using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProSnap.ActionItems;

namespace ProSnap.Options
{
    public partial class ShortcutActionProperties : Form
    {
        List<ActionTypes> ActionTypesList;
        IActionItem SelectedActionItem;

        public IActionItem ResultActionItem { get; private set; }

        public ShortcutActionProperties(IActionItem selectedActionItem)
        {
            InitializeComponent();

            this.MinimumSize = this.Size;

            SelectedActionItem = selectedActionItem;
            ActionTypesList = Enum.GetValues(typeof(ActionTypes)).Cast<ActionTypes>().ToList();

            cbActionType.DataSource = ActionTypesList.Select(at => new { Text = at.DisplayText(), Value = at }).ToList();
            cbActionType.ValueMember = "Value";
            cbActionType.DisplayMember = "Text";

            cbActionType.SelectedIndex = ActionTypesList.FindIndex(a => a == selectedActionItem.ActionType);
        }

        private void cbActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pgActionProperties.SelectedObject = SelectedActionItem.ActionType == ActionTypesList[cbActionType.SelectedIndex] ? SelectedActionItem.Clone() : ActionTypesList[cbActionType.SelectedIndex].ToInstance();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            ResultActionItem = pgActionProperties.SelectedObject as IActionItem;
        }
    }
}
