using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Common.Logging;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using JenkinsTray.UI;
using JenkinsTray.Utils;
using JenkinsTray.Utils.Logging;
using Spring.Context.Support;
using Squirrel;
using System.Linq;

namespace JenkinsTray
{
    internal static class Program
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Update();

                Application.ThreadException += ThreadExceptionHandler.Application_ThreadException;

                // skinning         
                SkinManager.EnableFormSkins();
                OfficeSkins.Register();
                UserLookAndFeel.Default.ActiveLookAndFeel.SkinName = "Office 2010 Blue";

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ApplicationExit += Application_Exit;
                Application_Prepare();

                // Spring
                ContextRegistry.GetContext();
                MainForm.Instance.Show();
                TrayNotifier.Instance.UpdateNotifierStartup();

                var appContext = new ApplicationContext();
                Application.Run(appContext);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, ex);
                MessageBox.Show(ex.ToString(), "Program exception handler");
            }
        }

        private static void Application_Prepare()
        {
            logger.Info("Log4net ready.");
            logger.Info(Assembly.GetExecutingAssembly().GetName().Name
                        + " v" + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);
            logger.Info(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location));
        }

        private static void Application_Exit(object sender, EventArgs e)
        {
            try
            {
                TrayNotifier.Instance.ConfigurationService.SaveConfiguration();
                TrayNotifier.Instance.Dispose();
            }
            catch (Exception ex)
            {
                logger.Error("Failed disposing tray notifier", ex);
            }

            logger.Info(Assembly.GetExecutingAssembly().GetName().Name
                        + " v" + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion +
                        " Exit");
        }

        private static async void Update()
        {
            try
            {
                using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/zionyx/jenkins-tray"))
                {
                    SquirrelAwareApp.HandleEvents(
                        onInitialInstall: v => OnInitialInstall(mgr.Result),
                        onAppUpdate: v => OnAppUpdate(mgr.Result),
                        onAppUninstall: v => OnAppUninstall(mgr.Result));
                    var updateInfo = await mgr.Result.CheckForUpdate();
                    if (updateInfo == null || !updateInfo.ReleasesToApply.Any())
                    {
                        return;
                    }
                    //this.events.GetEvent<UiMessageEvent>().Publish(new UiMessage($"Downloading new Version: {updateInfo.FutureReleaseEntry.Version}", UiMessage.Severity.Info));
                    var releases = updateInfo.ReleasesToApply;
                    await mgr.Result.DownloadReleases(releases);
                    //this.events.GetEvent<UiMessageEvent>().Publish(new UiMessage("Applying update", UiMessage.Severity.Info));
                    await mgr.Result.ApplyReleases(updateInfo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        private static void OnInitialInstall(UpdateManager mgr)
        {
            string thisExe = System.IO.Path.GetFileName(Assembly.GetExecutingAssembly().Location);
            mgr.CreateShortcutForThisExe();
            mgr.CreateShortcutsForExecutable(thisExe, ShortcutLocation.Desktop, false);
            mgr.CreateShortcutsForExecutable(thisExe, ShortcutLocation.StartMenu, false);
            mgr.CreateShortcutsForExecutable(thisExe, ShortcutLocation.Startup, true);
            mgr.CreateUninstallerRegistryEntry();
        }

        private static void OnAppUpdate(UpdateManager mgr)
        {
            string thisExe = System.IO.Path.GetFileName(Assembly.GetExecutingAssembly().Location);
            mgr.CreateShortcutsForExecutable(thisExe, ShortcutLocation.Desktop, true);
            mgr.CreateShortcutsForExecutable(thisExe, ShortcutLocation.StartMenu, true);
            mgr.CreateShortcutsForExecutable(thisExe, ShortcutLocation.Startup, true);
            mgr.RemoveUninstallerRegistryEntry();
            mgr.CreateUninstallerRegistryEntry();
        }

        private static void OnAppUninstall(UpdateManager mgr)
        {
            string thisExe = System.IO.Path.GetFileName(Assembly.GetExecutingAssembly().Location);
            mgr.RemoveShortcutsForExecutable(thisExe, ShortcutLocation.Desktop);
            mgr.RemoveShortcutsForExecutable(thisExe, ShortcutLocation.StartMenu);
            mgr.RemoveShortcutsForExecutable(thisExe, ShortcutLocation.Startup);
            mgr.RemoveUninstallerRegistryEntry();
        }
    }
}
