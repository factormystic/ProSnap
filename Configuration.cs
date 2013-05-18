using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProSnap.ActionItems;
using ProSnap.Uploading;

namespace ProSnap
{
    public static class Configuration
    {
        public static string LocalPath
        {
            get
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "factormystic.net", Application.ProductName, Application.ProductVersion);
                
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return path;
            }
        }

        public static string ConfigFilePath
        {
            get
            {
                return Path.Combine(LocalPath, "config.json");
            }
        }

        public static string ApplicationFilePath
        {
            get
            {
                return Assembly.GetEntryAssembly().Location;
            }
        }

        public static string RUninstallerFilePath
        {
            get
            {
                return Path.Combine(Directory.GetParent(LocalPath).FullName, "FMUtils.TaskRUninstaller.exe");
            }
        }

        public static List<ShortcutItem> Shortcuts;

        public static List<IUploadService> UploadServices;

        public static bool IgnoreAllKeyHooks
        {
            get
            {
                return _ignoreallkeyhooks;
            }
            set
            {
                _ignoreallkeyhooks = value;
            }
        }
        static bool _ignoreallkeyhooks = false;

        public static bool isElevated
        {
            get
            {
                return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public static int PreviewDelayTime { get; set; }

        public static FMUtils.WinApi.Helper.WindowLocation PreviewLocation { get; set; }

        public static DockStyle ButtonPanelLocation { get; set; }

        public static bool ShowPreviewWithoutActivation { get; set; }

        public static bool UpdateRestartRequired { get; set; }

        public static string DefaultSaveDirectory { get; set; }
        public static string DefaultFileName { get; set; }

        public static string FileDialogFilter { get; set; }
        public static int DefaultFilterIndex { get; set; }

        static Configuration()
        {
            MigrateFromPriorVersion();
            Read();
        }

        private static void MigrateFromPriorVersion()
        {

            Version CurrentVersion = Version.Parse(Application.ProductVersion);
            var ParentDirectory = System.IO.Directory.GetParent(Configuration.LocalPath);

            try
            {
                //Try and get content out of any prior version data directory
                foreach (var dir in ParentDirectory.EnumerateDirectories())
                {
                    Version v;
                    if (Version.TryParse(dir.Name, out v))
                    {
                        if (CurrentVersion.CompareTo(v) > 0)
                        {
                            foreach (var ss in dir.EnumerateDirectories("screenshots", SearchOption.TopDirectoryOnly))
                            {
                                var From = ss.FullName;
                                var To = Path.Combine(Configuration.LocalPath, "screenshots");

                                if (From != To)
                                    Directory.Move(From, To);
                            }

                            var TempProductPath = Path.Combine(Environment.ExpandEnvironmentVariables("%temp%"), Application.ProductName);

                            //Directory.Move requires the parent of the last directory part exist, which in this case is the product name directory
                            if (!Directory.Exists(TempProductPath))
                                Directory.CreateDirectory(TempProductPath);

                            var Destination = Path.Combine(Environment.ExpandEnvironmentVariables("%temp%"), Application.ProductName, dir.Name);
                            if (!Directory.Exists(Destination))
                                Directory.Move(dir.FullName, Destination);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Really, swallow everything here, but log it
                Trace.WriteLine(string.Format("Failed: '{0}'", ex.GetBaseException().Message), string.Format("Configuration.MigrateFromPriorVersion (screenshots) [{0}]", System.Threading.Thread.CurrentThread.Name));
            }

            try
            {
                //Copy FMUtils.TaskRUninstaller.exe and its dependency to somewhere that will exist across versions and when the ClickOnce app is uninstalled
                var From = Path.Combine(Directory.GetParent(Configuration.ApplicationFilePath).FullName, "FMUtils.TaskRUninstaller.exe");
                var To = Path.Combine(ParentDirectory.FullName, "FMUtils.TaskRUninstaller.exe");
                File.Copy(From, To, true);

                var FromDep = Path.Combine(Directory.GetParent(Configuration.ApplicationFilePath).FullName, "Microsoft.Win32.TaskScheduler.dll");
                var ToDep = Path.Combine(ParentDirectory.FullName, "Microsoft.Win32.TaskScheduler.dll");
                File.Copy(FromDep, ToDep, true);
            }
            catch (Exception ex)
            {
                //Really, swallow everything here, but log it
                Trace.WriteLine(string.Format("Failed: '{0}'", ex.GetBaseException().Message), string.Format("Configuration.MigrateFromPriorVersion (FMUtils.TaskRUninstaller) [{0}]", System.Threading.Thread.CurrentThread.Name));
            }
        }

        internal static void Read()
        {
            if (File.Exists(ConfigFilePath))
            {
                //todo: read
            }
            else
            {
                LoadDefaults();
            }
        }

        internal static void Write()
        {
            //todo: write
        }

        public static void LoadDefaults()
        {
            LoadDefaultProperties();
            LoadDefaultShortcuts();
            LoadDefaultUploadServices();
        }

        private static void LoadDefaultProperties()
        {
            PreviewDelayTime = 5;
            PreviewLocation = FMUtils.WinApi.Helper.WindowLocation.LowerRight;
            ButtonPanelLocation = DockStyle.Bottom;
            ShowPreviewWithoutActivation = false;

            DefaultSaveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ProSnap Screenshots");
            DefaultFileName = @":yyyy:MM:dd_:hh:mm:ss:tt :w.png";

            FileDialogFilter = @"PNG File (*.png)|*.png|Bitmap (*.bmp)|*.bmp|JPEG File (*.jpg)|*.jpg; *.jpeg|GIF File (*.gif)|*.gif|All files (*.*)|*.*";
            DefaultFilterIndex = 0;
        }

        private static void LoadDefaultShortcuts()
        {
            Shortcuts = new List<ShortcutItem>();

            Shortcuts.Add(new ShortcutItem(true, false, new KeyCombo(Keys.PrintScreen), new ProgramActionChain("Create Foreground Window Screenshot")
            {
                ActionItems = { 
                   ActionTypes.TakeForegroundScreenshot.ToInstance(),
                   ActionTypes.ApplyEdits.ToInstance(),
                   ActionTypes.ShowPreview.ToInstance(),
                }
            }));

            Shortcuts.Add(new ShortcutItem(true, false, new KeyCombo(Keys.PrintScreen, alt: true), new ProgramActionChain("Create Region Screenshot")
            {
                ActionItems = { 
                   ActionTypes.TakeRegionScreenshot.ToInstance(),
                   ActionTypes.ShowPreview.ToInstance(),
                }
            }));

            var OpenInBrowser = ActionTypes.Run.ToInstance() as RunAction;
            OpenInBrowser.ApplicationPath = "cmd.exe";
            OpenInBrowser.Parameters = "/c start \"\" \":url?DELETE_URL=:delete\"";

            var CopyImageUrl =  ActionTypes.Clipboard.ToInstance() as ClipboardAction;
            CopyImageUrl.Content = ":url";

            Shortcuts.Add(new ShortcutItem(true, false, new KeyCombo(Keys.PrintScreen, ctrl: true, shift: true), new ProgramActionChain("Create & Upload Screenshot")
            {
                ActionItems = { 
                   ActionTypes.TakeForegroundScreenshot.ToInstance(),
                   ActionTypes.ApplyEdits.ToInstance(),
                   ActionTypes.Upload.ToInstance(),
                   CopyImageUrl,
                   OpenInBrowser,
                }
            }));

            Shortcuts.Add(new ShortcutItem(true, false, new KeyCombo(Keys.Scroll), new ProgramActionChain("Begin Scrolling Screenshot")
            {
                ActionItems = {
                    ActionTypes.BeginScrollingScreenshot.ToInstance()
                }
            }));

            Shortcuts.Add(new ShortcutItem(true, false, new KeyCombo(Keys.PageDown), new ProgramActionChain("Continue Scrolling Screenshot")
            {
                ActionItems = {
                        ActionTypes.ContinueScrollingScreenshot.ToInstance()
                    }
            }));

            Shortcuts.Add(new ShortcutItem(true, false, new KeyCombo(Keys.End), new ProgramActionChain("End Scrolling Screenshot")
            {
                ActionItems = {
                        ActionTypes.EndScrollingScreenshot.ToInstance(),
                        ActionTypes.ShowPreview.ToInstance()
                    }
            }));

            Shortcuts.Add(new ShortcutItem(true, true, new KeyCombo(Keys.Enter), new ProgramActionChain("(Default) Toggle Heart")
            {
                ActionItems = { ActionTypes.Heart.ToInstance() }
            }));

            Shortcuts.Add(new ShortcutItem(true, true, new KeyCombo(Keys.S, ctrl: true), new ProgramActionChain("(Default) Prompt to Save")
            {
                ActionItems = { 
                    new SaveAction() {
                        FilePath = Configuration.DefaultFileName,
                        Prompt = true
                    } }
            }));

            Shortcuts.Add(new ShortcutItem(true, true, new KeyCombo(Keys.Up, ctrl: true), new ProgramActionChain("(Default) Upload")
            {
                ActionItems = { ActionTypes.Upload.ToInstance() }
            }));

            Shortcuts.Add(new ShortcutItem(true, true, new KeyCombo(Keys.Down, ctrl: true), new ProgramActionChain("(Default) Edit in default editor")
            {
                ActionItems = {
                    new RunAction() {
                        Mode = RunAction.Modes.ShellVerb,
                        ShellVerb = "edit",
                        Parameters = "\":file\""
                    }
                }
            }));

            Shortcuts.Add(new ShortcutItem(true, true, new KeyCombo(Keys.Delete), new ProgramActionChain("(Default) Delete")
            {
                ActionItems = { ActionTypes.Delete.ToInstance() }
            }));

            Shortcuts.Add(new ShortcutItem(true, true, new KeyCombo(Keys.Escape), new ProgramActionChain("(Default) Hide Preview")
            {
                ActionItems = { ActionTypes.HidePreview.ToInstance() }
            }));
        }

        private static void LoadDefaultUploadServices()
        {
            UploadServices = new List<IUploadService>();
            UploadServices.Add(new MultipartFormDataUploadService("Imgur Public")
            {
                isActive = true,
                EndpointUrl = "http://imgur.com/api/upload.xml",
                UploadValues = new NameValueCollection
                    {
                        {"key", "21925b62ac028b00051243ea38965eb9"},
                        {"image", "%i"}
                    },
                ImageLinkXPath = "rsp/original_image",
                DeleteLinkXPath = "rsp/delete_page",
            });
        }

        internal static Task<bool> ToggleStartupTask()
        {
            return Task.Factory.StartNew<bool>(() => FMUtils.TaskRUninstaller.Helper.Toggle(Configuration.RUninstallerFilePath, "ProSnap", Configuration.ApplicationFilePath));
        }

        internal static Task<bool> GetStartupTaskStatus()
        {
            return Task.Factory.StartNew<bool>(() => FMUtils.TaskRUninstaller.Helper.IsTaskInstalled("ProSnap"));
        }
    }
}
