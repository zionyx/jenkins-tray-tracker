using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using Spring.Context.Support;
using Jenkins.Tray.Utils;
using Common.Logging;

namespace Jenkins.Tray.UI
{
    public partial class AboutForm : DevExpress.XtraEditors.XtraForm
    {
        static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static AboutForm Instance
        {
            get
            {
                AboutForm instance = (AboutForm)ContextRegistry.GetContext().GetObject("AboutForm");
                return instance;
            }
        }

        public AboutForm()
        {
            InitializeComponent();

            versionLabelControl.Text = string.Format(JenkinsTrayResources.Version_Format, 
                FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion);
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = ((LinkLabel)sender).Text;
            UIUtils.OpenWebPage(url, logger);
        }

        public static void ShowDialogOrFocus()
        {
            if (Instance.Visible)
                Instance.Focus();
            else
                Instance.ShowDialog();
        }
    }
}