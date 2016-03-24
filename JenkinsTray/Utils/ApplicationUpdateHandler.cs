using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Common.Logging;
using System.Reflection;
using System.Diagnostics;
using JenkinsTray.BusinessComponents;

namespace JenkinsTray.Utils
{
    class ApplicationUpdateHandler
    {
        static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        ApplicationUpdateService applicationUpdateService;

        public ApplicationUpdateService ApplicationUpdateService
        {
            get { return applicationUpdateService; }
            set { applicationUpdateService = value; }
        }

        public void Initialize()
        {
            applicationUpdateService.NewVersionAvailable += applicationUpdateService_NewVersionAvailable;
        }

        private void applicationUpdateService_NewVersionAvailable(Version version, string installerUrl)
        {
            string message = string.Format(JenkinsTrayResources.ApplicationUpdates_NewVersion_Text, version);
            DialogResult res = XtraMessageBox.Show(message, JenkinsTrayResources.ApplicationUpdates_Caption,
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
