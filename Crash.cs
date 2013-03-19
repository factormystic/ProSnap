using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ProSnap
{
    internal static class Crash
    {
        internal static void SubmitCrashReport()
        {
            StringBuilder diag = new StringBuilder();
            diag.AppendLine("Application:\n" + Application.ProductName);
            diag.AppendLine("\nVersion:\n" + Application.ProductVersion);
            diag.AppendLine("\nDate:\n" + DateTime.Now.ToUniversalTime().ToString() + " UTC");
            diag.AppendLine("\nOperating System:\n" + Environment.OSVersion.VersionString);
            diag.AppendLine("\nPlatform:\n" + (IntPtr.Size == 8 ? "64" : "32") + " bit");
            diag.AppendLine("\nRunning As:\n" + (isElevated ? "Administrator" : "Standard User"));
            diag.AppendLine("\nFramework Version:\n" + FrameworkVersion);
            diag.AppendLine("\nUAC Enabled:\n" + UACEnabled);

            ReportListener reporter = Trace.Listeners.Cast<TraceListener>().Where(tl => tl is ReportListener).FirstOrDefault() as ReportListener;
            diag.AppendLine("\nDebug Log:\n" + string.Join("\n", string.Join("\n", reporter.Messages.Select(m => string.Format("<{0}> {1}: {2}", m.Timestamp, m.Category, m.Message)).ToArray())));

            UploadReport("http://factormystic.net/prosnap/feedback/report.php", Crash.Gzip(diag.ToString()));
        }

        private static byte[] Gzip(string data)
        {
            Trace.WriteLine("Staring gzip...", string.Format("Crash.Gzip [{0}]", System.Threading.Thread.CurrentThread.Name));

            using (var ms = new MemoryStream())
            {
                var gzip = new GZipStream(ms, CompressionMode.Compress);
                gzip.Write(UTF8Encoding.UTF8.GetBytes(data), 0, UTF8Encoding.UTF8.GetBytes(data).Length);
                gzip.Close();

                Trace.WriteLine("Done.", string.Format("Crash.Gzip [{0}]", System.Threading.Thread.CurrentThread.Name));
                return ms.ToArray();
            }
        }

        private static void UploadReport(string url, byte[] data)
        {
            Trace.WriteLine("Starting UploadReport...", string.Format("Crash.UploadReport [{0}]", System.Threading.Thread.CurrentThread.Name));

            string result = string.Empty;
            try
            {
                string report = Path.Combine(Application.LocalUserAppDataPath, "report.txt.gz");
                File.WriteAllBytes(report, data);

                using (var wc = new WebClient())
                {
                    wc.Headers.Add("Content-Type", "binary/octet-stream");
                    result = UTF8Encoding.UTF8.GetString(wc.UploadFile(new Uri(url), report));
                }
            }
            catch (WebException e)
            {
                Trace.WriteLine(string.Format("UploadReport WebException: '{0}'", e.GetBaseException()), string.Format("Crash.UploadReport [{0}]", System.Threading.Thread.CurrentThread.Name));
            }

            Trace.WriteLine(string.Format("UploadReport complete: '{0}'", result), string.Format("Crash.UploadReport [{0}]", System.Threading.Thread.CurrentThread.Name));
        }

        private static bool UACEnabled
        {
            get
            {
                int enabled = 0;

                try
                {
                    object key_value = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", "EnableLUA", 0);

                    if (key_value is int)
                        enabled = (int)key_value;
                    else if (!int.TryParse(key_value.ToString(), out enabled))
                    {
                        Trace.WriteLine("*UACEnabled: TryParse failed on " + key_value);
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine("*UACEnabled Exception: " + e.ToString());
                }

                return enabled == 1;
            }
        }

        private static Version FrameworkVersion
        {
            get
            {
                //based on http://msdn.microsoft.com/en-us/kb/kb00318785.aspx
                if (_framework != null)
                    return _framework;

                try
                {
                    string[] dirs = Directory.GetDirectories(Environment.ExpandEnvironmentVariables(@"%systemroot%\Microsoft.NET\Framework"), "v?.*", SearchOption.TopDirectoryOnly);
                    string latest = dirs[dirs.Length - 1];
                    _framework = new Version(latest.Remove(0, Path.GetDirectoryName(latest).Length + 1).Trim(new char[] { 'v' }));

                    return _framework;
                }
                catch (Exception e)
                {
                    Trace.WriteLine("*FrameworkVersion: File based version detection failed; " + e.GetBaseException().Message);
                    return new Version();
                }
            }
        }
        private static Version _framework;

        private static bool isElevated
        {
            get
            {
                return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}
