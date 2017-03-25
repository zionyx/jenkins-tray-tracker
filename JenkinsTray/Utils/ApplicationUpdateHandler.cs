using System;
using System.Reflection;
using System.Windows.Forms;
using Common.Logging;
using DevExpress.XtraEditors;
using JenkinsTray.BusinessComponents;

namespace JenkinsTray.Utils
{
    internal class ApplicationUpdateHandler
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ApplicationUpdateService ApplicationUpdateService { get; set; }

        public void Initialize()
        {
            ApplicationUpdateService.NewVersionAvailable += applicationUpdateService_NewVersionAvailable;
        }

        private void applicationUpdateService_NewVersionAvailable(Version version, string installerUrl)
        {
            var message = string.Format(JenkinsTrayResources.ApplicationUpdates_NewVersion_Text, version);
            var res = XtraMessageBox.Show(message, JenkinsTrayResources.ApplicationUpdates_Caption,
                                          MessageBoxButtons.YesNo);
            if (res != DialogResult.Yes)
            {
                logger.Info("Update refused by user");
                return;
            }
            logger.Info("Update accepted by user");
            UIUtils.OpenWebPage(installerUrl, logger);
            Application.Exit();
        }
    }
}