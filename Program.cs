using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
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

        private static PeekPreview Preview;
        public static ExtendedScreenshot CurrentHistoryItem { get; set; }

        private static bool isTakingScrollingScreenshot = false;
        private static List<ExtendedScreenshot> timelapse = new List<ExtendedScreenshot>();

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

                        if (Preview.Opacity == 1)
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

            if (e.ActionItem is HeartAction)
            {
                Trace.WriteLine("Applying HeartAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

                lock (_actionlock)
                {
                    switch ((e.ActionItem as HeartAction).HeartMode)
                    {
                        case HeartAction.Modes.Toggle: s.isFlagged = !s.isFlagged; break;
                        case HeartAction.Modes.On: s.isFlagged = true; break;
                        case HeartAction.Modes.Off: s.isFlagged = false; break;
                    }

                    Preview.UpdateHeart();
                    Monitor.Pulse(_actionlock);
                }
            }

            if (e.ActionItem is SaveAction)
            {
                Trace.WriteLine("Applying SaveAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

                Preview.SaveComplete += (ss, se) =>
                    {
                        lock (_actionlock)
                            Monitor.Pulse(_actionlock);
                    };

                lock (_actionlock)
                    Preview.Save();
            }

            if (e.ActionItem is UploadAction)
            {
                Trace.WriteLine("Applying UploadAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

                Preview.UploadComplete += (us, ue) =>
                    {
                        lock (_actionlock)
                            Monitor.Pulse(_actionlock);
                    };

                lock (_actionlock)
                    Preview.Upload();
            }

            if (e.ActionItem is DeleteAction)
            {
                Trace.WriteLine("Applying DeleteAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

                lock (_actionlock)
                {
                    Preview.Delete();
                    Monitor.Pulse(_actionlock);
                }
            }

            if (e.ActionItem is HidePreviewAction)
            {
                Trace.WriteLine("Applying HidePreviewAction...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

                lock (_actionlock)
                {
                    Preview.FadeClose();
                    Monitor.Pulse(_actionlock);
                }
            }

            if (e.ActionItem is TakeRegionScreenshotAction)
            {
                Trace.WriteLine("Opening region selector...", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

                var rs = new RegionSelector();
                rs.FormClosed += (ss, se) =>
                {
                    Trace.WriteLine("Closed region selector.", string.Format("Program.Program_ShowPreviewEvent [{0}]", Thread.CurrentThread.Name));

                    lock (_actionlock)
                    {
                        e.Result = rs.DialogResult == DialogResult.OK ? rs.SnapshotRectangle : Rectangle.Empty;
                        Monitor.Pulse(_actionlock);
                    }
                };

                lock (_actionlock)
                    rs.Show();
            }

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

            var HookAction = new Thread(new ThreadStart(() => DoActionItem(CurrentShortcutItem, 0)));
            HookAction.Name = "Hook Action " + HookAction.ManagedThreadId;
            HookAction.SetApartmentState(ApartmentState.STA);
            HookAction.Start();
        }

        private static void DoActionItem(ShortcutItem ActiveShortcutItem, int itemIndex)
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

            switch (CurrentActionItem.ActionType)
            {
                case ActionTypes.TakeForegroundScreenshot:
                    {
                        var CurrentActionConfig = CurrentActionItem as TakeForegroundScreenshotAction;

                        try
                        {
                            var ForegroundWindowScreenshot = new ExtendedScreenshot(CurrentActionConfig.Method, CurrentActionConfig.SolidGlass);
                            History.Add(ForegroundWindowScreenshot);
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine(string.Format("Exception in TakeForegroundScreenshot: {0}", e.GetBaseException()), string.Format("Program.DoActionItem [{0}]", Thread.CurrentThread.Name));

                            ReportListener reporter = Trace.Listeners.Cast<TraceListener>().Where(tl => tl is ReportListener).FirstOrDefault() as ReportListener;
                            File.WriteAllText(Path.Combine(Configuration.LocalPath, "report.txt"), string.Join("\n", reporter.Messages.Select(m => string.Format("{0} {1}: {2}", m.Timestamp, m.Category, m.Message)).ToArray()));
                        }

                        DoActionItem(ActiveShortcutItem, itemIndex + 1);
                    } break;

                case ActionTypes.TakeRegionScreenshot:
                    {
                        var CurrentActionConfig = CurrentActionItem as TakeRegionScreenshotAction;
                        Rectangle SelectedRegion = CurrentActionConfig.Region;

                        try
                        {
                            if (CurrentActionConfig.UseRegionSelector)
                            {
                                lock (_actionlock)
                                {
                                    var e = new PreviewEventArgs() { ActionItem = CurrentActionItem };
                                    ShowPreviewEvent(null, e);

                                    Monitor.Wait(_actionlock);
                                    SelectedRegion = (Rectangle)e.Result;
                                }
                            }

                            if (!SelectedRegion.IsEmpty)
                            {
                                var RegionScreenshot = new ExtendedScreenshot(SelectedRegion);
                                History.Add(RegionScreenshot);

                                DoActionItem(ActiveShortcutItem, itemIndex + 1);
                            }
                            else
                            {
                                DoActionItem(null, itemIndex + 1);
                            }
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine(string.Format("Exception in TakeRegionScreenshot: {0}", e.GetBaseException()), string.Format("Program.DoActionItem [{0}]", Thread.CurrentThread.Name));

                            ReportListener reporter = Trace.Listeners.Cast<TraceListener>().Where(tl => tl is ReportListener).FirstOrDefault() as ReportListener;
                            File.WriteAllText(Path.Combine(Configuration.LocalPath, "report.txt"), string.Join("\n", reporter.Messages.Select(m => string.Format("{0} {1}: {2}", m.Timestamp, m.Category, m.Message)).ToArray()));

                            DoActionItem(null, itemIndex + 1);

                        }
                    } break;

                case ActionTypes.ShowPreview:
                    {
                        if (Configuration.PreviewDelayTime != 0 && History.Count > 0)
                            ShowPreviewEvent(History.Last(), new PreviewEventArgs());

                        DoActionItem(ActiveShortcutItem, itemIndex + 1);
                    } break;
                case ActionTypes.Heart:
                    {
                        var LatestScreenshot = History.LastOrDefault();
                        if (LatestScreenshot != null)
                        {
                            lock (_actionlock)
                            {
                                ShowPreviewEvent(LatestScreenshot, new PreviewEventArgs() { ActionItem = CurrentActionItem });
                                Monitor.Wait(_actionlock);
                                DoActionItem(ActiveShortcutItem, itemIndex + 1);
                            }
                        }
                    } break;
                case ActionTypes.Save:
                    {
                        var LatestScreenshot = History.LastOrDefault();
                        if (LatestScreenshot != null)
                        {
                            var Save = CurrentActionItem as SaveAction;
                            Trace.WriteLine(string.Format("Prompting for save: {0}", Save.Prompt), string.Format("Program.DoActionItem Save [{0}]", Thread.CurrentThread.Name));

                            if (Save.Prompt)
                            {
                                lock (_actionlock)
                                {
                                    ShowPreviewEvent(LatestScreenshot, new PreviewEventArgs() { ActionItem = CurrentActionItem });
                                    Monitor.Wait(_actionlock);
                                    DoActionItem(ActiveShortcutItem, itemIndex + 1);
                                }
                            }
                            else
                            {
                                string FileName = Environment.ExpandEnvironmentVariables(Helper.ExpandParameters(Save.FilePath, LatestScreenshot));
                                if (!string.IsNullOrEmpty(FileName))
                                {
                                    try
                                    {
                                        var dir = Path.GetDirectoryName(FileName);
                                        if (!Directory.Exists(dir))
                                        {
                                            Trace.WriteLine("Attempting to create directory...", string.Format("Program.DoActionItem Save [{0}]", Thread.CurrentThread.Name));
                                            Directory.CreateDirectory(dir);
                                        }

                                        LatestScreenshot.ComposedScreenshotImage.Save(FileName, Helper.ExtToImageFormat(Path.GetExtension(FileName)));
                                        LatestScreenshot.SavedFileName = FileName;
                                    }
                                    catch (Exception e)
                                    {
                                        MessageBox.Show(string.Format("There was a problem saving your screenshot:\r\n\r\n{0}", e.GetBaseException().Message), "ProSnap", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(string.Format("There was a problem saving your screenshot:\r\n\r\n{0}", "No file name has been provided."), "ProSnap", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                }

                                DoActionItem(ActiveShortcutItem, itemIndex + 1);
                            }
                        }
                    } break;

                case ActionTypes.Upload:
                    {
                        var LatestScreenshot = History.LastOrDefault();
                        if (LatestScreenshot != null)
                        {
                            lock (_actionlock)
                            {
                                ShowPreviewEvent(LatestScreenshot, new PreviewEventArgs() { ActionItem = CurrentActionItem });
                                Monitor.Wait(_actionlock);
                                DoActionItem(ActiveShortcutItem, itemIndex + 1);
                            }
                        }
                    } break;

                case ActionTypes.ApplyEdits:
                    {
                        var LatestScreenshot = History.LastOrDefault();
                        if (LatestScreenshot != null)
                        {
                            var CurrentActionConfig = CurrentActionItem as ApplyEditsAction;

                            switch (CurrentActionConfig.DefaultBorderRounding)
                            {
                                case ApplyEditsAction.ApplicationMode.Automatic: LatestScreenshot.withBorderRounding = !LatestScreenshot.isMaximized && LatestScreenshot.isRounded && !(FMUtils.WinApi.Helper.OperatingSystem == FMUtils.WinApi.Helper.OperatingSystems.Win8); break;
                                case ApplyEditsAction.ApplicationMode.On: LatestScreenshot.withBorderRounding = true; break;
                                case ApplyEditsAction.ApplicationMode.Off: LatestScreenshot.withBorderRounding = false; break;
                            }

                            switch (CurrentActionConfig.DefaultBorderShadow)
                            {
                                case ApplyEditsAction.ApplicationMode.Automatic: LatestScreenshot.withBorderShadow = !LatestScreenshot.isMaximized; break;
                                case ApplyEditsAction.ApplicationMode.On: LatestScreenshot.withBorderShadow = true; break;
                                case ApplyEditsAction.ApplicationMode.Off: LatestScreenshot.withBorderShadow = false; break;
                            }

                            switch (CurrentActionConfig.ShowMouseCursor)
                            {
                                case ApplyEditsAction.ApplicationMode.Automatic: LatestScreenshot.withCursor = LatestScreenshot.CompositionRect.Contains(LatestScreenshot.CursorLocation); break;
                                case ApplyEditsAction.ApplicationMode.On: LatestScreenshot.withCursor = true; break;
                                case ApplyEditsAction.ApplicationMode.Off: LatestScreenshot.withCursor = false; break;
                            }
                        }

                        DoActionItem(ActiveShortcutItem, itemIndex + 1);
                    } break;

                case ActionTypes.Run:
                    {
                        var LatestScreenshot = History.LastOrDefault();
                        if (LatestScreenshot != null)
                        {
                            var Run = CurrentActionItem as RunAction;

                            if (!File.Exists(LatestScreenshot.InternalFileName))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(LatestScreenshot.InternalFileName));
                                LatestScreenshot.ComposedScreenshotImage.Save(LatestScreenshot.InternalFileName, ImageFormat.Png);
                            }

                            var parameters = Helper.ExpandParameters(Run.Parameters, LatestScreenshot);
                            var working = Environment.ExpandEnvironmentVariables(Helper.ExpandParameters(Run.WorkingDirectory, LatestScreenshot));

                            switch (Run.Mode)
                            {
                                case RunAction.Modes.ShellVerb:
                                    {
                                        Windowing.ShellExecute(IntPtr.Zero, Run.ShellVerb, LatestScreenshot.InternalFileName, parameters, working, Windowing.ShowCommands.SW_NORMAL);
                                    } break;
                                case RunAction.Modes.FilePath:
                                    {
                                        var psi = new ProcessStartInfo(Environment.ExpandEnvironmentVariables(Helper.ExpandParameters(Run.ApplicationPath, LatestScreenshot)), parameters)
                                        {
                                            UseShellExecute = false,
                                            WorkingDirectory = working,
                                            CreateNoWindow = Run.HideCommandPrompt,
                                        };

                                        Process.Start(psi);
                                    } break;
                            }
                        }

                        DoActionItem(ActiveShortcutItem, itemIndex + 1);
                    } break;

                case ActionTypes.Delete:
                    {
                        var LatestScreenshot = History.LastOrDefault();
                        if (LatestScreenshot != null)
                        {
                            lock (_actionlock)
                            {
                                ShowPreviewEvent(LatestScreenshot, new PreviewEventArgs() { ActionItem = CurrentActionItem });
                                Monitor.Wait(_actionlock);
                                DoActionItem(ActiveShortcutItem, itemIndex + 1);
                            }
                        }
                    } break;

                case ActionTypes.HidePreview:
                    {
                        lock (_actionlock)
                        {
                            ShowPreviewEvent(null, new PreviewEventArgs() { ActionItem = CurrentActionItem });
                            Monitor.Wait(_actionlock);
                            DoActionItem(ActiveShortcutItem, itemIndex + 1);
                        }
                    } break;

                case ActionTypes.BeginScrollingScreenshot:
                    {
                        isTakingScrollingScreenshot = true;

                        timelapse.Clear();
                        timelapse.Add(new ExtendedScreenshot());

                        DoActionItem(ActiveShortcutItem, itemIndex + 1);
                    } break;

                case ActionTypes.ContinueScrollingScreenshot:
                    {
                        if (isTakingScrollingScreenshot)
                            timelapse.Add(new ExtendedScreenshot());

                        DoActionItem(ActiveShortcutItem, itemIndex + 1);
                    } break;

                case ActionTypes.EndScrollingScreenshot:
                    {
                        if (isTakingScrollingScreenshot)
                        {
                            isTakingScrollingScreenshot = false;

                            Bitmap final = null;

                            Bitmap b = new Bitmap(timelapse.First().BaseScreenshotImage.Width, timelapse.First().BaseScreenshotImage.Height * timelapse.Count);
                            Graphics g = Graphics.FromImage(b);
                            int yoffset = 0;
                            int working_height = 0;

                            int ignore_header = 100;
                            int ignore_footer = 10;

                            var offsets = new Dictionary<int, int>();

                            //foreach (var ss in timelapse)
                            for (int c = 0; c < timelapse.Count; c++)
                            {
                                var debug = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"prosnap-debug");
                                if (Directory.Exists(debug))
                                {
                                    timelapse[c].BaseScreenshotImage.Save(Path.Combine(debug, @"ss-" + c + ".png"), ImageFormat.Png);
                                }

                                if (c == 0)
                                {
                                    g.DrawImageUnscaled(timelapse[c].BaseScreenshotImage, new Point(0, 0));
                                    working_height = timelapse[c].BaseScreenshotImage.Height;
                                    offsets.Add(c, timelapse[c].BaseScreenshotImage.Height);
                                }
                                else
                                {
                                    int old_yoffset = yoffset;
                                    yoffset = GetYOffset(timelapse[c - 1].BaseScreenshotImage, timelapse[c].BaseScreenshotImage, ignore_header, ignore_footer, old_yoffset, working_height, c);

                                    offsets.Add(c, yoffset);

                                    int sum = 0;
                                    if (offsets.Count > 0)
                                        for (int k = offsets.First().Key; k <= offsets.Last().Key; k++)
                                            sum += offsets[k];

                                    working_height = sum;

                                    g.DrawImage(timelapse[c].BaseScreenshotImage, new RectangleF(0, working_height - (timelapse[c].BaseScreenshotImage.Height - ignore_header), timelapse[c].BaseScreenshotImage.Width, timelapse[c].BaseScreenshotImage.Height - ignore_header), new RectangleF(0, ignore_header, timelapse[c].BaseScreenshotImage.Width, timelapse[c].BaseScreenshotImage.Height - ignore_header), GraphicsUnit.Pixel);
                                }

                                g.Flush();
                                g.Save();

                                final = new Bitmap(b.Width, working_height);
                                Graphics gfinal = Graphics.FromImage(final);
                                gfinal.DrawImage(b, new Rectangle(0, 0, final.Width, final.Height), new Rectangle(0, 0, b.Width, working_height), GraphicsUnit.Pixel);

                                gfinal.Flush();
                                gfinal.Save();

                                if (Directory.Exists(debug))
                                {
                                    final.Save(Path.Combine(debug, @"final-" + c + ".png"), System.Drawing.Imaging.ImageFormat.Png);
                                }
                            }

                            timelapse.First().ReplaceWithBitmap(final);
                            History.Add(timelapse.First());

                            DoActionItem(ActiveShortcutItem, itemIndex + 1);
                        }
                        else
                        {
                            //Must call, so that the action chain is properly ended
                            DoActionItem(null, 0);
                        }
                    } break;
            }
        }

        private static int GetYOffset(Bitmap previous, Bitmap current, int ignore_header, int ignore_footer, int last_offset, int previous_working_height, int n)
        {
            BitmapData previous_bmd = previous.LockBits(new Rectangle(0, ignore_header, previous.Width, previous.Height - ignore_header - ignore_footer), ImageLockMode.ReadOnly, previous.PixelFormat);
            BitmapData current_bmd = current.LockBits(new Rectangle(0, ignore_header, current.Width, current.Height - ignore_header - ignore_footer), ImageLockMode.ReadOnly, current.PixelFormat);

            int current_byte_count = Math.Abs(current_bmd.Stride) * current.Height;
            byte[] current_bytes = new byte[current_byte_count];
            //Marshal.Copy(current_bmd.Scan0, current_bytes, 0, current_byte_count);

            int bpp = current_bmd.Stride / current_bmd.Width;
            int offset = 0;
            long current_ptr = current_bmd.Scan0.ToInt32();//.ToInt64();
            for (int i = 0; i < current_bmd.Height; i++)
            {
                Marshal.Copy(new IntPtr((int)current_bmd.Scan0 + current_bmd.Stride * i), current_bytes, current_bmd.Width * bpp * i, current_bmd.Width * bpp);

            }




            int previous_byte_count = Math.Abs(previous_bmd.Stride) * previous_bmd.Height;
            byte[] previous_bytes = new byte[previous_byte_count];
            //Marshal.Copy(previous_bmd.Scan0, previous_bytes, 0, previous_byte_count);


            bpp = previous_bmd.Stride / previous_bmd.Width;
            offset = 0;
            long previous_ptr = previous_bmd.Scan0.ToInt64();
            for (int i = 0; i < previous_bmd.Height; i++)
            {
                Marshal.Copy(new IntPtr((int)previous_bmd.Scan0 + previous_bmd.Stride * i), previous_bytes, previous_bmd.Width * bpp * i, previous_bmd.Width * bpp);

            }


            previous.UnlockBits(previous_bmd);
            current.UnlockBits(current_bmd);

            int scan_window_height = 10;
            var score = new Dictionary<int, int>();

            for (int orig_y = previous_bmd.Height - scan_window_height - 1; orig_y >= 0; orig_y--)
            {
                //byte[] previous_window_section = new byte[previous_bmd.Stride * scan_window_height];
                //Array.Copy(previous_bytes, previous_bmd.Stride * orig_y, previous_window_section, 0, previous_window_section.Length);

                score.Add(orig_y, GetScore(previous_bytes.Skip(previous_bmd.Stride * orig_y).Take(previous_bmd.Stride * scan_window_height).ToArray(), current_bytes.Skip(0).Take(current_bmd.Stride * scan_window_height).ToArray(), orig_y));
            }

            //Write(score, n);

            int minscore = int.MaxValue;
            int minscoreindex = -1;

            foreach (var kvp in score)
            {
                if (kvp.Value < minscore)
                {
                    minscoreindex = kvp.Key;
                    minscore = kvp.Value;
                }
            }

            return minscoreindex > 0 ? minscoreindex : previous_bmd.Height;
        }

        private static int GetScore(byte[] current_bytes, byte[] previous_bytes, int orig_y)
        {
            int score = 0;

            for (int i = 0; i < current_bytes.Length; i++)
            {
                score += current_bytes[i] != previous_bytes[i] ? 1 : 0;
            }

            return score;
        }
    }
}
