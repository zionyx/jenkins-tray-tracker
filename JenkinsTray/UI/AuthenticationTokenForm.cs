using System;
using System.Reflection;
using Common.Logging;
using DevExpress.XtraEditors;
using JenkinsTray.Entities;
using Spring.Context.Support;

namespace JenkinsTray.UI
{
    public partial class AuthenticationTokenForm : XtraForm
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public AuthenticationTokenForm()
        {
            InitializeComponent();
        }

        public Project referenceProject { get; set; }

        public static AuthenticationTokenForm Instance
        {
            get
            {
                var instance =
                    (AuthenticationTokenForm) ContextRegistry.GetContext().GetObject("AuthenticationTokenForm");
                return instance;
            }
        }

        public void UpdateValues()
        {
            if (referenceProject != null)
            {
                Text = string.Format(JenkinsTrayResources.AuthenticateToken_ProjectName, referenceProject.Name);
                TokentextBox.Text = referenceProject.AuthenticationToken;
                CausetextBox.Text = referenceProject.CauseText;
            }
        }

        public static void ShowDialogOrFocus(Project project)
        {
            Instance.referenceProject = project;
            Instance.UpdateValues();

            if (Instance.Visible)
                Instance.Focus();
            else
                Instance.ShowDialog();
        }

        private void validateButton_Click(object sender, EventArgs e)
        {
            if (referenceProject != null)
            {
                if (referenceProject.AuthenticationToken != TokentextBox.Text.Trim())
                    referenceProject.AuthenticationToken = TokentextBox.Text.Trim();

                if (referenceProject.CauseText != CausetextBox.Text.Trim())
                    referenceProject.CauseText = CausetextBox.Text.Trim();
            }
        }
    }
}