using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FMUtils.KeyboardHook;
using FMUtils.WinApi;
using ProSnap.ActionItems;
using ProSnap.Properties;

namespace ProSnap
{
    static class Program
    {
        private static Hook KeyboardHook;
        private static NotifyIcon TrayIcon;
        internal static List<ExtendedScreenshot> History;

        internal static PeekPreview Preview;
        internal static RegionSelector Selector;

        internal static bool isTakingScrollingScreenshot = false;
        internal static List<ExtendedScreenshot> timelapse = new List<ExtendedScreenshot>();

        private static BackgroundWorker IconAnimation = new BackgroundWorker();
        private static int CurrentIcon = 0;

        [STAThread]
        private static void Main()
        {
            Trace.Listeners.Add(new ReportListener());

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;

            //todo: It's reccomended that this be set in the manifest, but I can't figure out how to do that successfully with clickonce
            Windowing.SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            System.Threading.Thread.CurrentThread.Name = "Main UI " + System.Threading.Thread.CurrentThread.ManagedThreadId;
            Update.CheckForUpdate();

            History = new List<ExtendedScreenshot>();

            Preview = new PeekPreview();
            Selector = new RegionSelector();
            Trace.WriteLine(string.Format("Forcing the creation of windows by accessing their handles on the Main UI thread: {0}, {1}", Preview.Handle, Selector.Handle), string.Format("Program [{0}]", System.Threading.Thread.CurrentThread.Name));

            KeyboardHook = new Hook("Global Action Hook");
            KeyboardHook.KeyDownEvent += KeyDown;
            KeyboardHook.KeyUpEvent += KeyUp;

            TrayIcon = new NotifyIcon()
            {
                Icon = ProSnap.Properties.Resources.camera_36x36_icon,
                Text = "ProSnap",
                Visible = true,
                ContextMenuStrip = new ContextMenuStrip()
            };

            //TrayIcon.ContextMenuStrip.Items.Add("Take Screenshot", null, new EventHandler((o, e) => { Thread.Sleep(3000); SpawnActionChain(ActiveShortcutProfile.Shortcuts.FirstOrDefault(s => s.Actions.ActionItems.FirstOrDefault().ActionType == ActionTypes.TakeForegroundScreenshot)); }));

            TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("History", null, (o, e) => (o as ToolStripMenuItem).ShowDropDown()) { Name = "tsmiHistory" });
            TrayIcon.ContextMenuStrip.Items.Add("Options", null, (o, e) => Options.Options.ShowOrActivate());
            TrayIcon.ContextMenuStrip.Items.Add("Exit", null, (o, e) =>
            {
                Application.Exit();
                DumpTraceReport();
            });

            TrayIcon.MouseDown += TrayIcon_MouseDown;

            IconAnimation.WorkerSupportsCancellation = true;
            IconAnimation.DoWork += IconAnimation_DoWork;

            Application.Run();

            KeyboardHook.isPaused = true;
            TrayIcon.Visible = false;
        }

        private static void IconAnimation_DoWork(object sender, DoWorkEventArgs e)
        {
            do
            {
                CurrentIcon++;
                if (CurrentIcon > 2)
                    CurrentIcon = 1;

                TrayIcon.Icon = CurrentIcon == 1 ? ProSnap.Properties.Resources.camera_16x16_flash_small_icon : ProSnap.Properties.Resources.camera_16x16_flash_large_icon;

                Thread.Sleep(500);
            } while (!IconAnimation.CancellationPending);

            CurrentIcon = 0;
            TrayIcon.Icon = ProSnap.Properties.Resources.camera_16x16_icon;
        }

