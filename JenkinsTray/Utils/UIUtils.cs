using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Common.Logging;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace JenkinsTray.Utils
{
    public static class UIUtils
    {
        public static void OpenWebPage(string url, ILog logger)
        {
            try
            {
                Process.Start(url);
            }
            catch (Exception ex)
            {
                logger.Warn("Failed opening page: " + url, ex);

                string errorMessage = String.Format(JenkinsTrayResources.ErrorBoxMessage, ex.Message);
                XtraMessageBox.Show(errorMessage, JenkinsTrayResources.ErrorBoxCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
