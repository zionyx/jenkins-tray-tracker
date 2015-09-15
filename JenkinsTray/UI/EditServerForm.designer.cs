namespace Jenkins.Tray.UI
{
    partial class EditServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param label="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditServerForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.displayNameTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.urlTextBox = new DevExpress.XtraEditors.TextEdit();
            this.displayNameLabel = new DevExpress.XtraEditors.LabelControl();
            this.urlLabel = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.validateButton = new DevExpress.XtraEditors.SimpleButton();
            this.cancelButton = new DevExpress.XtraEditors.SimpleButton();
            this.authenticationCheckBox = new DevExpress.XtraEditors.CheckEdit();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.usernameTextBox = new DevExpress.XtraEditors.TextEdit();
            this.passwordTextBox = new DevExpress.XtraEditors.TextEdit();
            this.passwordLabel = new DevExpress.XtraEditors.LabelControl();
            this.usernameLabel = new DevExpress.XtraEditors.LabelControl();
            this.ignoreUntrustedCertificateCheckBox = new DevExpress.XtraEditors.CheckEdit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayNameTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.urlTextBox.Properties)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.authenticationCheckBox.Properties)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usernameTextBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.passwordTextBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ignoreUntrustedCertificateCheckBox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.displayNameTextEdit, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.urlTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.displayNameLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.urlLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.authenticationCheckBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.ignoreUntrustedCertificateCheckBox, 0, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // displayNameTextEdit
            // 
            resources.ApplyResources(this.displayNameTextEdit, "displayNameTextEdit");
            this.displayNameTextEdit.Name = "displayNameTextEdit";
            // 
            // urlTextBox
            // 
            resources.ApplyResources(this.urlTextBox, "urlTextBox");
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Leave += new System.EventHandler(this.urlTextBox_Leave);
            // 
            // displayNameLabel
            // 
            this.displayNameLabel.Appearance.Options.UseTextOptions = true;
            this.displayNameLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            resources.ApplyResources(this.displayNameLabel, "displayNameLabel");
            this.displayNameLabel.Name = "displayNameLabel";
            // 
            // urlLabel
            // 
            this.urlLabel.Appearance.Options.UseTextOptions = true;
            this.urlLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            resources.ApplyResources(this.urlLabel, "urlLabel");
            this.urlLabel.Name = "urlLabel";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.Controls.Add(this.validateButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cancelButton, 2, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // validateButton
            // 
            this.validateButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.validateButton, "validateButton");
            this.validateButton.Name = "validateButton";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            // 
            // authenticationCheckBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.authenticationCheckBox, 2);
            resources.ApplyResources(this.authenticationCheckBox, "authenticationCheckBox");
            this.authenticationCheckBox.Name = "authenticationCheckBox";
            this.authenticationCheckBox.Properties.AutoWidth = true;
            this.authenticationCheckBox.Properties.Caption = resources.GetString("authenticationCheckBox.Properties.Caption");
            this.authenticationCheckBox.CheckStateChanged += new System.EventHandler(this.authenticationCheckBox_CheckStateChanged);
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel3, 2);
            this.tableLayoutPanel3.Controls.Add(this.usernameTextBox, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.passwordTextBox, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.passwordLabel, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.usernameLabel, 1, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // usernameTextBox
            // 
            resources.ApplyResources(this.usernameTextBox, "usernameTextBox");
            this.usernameTextBox.Name = "usernameTextBox";
            // 
            // passwordTextBox
            // 
            resources.ApplyResources(this.passwordTextBox, "passwordTextBox");
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Properties.PasswordChar = '*';
            // 
            // passwordLabel
            // 
            this.passwordLabel.Appearance.Options.UseTextOptions = true;
            this.passwordLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            resources.ApplyResources(this.passwordLabel, "passwordLabel");
            this.passwordLabel.Name = "passwordLabel";
            // 
            // usernameLabel
            // 
            this.usernameLabel.Appearance.Options.UseTextOptions = true;
            this.usernameLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            resources.ApplyResources(this.usernameLabel, "usernameLabel");
            this.usernameLabel.Name = "usernameLabel";
            // 
            // ignoreUntrustedCertificateCheckBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.ignoreUntrustedCertificateCheckBox, 2);
            resources.ApplyResources(this.ignoreUntrustedCertificateCheckBox, "ignoreUntrustedCertificateCheckBox");
            this.ignoreUntrustedCertificateCheckBox.Name = "ignoreUntrustedCertificateCheckBox";
            this.ignoreUntrustedCertificateCheckBox.Properties.AutoWidth = true;
            this.ignoreUntrustedCertificateCheckBox.Properties.Caption = resources.GetString("ignoreUntrustedCertificateCheckBox.Properties.Caption");
            // 
            // EditServerForm
            // 
            this.AcceptButton = this.validateButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditServerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditServerForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayNameTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.urlTextBox.Properties)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.authenticationCheckBox.Properties)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usernameTextBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.passwordTextBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ignoreUntrustedCertificateCheckBox.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.TextEdit urlTextBox;
        private DevExpress.XtraEditors.LabelControl urlLabel;
        private DevExpress.XtraEditors.CheckEdit authenticationCheckBox;
        private DevExpress.XtraEditors.TextEdit usernameTextBox;
        private DevExpress.XtraEditors.TextEdit passwordTextBox;
        private DevExpress.XtraEditors.LabelControl usernameLabel;
        private DevExpress.XtraEditors.LabelControl passwordLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraEditors.SimpleButton validateButton;
        private DevExpress.XtraEditors.SimpleButton cancelButton;
        private DevExpress.XtraEditors.CheckEdit ignoreUntrustedCertificateCheckBox;
        private DevExpress.XtraEditors.TextEdit displayNameTextEdit;
        private DevExpress.XtraEditors.LabelControl displayNameLabel;
    }
}