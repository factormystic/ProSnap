using System;
using System.ComponentModel;
using System.Diagnostics;
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
            Trace.WriteLine("Applying ClipboardAction...", string.Format("ClipboardAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));

            if (LatestScreenshot == null)
            {
                Trace.WriteLine("Latest Screenshot is null, continuing...", string.Format("ClipboardAction.Invoke [{0}]", System.Threading.Thread.CurrentThread.Name));
                return null;
            }

            var set = new Action(() =>
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
                            var text = Helper.ExpandParameters(this.Content, LatestScreenshot);
                            if (!string.IsNullOrEmpty(text))
                            {
                                Clipboard.SetText(text, TextDataFormat.UnicodeText);
                            }
                        }
                        break;
                }
            });

            if (Program.Preview.InvokeRequired)
            {
                var tcs = new TaskCompletionSource<object>();
                Program.Preview.BeginInvoke(new MethodInvoker(() =>
                {
                    set();
                    tcs.SetResult(null);
                }));

                Task.WaitAll(tcs.Task);
            }
            else
            {
                set();
            }

            return LatestScreenshot;
        }
    }
}
