using System.Windows.Forms;

namespace ProSnap
{
    public class ShortcutItem
    {
        public bool Enabled { get; set; }
        public bool RequirePreviewOpen { get; set; }
        public KeyCombo KeyCombo { get; set; }
        public ProgramActionChain ActionChain { get; set; }

        public ShortcutItem(bool enabled, bool requirePreviewOpen, KeyCombo keyCombo, ProgramActionChain actionChain)
        {
            this.Enabled = enabled;
            this.RequirePreviewOpen = requirePreviewOpen;
            this.KeyCombo = keyCombo;
            this.ActionChain = actionChain;
        }

        public static ShortcutItem Empty()
        {
            return new ShortcutItem(false, false, new KeyCombo(Keys.None), new ProgramActionChain("New Action Chain"));
        }

        public override string ToString()
        {
            return string.Format("[{0}]: {1}", this.KeyCombo, this.ActionChain);
        }
    }
}
