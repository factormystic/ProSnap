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
        public static ExtendedScreenshot CurrentHistoryItem { get; set; }

        internal static bool isTakingScrollingScreenshot = false;
        internal static List<ExtendedScreenshot> timelapse = new List<ExtendedScreenshot>();

        public delegate void PreviewEventHandler(ExtendedScreenshot s, PreviewEventArgs e);
        public static event PreviewEventHandler ShowPreviewEvent = delegate { };

        private static BackgroundWorker IconAnimation = new BackgroundWorker();
        private static int CurrentIcon = 0;

        private static object _actionlock = new object();

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

            Thread.CurrentThread.Name = "Main UI " + Thread.CurrentThread.ManagedThreadId;
            Update.CheckForUpdate();

            History = new List<ExtendedScreenshot>();

            Preview = new PeekPreview();
            Trace.WriteLine("Forcing the creation of PeekPreview by accessing its handle on the Main UI thread: " + Preview.Handle, string.Format("Program [{0}]", Thread.CurrentThread.Name));

            KeyboardHook = new Hook("Global Action Hook");
            KeyboardHook.KeyDownEvent += KeyDown;
            KeyboardHook.KeyUpEvent += KeyUp;

            TrayIcon = new NotifyIcon()
            {
                Icon = ProSnap.Properties.Resources.camera_16x16_icon,
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

            ShowPreviewEvent += Program_ShowPreviewEvent;

            IconAnimation.WorkerSupportsCancellation = true;
            IconAnimation.DoWork += IconAnimation_DoWork;

            AppDomain.CurrentDomain.ProcessExit += (o, e) => KeyboardHook.isPaused = true;
            Application.Run();

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
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        Trace.WriteLine("Left Button", string.Format("TrayIcon.MouseDown [{0}]", Thread.CurrentThread.Name));

                        if (Preview.Visible && Preview.Opacity == 1)
                        {
                            Trace.WriteLine("Activating preview...", string.Format("TrayIcon.MouseDown [{0}]", Thread.CurrentThread.Name));
                            Preview.Activate();
                        }
                        else
                        {
                            var ToShow = CurrentHistoryItem ?? History.LastOrDefault();
                            if (ToShow != null)
                            {
                                Trace.WriteLine("Showing preview with image...", string.Format("TrayIcon.MouseDown [{0}]", Thread.CurrentThread.Name));
                                ShowPreviewEvent(ToShow, new PreviewEventArgs());
                                Preview.Activate();
                            }
                        }
                    } break;

                case MouseButtons.Right:
                    {
                        Trace.WriteLine("Right Button", string.Format("TrayIcon.MouseDown [{0}]", Thread.CurrentThread.Name));

                        if (Configuration.UpdateRestartRequired && TrayIcon.ContextMenuStrip.Items[0].Name != "tsmiRestart")
                        {
                            TrayIcon.ContextMenuStrip.Items.Insert(0, new ToolStripMenuItem("Relaunch for update", null, (o, a) => Application.Restart()) { Name = "tsmiRestart" });
                            TrayIcon.ContextMenuStrip.Items.Insert(1, new ToolStripSeparator());
                        }

                        var DefaultFont = new ToolStripMenuItem().Font;

                        ToolStripMenuItem HistoryItem = TrayIcon.ContextMenuStrip.Items.Cast<ToolStripItem>().Where(tsi => tsi.Name == "tsmiHistory").FirstOrDefault() as ToolStripMenuItem;
                        HistoryItem.DropDownItems.Clear();
                        HistoryItem.DropDownItems.AddRange(History.Select(ess => new ToolStripMenuItem(ess.WindowTitle, null, (mi, e_mi) => ShowPreviewEvent(ess, new PreviewEventArgs()))
                        {
                            Tag = ess,
                            Image = ess.isFlagged ? Resources.heart_fill_12x11 : null,
                            Font = ess == CurrentHistoryItem ? new Font(DefaultFont, FontStyle.Bold) : DefaultFont
                        }).ToArray());

                        HistoryItem.Enabled = HistoryItem.DropDownItems.Count > 0;

                    } break;
            }
        }

        private static void Program_ShowPreviewEvent(ExtendedScreenshot s, PreviewEventArgs e)
        {
            if (Preview.InvokeRequired)
            {
                Trace.WriteLine("Self invoking...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

                Preview.BeginInvoke(new MethodInvoker(() => Program_ShowPreviewEvent(s, e)));
                return;
            }

            //It is much easier to simply invoke onto the preview thread for actions which have UI ramification, rather than managing cross thread status updates
            //Currently there are only a couple actions this really makes sense for: Heart, Save, Upload, Delete (because it potentially also hides the form)
            //Save doesn't really have a UI change, but doing it on the UI thread means the SFD is positioned correctly

            //The locking scheme is so that dispatched operations block the action thread until complete
            //So for example, an action chain with Upload followed by Run can use the uploaded image url as a parameter for itself

            if (s != null)
            {
                Trace.WriteLine("Loading preview screenshot...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));
                Preview.LoadScreenshot(s);
            }

            if (e.ActionItem == null)
            {
                Trace.WriteLine("Showing preview...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));
                Preview.Show();
            }

            //if (e.ActionItem is HeartAction)
            //{
            //    Trace.WriteLine("Applying HeartAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

            //    lock (_actionlock)
            //    {
            //        switch ((e.ActionItem as HeartAction).HeartMode)
            //        {
            //            case HeartAction.Modes.Toggle: s.isFlagged = !s.isFlagged; break;
            //            case HeartAction.Modes.On: s.isFlagged = true; break;
            //            case HeartAction.Modes.Off: s.isFlagged = false; break;
            //        }

            //        Preview.UpdateHeart();
            //        Monitor.Pulse(_actionlock);
            //    }
            //}

            //if (e.ActionItem is SaveAction)
            //{
            //    Trace.WriteLine("Applying SaveAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

            //    Preview.SaveComplete += (ss, se) =>
            //        {
            //            lock (_actionlock)
            //                Monitor.Pulse(_actionlock);
            //        };

            //    lock (_actionlock)
            //        Preview.Save();
            //}

            //if (e.ActionItem is UploadAction)
            //{
            //    Trace.WriteLine("Applying UploadAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

            //    Preview.UploadComplete += (us, ue) =>
            //        {
            //            lock (_actionlock)
            //                Monitor.Pulse(_actionlock);
            //        };

            //    lock (_actionlock)
            //        Preview.Upload();
            //}

            //if (e.ActionItem is DeleteAction)
            //{
            //    Trace.WriteLine("Applying DeleteAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

            //    lock (_actionlock)
            //    {
            //        Preview.Delete();
            //        Monitor.Pulse(_actionlock);
            //    }
            //}

            //if (e.ActionItem is HidePreviewAction)
            //{
            //    Trace.WriteLine("Applying HidePreviewAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

            //    lock (_actionlock)
            //    {
            //        Preview.FadeClose();
            //        Monitor.Pulse(_actionlock);
            //    }
            //}

            //if (e.ActionItem is TakeRegionScreenshotAction)
            //{
            //    Trace.WriteLine("Opening region selector...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

            //    var rs = new RegionSelector();
            //    rs.FormClosed += (ss, se) =>
            //    {
            //        Trace.WriteLine("Closed region selector.", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

            //        lock (_actionlock)
            //        {
            //            e.Result = rs.DialogResult == DialogResult.OK ? rs.SnapshotRectangle : Rectangle.Empty;
            //            Monitor.Pulse(_actionlock);
            //        }
            //    };

            //    lock (_actionlock)
            //        rs.Show();
            //}

            Trace.WriteLine("Done.", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var InnermostException = e.ExceptionObject as Exception;
                Trace.WriteLine(string.Format("Unhandled Exception at '{0}':\n{1}", InnermostException.TargetSite, InnermostException), string.Format("Program.CurrentDomain_UnhandledException [{0}]", Thread.CurrentThread.Name));

                DumpTraceReport();
                Crash.SubmitCrashReport();

                new CrashReportForm().ShowDialog();
                Application.Exit();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Unhandled Exception... Exception:\n{0}", ex), string.Format("Program.CurrentDomain_UnhandledException [{0}]", Thread.CurrentThread.Name));
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
            Trace.WriteLine("Beginning action chain for " + CurrentShortcutItem, string.Format("Program.SpawnActionChain [{0}]", Thread.CurrentThread.Name));

            //Creating a STA thread manually here, because in order for SaveFileDialog to work it must be created on a STA thread. BackgroundWorker/ThreadPool is MTA.
            //todo: Is the above still relevant now that we safely invoke to the UI thread?

            var HookAction = new Thread(new ThreadStart(() => DoActionItem(CurrentShortcutItem, 0, CurrentHistoryItem ?? History.LastOrDefault())));
            HookAction.Name = "Hook Action " + HookAction.ManagedThreadId;
            HookAction.SetApartmentState(ApartmentState.STA);
            HookAction.Start();
        }

        private static void DoActionItem(ShortcutItem ActiveShortcutItem, int itemIndex, ExtendedScreenshot ActionItemScreenshot)
        {
            if (ActiveShortcutItem == null || itemIndex >= ActiveShortcutItem.ActionChain.ActionItems.Count)
            {
                Trace.WriteLine("No more action items to execute", string.Format("Program.DoActionItem [{0}]", Thread.CurrentThread.Name));

                //todo: Why can't the hook be reinstated on the action thread?
                Preview.BeginInvoke(new MethodInvoker(() => KeyboardHook.isPaused = false));
                IconAnimation.CancelAsync();

                return;
            }

            KeyboardHook.isPaused = true;

            IActionItem CurrentActionItem = ActiveShortcutItem.ActionChain.ActionItems[itemIndex];
            Trace.WriteLine(CurrentActionItem.ActionType, string.Format("Program.DoActionItem [{0}]", Thread.CurrentThread.Name));

            //todo: figure out a better way to do these exclusions rather than harcoding behavior for scrolling screenshots
            if (!IconAnimation.IsBusy)
                if ((new[] { ActionTypes.ContinueScrollingScreenshot, ActionTypes.EndScrollingScreenshot }.Contains(CurrentActionItem.ActionType) && isTakingScrollingScreenshot) || !new[] { ActionTypes.ContinueScrollingScreenshot, ActionTypes.EndScrollingScreenshot }.Contains(CurrentActionItem.ActionType))
                    IconAnimation.RunWorkerAsync();

            //todo: on first action, capture and pass through the continuations the current screenshot item
            //this should then mean that no action looks up history.last(). That's flawed anyway, since the active item may not always be last
            //esp if a chain is called from the preview window, but a screenshot from the middle of the history is open
            //or if an action is long running and another shot has been taken in that time
            //can't rely on the global variable for that reason, as well. pass it along as a parameter
            Task.Factory.StartNew<ExtendedScreenshot>(() => CurrentActionItem.Invoke(ActionItemScreenshot))
                .ContinueWith(t => DoActionItem(ActiveShortcutItem, itemIndex + 1, t.Result));
        }

        internal static void InvokeShowPreviewEvent(ExtendedScreenshot extendedScreenshot, PreviewEventArgs previewEventArgs)
        {
            ShowPreviewEvent(extendedScreenshot, previewEventArgs);
        }
    }
}
