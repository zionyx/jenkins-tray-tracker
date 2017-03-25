using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Common.Logging;
using DevExpress.XtraEditors;
using JenkinsTray.Utils;
using Spring.Context.Support;

namespace JenkinsTray.UI
{
    public partial class AboutForm : XtraForm
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public AboutForm()
        {
            InitializeComponent();

            versionLabelControl.Text = string.Format(JenkinsTrayResources.Version_Format,
                                                     FileVersionInfo.GetVersionInfo(
                                                         Assembly.GetExecutingAssembly().Location).FileVersion);
        }

        public static AboutForm Instance
        {
            get
            {
                var instance = (AboutForm) ContextRegistry.GetContext().GetObject("AboutForm");
                return instance;
            }
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var url = ((LinkLabel) sender).Text;
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