using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Jenkins.TrayTracker.BusinessComponents;
using DevExpress.XtraEditors.Controls;
using Spring.Context.Support;
using System.Diagnostics;

namespace Jenkins.TrayTracker.UI.Controls
{
    public partial class NotificationSettingsControl : DevExpress.XtraEditors.XtraUserControl
    {
        ConfigurationService configurationService;

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

        public NotificationSettingsControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // designer mode (workaround: DesignMode is not chained to child controls)
            if (DesignMode || Process.GetCurrentProcess().ProcessName == "devenv")
                return;

            if (configurationService == null)
            {
                configurationService = (ConfigurationService)ContextRegistry.GetContext().GetObject("ConfigurationService");
            }

            statusLabel.Text = JenkinsTrayTrackerResources.ResourceManager.GetString("NotificationSettings_" + Status);
        }

        public void Initialize()
        {
            if (configurationService == null)
            {
                configurationService = (ConfigurationService)ContextRegistry.GetContext().GetObject("ConfigurationService");
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
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.CheckFileExists = true;
            fileDialog.Filter = "Sound file (*.wav)|*.wav";

            DialogResult res = fileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                SetPath(fileDialog.FileName);
            }
        }

        private void SetPath(string path)
        {
            if (configurationService == null)
            {
                configurationService = (ConfigurationService)ContextRegistry.GetContext().GetObject("ConfigurationService");
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
            if (!String.IsNullOrEmpty(pathEdit.Text))
            {
                SetPath(pathEdit.Text);
            }
        }

        public void InvalidateData()
        {
            if (String.IsNullOrEmpty(pathEdit.Text) ||
                !pathEdit.Text.EndsWith(".wav", true, System.Globalization.CultureInfo.CurrentCulture) ||
                !System.IO.File.Exists(pathEdit.Text))
            {
                SetPath(String.Empty);
            }
        }
    }
}
