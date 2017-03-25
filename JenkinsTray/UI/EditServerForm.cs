using System;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JenkinsTray.Entities;

namespace JenkinsTray.UI
{
    public partial class EditServerForm : XtraForm
    {
        public EditServerForm()
        {
            InitializeComponent();
            urlTextBox.TextChanged += delegate { ValidateForm(); };
        }

        public EditServerForm(Server server)
            : this()
        {
            ServerAddress = server.Url;
            ServerName = server.DisplayName;
            IgnoreUntrustedCertificate = server.IgnoreUntrustedCertificate;
            if (server.Credentials != null)
            {
                RequiresAuthentication = true;
                Username = server.Credentials.Username;
                Password = server.Credentials.Password;
            }
        }

        public string ServerAddress
        {
            get { return urlTextBox.Text; }
            set { urlTextBox.Text = value; }
        }

        public string ServerName
        {
            get { return displayNameTextEdit.Text; }
            set { displayNameTextEdit.Text = value; }
        }

        public bool RequiresAuthentication
        {
            get { return authenticationCheckBox.CheckState == CheckState.Checked; }
            set { authenticationCheckBox.CheckState = value ? CheckState.Checked : CheckState.Unchecked; }
        }

        public string Username
        {
            get
            {
                if (RequiresAuthentication == false)
                    return null;
                return usernameTextBox.Text;
            }
            set { usernameTextBox.Text = value; }
        }

        public string Password
        {
            get
            {
                if (RequiresAuthentication == false)
                    return null;
                return passwordTextBox.Text;
            }
            set { passwordTextBox.Text = value; }
        }

        public bool IgnoreUntrustedCertificate
        {
            get { return ignoreUntrustedCertificateCheckBox.CheckState == CheckState.Checked; }
            set { ignoreUntrustedCertificateCheckBox.CheckState = value ? CheckState.Checked : CheckState.Unchecked; }
        }

        protected virtual bool IsUrlValid(string url)
        {
            url = url.Trim();
            var valid = string.IsNullOrEmpty(url) == false && url != "http://" && url != "https://";
            return valid;
        }

        private void ValidateForm()
        {
            var url = urlTextBox.Text;
            var nameValid = IsUrlValid(url);

            validateButton.Enabled = nameValid;
        }

        private void authenticationCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            usernameLabel.Enabled
                = usernameTextBox.Enabled
                    = passwordLabel.Enabled
                        = passwordTextBox.Enabled
                            = RequiresAuthentication;
        }

        private void urlTextBox_Leave(object sender, EventArgs e)
        {
            var url = ServerAddress.Trim();
            if (string.IsNullOrEmpty(url) == false
                && url.StartsWith("http://") == false
                && url.StartsWith("https://") == false)
            {
                ServerAddress = "http://" + url;
            }
            //  Prevents malformed URL
            if (!ServerAddress.EndsWith("/", true, CultureInfo.CurrentCulture))
                ServerAddress += "/";
        }

        private void EditServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            urlTextBox_Leave(null, null);
        }
    }
}