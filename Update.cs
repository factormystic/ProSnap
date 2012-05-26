using System;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace ProSnap
{
    internal static class Update
    {
        internal static void CheckForUpdate(Action action = null)
        {
            Trace.WriteLine("Attempting application update...", string.Format("Update.CheckForUpdate [{0}]", System.Threading.Thread.CurrentThread.Name));

            if (Configuration.UpdateRestartRequired)
            {
                Trace.WriteLine("Restart flag set, no need to check for update.", string.Format("Update.CheckForUpdate [{0}]", System.Threading.Thread.CurrentThread.Name));
                return;
            }

            var bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
                {
                    if (VersionCheck())
                    {
                        UpdateCheckInfo info = null;

                        if (!ApplicationDeployment.IsNetworkDeployed)
                        {
                            Trace.WriteLine("Not running as ClickOnce, update not possible.", string.Format("Update.CheckForUpdate [{0}]", System.Threading.Thread.CurrentThread.Name));
                            return;
                        }

                        Trace.WriteLine("Identified as ClickOnce...", string.Format("Update.CheckForUpdate [{0}]", System.Threading.Thread.CurrentThread.Name));
                        ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                        try
                        {
                            info = ad.CheckForDetailedUpdate();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            Trace.WriteLine(dde, string.Format("Update.DeploymentDownloadException [{0}]", System.Threading.Thread.CurrentThread.Name));
                            return;
                        }
                        catch (InvalidDeploymentException ide)
                        {
                            Trace.WriteLine(ide, string.Format("Update.InvalidDeploymentException [{0}]", System.Threading.Thread.CurrentThread.Name));
                            return;
                        }
                        catch (InvalidOperationException ioe)
                        {
                            Trace.WriteLine(ioe, string.Format("Update.InvalidOperationException [{0}]", System.Threading.Thread.CurrentThread.Name));
                            return;
                        }

                        if (!info.UpdateAvailable)
                        {
                            Trace.WriteLine("No update available.", string.Format("Update.CheckForUpdate [{0}]", System.Threading.Thread.CurrentThread.Name));
                            return;
                        }

                        Trace.WriteLine(string.Format("Update is available, required: '{0}'", info.IsUpdateRequired), string.Format("Update.CheckForUpdate [{0}]", System.Threading.Thread.CurrentThread.Name));

                        try
                        {
                            Trace.WriteLine("Attempting update...", string.Format("Update.CheckForUpdate [{0}]", System.Threading.Thread.CurrentThread.Name));
                            ad.Update();

                            Trace.WriteLine("Done, restart required", string.Format("Update.CheckForUpdate [{0}]", System.Threading.Thread.CurrentThread.Name));
                            Configuration.UpdateRestartRequired = true;
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            Trace.WriteLine(dde, string.Format("Update.DeploymentDownloadException [{0}]", System.Threading.Thread.CurrentThread.Name));
                        }
                    }
                };

            bg.RunWorkerCompleted += (s, e) =>
            {
                Trace.WriteLine("Update check complete.", string.Format("Update.RunWorkerCompleted [{0}]", System.Threading.Thread.CurrentThread.Name));

                if (action != null)
                    action();
            };

            bg.RunWorkerAsync();
        }

        private static bool VersionCheck()
        {
            Trace.WriteLine("Checking to see if we're running out of date...", string.Format("Update.VersionCheck [{0}]", System.Threading.Thread.CurrentThread.Name));

            try
            {
                using (var wc = new WebClient())
                {
                    string result = wc.DownloadString(new Uri(@"http://factormystic.net/prosnap/version?v=" + Application.ProductVersion));

                    if (string.IsNullOrEmpty(result))
                    {
                        Trace.WriteLine("Failed: result null or empty", string.Format("Update.VersionCheck [{0}]", System.Threading.Thread.CurrentThread.Name));
                        return false;
                    }

                    int cr = result.IndexOf('\n');
                    Version Latest = new Version(cr > -1 ? result.Substring(0, cr) : result.Trim());

                    Trace.WriteLine(string.Format("Latest version is '{0}'; Current version is '{1}'", Latest, Application.ProductVersion), string.Format("Update.VersionCheck [{0}]", System.Threading.Thread.CurrentThread.Name));
                    return new Version(Application.ProductVersion).CompareTo(Latest) < 0;
                }
            }
            catch (Exception ex)
            {
                //Really, swallow everything here, but log it
                Trace.WriteLine(string.Format("Failed: '{0}'", ex.GetBaseException().Message), string.Format("Update.VersionCheck [{0}]", System.Threading.Thread.CurrentThread.Name));
                return false;
            }
        }
    }
}
