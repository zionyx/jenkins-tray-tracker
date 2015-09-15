using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Jenkins.Tray.BusinessComponents;
using Jenkins.Tray.Entities;
using Jenkins.Tray.Utils.BackgroundProcessing;
using DevExpress.XtraBars;
using Spring.Context.Support;

namespace Jenkins.Tray.UI
{
    public partial class SettingsForm : DevExpress.XtraEditors.XtraForm
    {
        public static SettingsForm Instance
        {
            get
            {
                SettingsForm instance = (SettingsForm)ContextRegistry.GetContext().GetObject("SettingsForm");
                return instance;
            }
        }

        public ConfigurationService ConfigurationService { get; set; }
        public JenkinsService JenkinsService { get; set; }
        private ApplicationUpdateService applicationUpdateService;

        public SettingsForm()
        {
            InitializeComponent();
        }

        public static void ShowDialogOrFocus()
        {
            if (Instance.Visible)
                Instance.Focus();
            else
                Instance.ShowDialog();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            tabControl.SelectedTabPageIndex = 0;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            refreshSpinEdit.Value = ConfigurationService.GeneralSettings.RefreshIntervalInSeconds;
            updateMainWindowIconCheckEdit.Checked = ConfigurationService.GeneralSettings.UpdateMainWindowIcon;
            integrateWithClaimPluginCheckEdit.Checked = ConfigurationService.GeneralSettings.IntegrateWithClaimPlugin;
            checkForUpdatesCheckEdit.Checked = ConfigurationService.GeneralSettings.CheckForUpdates;
            notificationsSettingsControl.InitializeValues();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            int refreshInterval = (int)refreshSpinEdit.Value;
            ConfigurationService.SetRefreshIntervalInSeconds(refreshInterval);
            ConfigurationService.SetUpdateMainWindowIcon(updateMainWindowIconCheckEdit.Checked);
            ConfigurationService.SetIntegrateWithClaimPlugin(integrateWithClaimPluginCheckEdit.Checked);
            ConfigurationService.SetCheckForUpdates(checkForUpdatesCheckEdit.Checked);
            ConfigurationService.SetTreadUnstableAsFailed(notificationsSettingsControl.TreadUnstableAsFailed());
            ConfigurationService.SetSoundNotifications(notificationsSettingsControl.SoundNotificationsEnabled());
            notificationsSettingsControl.InvalidateData();
        }

        private void checkForUpdatesCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (applicationUpdateService == null)
            {
                applicationUpdateService = (ApplicationUpdateService)ContextRegistry.GetContext().GetObject("ApplicationUpdateService");
            }
            applicationUpdateService.EnableTimer(checkForUpdatesCheckEdit.Checked);
        }
    }
}
