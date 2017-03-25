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
    }
}