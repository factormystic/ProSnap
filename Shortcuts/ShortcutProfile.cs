using System.Collections.Generic;

namespace ProSnap
{
    public class ShortcutProfile
    {
        public string Name { get; set; }

        //isChecked, KeyCombo, ProgramAction
        public List<ShortcutItem> Shortcuts { get; private set; }
        public bool isActiveProfile { get; set; }

        public ShortcutProfile(string name)
        {
            this.Name = name;
            this.Shortcuts = new List<ShortcutItem>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
