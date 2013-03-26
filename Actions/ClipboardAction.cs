using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProSnap.ActionItems
{
    class ClipboardAction : IActionItem
    {
        public enum Modes { Copy };

        [DescriptionAttribute("Determines how this action should behave when invoked.")]
        [DefaultValueAttribute(Modes.Copy)]
        public Modes ClipboardMode { get; set; }

        [DescriptionAttribute("When using Copy mode, puts the value on the clipboard. See documentation for available path variables.")]
        public string Content { get; set; }

        [Browsable(false)]
        public ActionTypes ActionType
        {
            get { return ActionTypes.Clipboard; }
        }

        public ClipboardAction(string content = null)
        {
            this.ClipboardMode = Modes.Copy;
            this.Content = content;
        }

        public IActionItem Clone()
        {
            return new ClipboardAction()
            {
                ClipboardMode = this.ClipboardMode,
                Content = this.Content,
            };
        }

        public ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot)
        {
            if (LatestScreenshot == null)
                return null;

            var tcs = new TaskCompletionSource<object>();
            Program.Preview.BeginInvoke(new MethodInvoker(() =>
            {
                switch (this.ClipboardMode)
                {
                    case Modes.Copy:
                        if (this.Content == ":image")
                        {
                            Clipboard.SetImage(LatestScreenshot.ComposedScreenshotImage);
                        }
                        else if (!string.IsNullOrEmpty(this.Content))
                        {
                            Clipboard.SetText(Helper.ExpandParameters(this.Content, LatestScreenshot), TextDataFormat.UnicodeText);
                        }
                        break;
                }

                tcs.SetResult(null);
            }));

            Task.WaitAll(tcs.Task);

            return LatestScreenshot;
        }
    }
}
