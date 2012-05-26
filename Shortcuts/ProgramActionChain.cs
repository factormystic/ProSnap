using System.Collections.Generic;
using ProSnap.ActionItems;

namespace ProSnap
{
    public class ProgramActionChain
    {
        public string Name { get; set; }
        public List<IActionItem> ActionItems { get; set; }

        public ProgramActionChain(string name)
        {
            this.Name = name;
            this.ActionItems = new List<IActionItem>();
        }

        public override string ToString()
        {
            return string.Format("{0}", this.Name);
        }
    }
}