        private static void TrayIcon_MouseDown(object sender, MouseEventArgs e)
        {
            Trace.WriteLine(string.Format("{0} Button", e.Button), string.Format("TrayIcon.MouseDown [{0}]", System.Threading.Thread.CurrentThread.Name));

            switch (e.Button)
            {
                case MouseButtons.Left:
                    Trace.WriteLine("Show & activate preview...", string.Format("TrayIcon.MouseDown [{0}]", System.Threading.Thread.CurrentThread.Name));

                    Preview.Show();
                    Preview.Activate();
                    break;

                case MouseButtons.Right:
                    if (Configuration.UpdateRestartRequired && TrayIcon.ContextMenuStrip.Items[0].Name != "tsmiRestart")
                    {
                        TrayIcon.ContextMenuStrip.Items.Insert(0, new ToolStripMenuItem("Relaunch for update", null, (o, a) => Application.Restart()) { Name = "tsmiRestart" });
                        TrayIcon.ContextMenuStrip.Items.Insert(1, new ToolStripSeparator());
                    }

                    var DefaultFont = new ToolStripMenuItem().Font;

                    ToolStripMenuItem HistoryItem = TrayIcon.ContextMenuStrip.Items.Cast<ToolStripItem>().Where(tsi => tsi.Name == "tsmiHistory").FirstOrDefault() as ToolStripMenuItem;
                    HistoryItem.DropDownItems.Clear();
                    HistoryItem.DropDownItems.AddRange(History.Select(ess => new ToolStripMenuItem(ess.WindowTitle, null, (mi, e_mi) => Program.Preview.Show(ess))
                    {
                        Tag = ess,
                        Image = ess.isFlagged ? Resources.heart_fill_12x11 : null,
                        Font = Preview.CurrentScreenshot == ess ? new Font(DefaultFont, FontStyle.Bold) : DefaultFont
                    }).ToArray());

                    HistoryItem.Enabled = HistoryItem.DropDownItems.Count > 0;
                    break;
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var InnermostException = e.ExceptionObject as Exception;
                Trace.WriteLine(string.Format("Unhandled Exception at '{0}':\n{1}", InnermostException.TargetSite, InnermostException), string.Format("Program.CurrentDomain_UnhandledException [{0}]", System.Threading.Thread.CurrentThread.Name));

                DumpTraceReport();

                if (!Debugger.IsAttached)
                    Crash.SubmitCrashReport();

                new CrashReportForm().ShowDialog();
                Application.Exit();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Unhandled Exception... Exception:\n{0}", ex), string.Format("Program.CurrentDomain_UnhandledException [{0}]", System.Threading.Thread.CurrentThread.Name));
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            CurrentDomain_UnhandledException(sender, new UnhandledExceptionEventArgs(e.Exception, true));
        }

        internal static void DumpTraceReport()
        {
            try
            {
                ReportListener reporter = Trace.Listeners.Cast<TraceListener>().Where(tl => tl is ReportListener).FirstOrDefault() as ReportListener;
                File.WriteAllText(Path.Combine(Configuration.LocalPath, "report.txt"), string.Join("\n", reporter.Messages.Select(m => string.Format("<{0}> {1}: {2}", m.Timestamp, m.Category, m.Message)).ToArray()));
            }
            catch (Exception e)
            {

            }
        }

        private static void KeyDown(KeyboardHookEventArgs e)
        {
            //Trace.WriteLine(e.ToString(), "Program.KeyDown");

            //Trace.WriteLine("Shortcut match: " + CurrentShortcutItem.ToString(), "Program.KeyDown");
            //Trace.WriteLine("isTakingScrollingScreenshot: " + isTakingScrollingScreenshot, "Program.KeyDown");

            var Shortcut = Configuration.Shortcuts.FirstOrDefault(S => S.Enabled && S.KeyCombo.Key == e.Key && S.KeyCombo.isAltPressed == e.isAltPressed && S.KeyCombo.isCtrlPressed == e.isCtrlPressed && S.KeyCombo.isShiftPressed == e.isShiftPressed && S.KeyCombo.isWinPressed == e.isWinPressed);

            if (Configuration.IgnoreAllKeyHooks || Shortcut == null || Shortcut.RequirePreviewOpen && !Preview.Focused)
                return;

            SpawnActionChain(Shortcut);
        }

        private static void KeyUp(KeyboardHookEventArgs e)
        {
            //Trace.WriteLine(e.ToString(), "Program.KeyUp");

            if (Configuration.IgnoreAllKeyHooks)
                return;

            //if (e.Key == Keys.RControlKey && isTakingScrollingScreenshot)
            //{
            //    //Trace.WriteLine("isTakingScrollingScreenshot: " + isTakingScrollingScreenshot);
            //    SpawnActionChain(ActiveShortcutProfile.Shortcuts.LastOrDefault());
            //}
        }

        private static void SpawnActionChain(ShortcutItem CurrentShortcutItem)
        {
            Trace.WriteLine("Beginning action chain for " + CurrentShortcutItem, string.Format("Program.SpawnActionChain [{0}]", System.Threading.Thread.CurrentThread.Name));

            //Creating a STA thread manually here, because in order for SaveFileDialog to work it must be created on a STA thread. BackgroundWorker/ThreadPool is MTA.
            //todo: Is the above still relevant now that we safely invoke to the UI thread?

            var HookAction = new Thread(new ThreadStart(() => DoActionItem(CurrentShortcutItem, 0, Preview.CurrentScreenshot ?? History.LastOrDefault())));
            HookAction.Name = "Hook Action " + HookAction.ManagedThreadId;
            HookAction.SetApartmentState(ApartmentState.STA);
            HookAction.Start();
        }

        private static void DoActionItem(ShortcutItem ActiveShortcutItem, int itemIndex, ExtendedScreenshot ActionItemScreenshot)
        {
            if (ActiveShortcutItem == null || itemIndex >= ActiveShortcutItem.ActionChain.ActionItems.Count)
            {
                Trace.WriteLine("No more action items to execute", string.Format("Program.DoActionItem [{0}]", System.Threading.Thread.CurrentThread.Name));

                //todo: Why can't the hook be reinstated on the action thread?
                Preview.BeginInvoke(new MethodInvoker(() => KeyboardHook.isPaused = false));
                IconAnimation.CancelAsync();

                return;
            }

            IActionItem CurrentActionItem = ActiveShortcutItem.ActionChain.ActionItems[itemIndex];

            //don't try and invoke the scrolling screenshot actions if they're the first type in the action chain (eg, solo items for their shortcuts) and there's no active scrolling screenshot happening
            //at least this solves flashing the icon whenever 'End' key is pressed during normal computer use
            if (!Program.isTakingScrollingScreenshot && (CurrentActionItem.ActionType == ActionTypes.ContinueScrollingScreenshot || CurrentActionItem.ActionType == ActionTypes.EndScrollingScreenshot) && itemIndex == 0)
                return;

            KeyboardHook.isPaused = true;

            Trace.WriteLine(CurrentActionItem.ActionType, string.Format("Program.DoActionItem [{0}]", System.Threading.Thread.CurrentThread.Name));

            //todo: figure out a better way to do these exclusions rather than harcoding behavior for scrolling screenshots
            if (!IconAnimation.IsBusy)
                if ((new[] { ActionTypes.ContinueScrollingScreenshot, ActionTypes.EndScrollingScreenshot }.Contains(CurrentActionItem.ActionType) && isTakingScrollingScreenshot) || !new[] { ActionTypes.ContinueScrollingScreenshot, ActionTypes.EndScrollingScreenshot }.Contains(CurrentActionItem.ActionType))
                    IconAnimation.RunWorkerAsync();

            Task.Factory.StartNew<ExtendedScreenshot>(() => CurrentActionItem.Invoke(ActionItemScreenshot))
                .ContinueWith(t => DoActionItem(ActiveShortcutItem, itemIndex + 1, t.Result));
        }
    }
}
