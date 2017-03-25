using System;
using System.Diagnostics;
using System.Windows.Forms;
using Common.Logging;
using DevExpress.XtraEditors;

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

                var errorMessage = string.Format(JenkinsTrayResources.ErrorBoxMessage, ex.Message);
                XtraMessageBox.Show(errorMessage, JenkinsTrayResources.ErrorBoxCaption,
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}