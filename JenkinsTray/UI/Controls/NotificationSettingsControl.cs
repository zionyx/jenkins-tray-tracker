using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using JenkinsTray.BusinessComponents;
using Spring.Context.Support;

namespace JenkinsTray.UI.Controls
{
    public partial class NotificationSettingsControl : XtraUserControl
    {
        private ConfigurationService configurationService;

        public NotificationSettingsControl()
        {
            InitializeComponent();
        }

        public string Status { get; set; }

        public string SoundPath
        {
            get { return pathEdit.Text; }
            set
            {
                pathEdit.Text = value;
                testSoundButton.Enabled = string.IsNullOrEmpty(value) == false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // designer mode (workaround: DesignMode is not chained to child controls)
            if (DesignMode || Process.GetCurrentProcess().ProcessName == "devenv")
                return;

            if (configurationService == null)
            {
                configurationService =
                    (ConfigurationService) ContextRegistry.GetContext().GetObject("ConfigurationService");
            }

            statusLabel.Text = JenkinsTrayResources.ResourceManager.GetString("NotificationSettings_" + Status);
        }

        public void Initialize()
        {
            if (configurationService == null)
            {
                configurationService =
                    (ConfigurationService) ContextRegistry.GetContext().GetObject("ConfigurationService");
            }
            SoundPath = configurationService.GetSoundPath(Status);
        }

        private void pathEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Close)
                SetPath(null);
            else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                ChooseFile();
        }

        private void ChooseFile()
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.CheckFileExists = true;
            fileDialog.Filter = "Sound file (*.wav)|*.wav";

            var res = fileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                SetPath(fileDialog.FileName);
            }
        }

        private void SetPath(string path)
        {
            if (configurationService == null)
            {
                configurationService =
                    (ConfigurationService) ContextRegistry.GetContext().GetObject("ConfigurationService");
            }

            configurationService.SetSoundPath(Status, path);
            SoundPath = path;
        }

        private void testSoundButton_Click(object sender, EventArgs e)
        {
            SoundPlayer.PlayFile(SoundPath);
        }

        private void pathEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(pathEdit.Text))
            {
                SetPath(pathEdit.Text);
            }
        }

        public void InvalidateData()
        {
            if (string.IsNullOrEmpty(pathEdit.Text) ||
                !pathEdit.Text.EndsWith(".wav", true, CultureInfo.CurrentCulture) ||
                !File.Exists(pathEdit.Text))
            {
                SetPath(string.Empty);
            }
        }
    }
}