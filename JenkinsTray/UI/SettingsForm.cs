using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JenkinsTray.BusinessComponents;
using Spring.Context.Support;

namespace JenkinsTray.UI
{
    public partial class SettingsForm : XtraForm
    {
        private ApplicationUpdateService applicationUpdateService;

        public SettingsForm()
        {
            InitializeComponent();
        }

        public static SettingsForm Instance
        {
            get
            {
                var instance = (SettingsForm) ContextRegistry.GetContext().GetObject("SettingsForm");
                return instance;
            }
        }

        public ConfigurationService ConfigurationService { get; set; }
        public JenkinsService JenkinsService { get; set; }

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
            showFoldersCheckEdit.Checked = ConfigurationService.GeneralSettings.ShowFolders;
            checkForUpdatesCheckEdit.Checked = ConfigurationService.GeneralSettings.CheckForUpdates;
            notificationsSettingsControl.InitializeValues();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var refreshInterval = (int)refreshSpinEdit.Value;
            ConfigurationService.SetRefreshIntervalInSeconds(refreshInterval);
            ConfigurationService.SetUpdateMainWindowIcon(updateMainWindowIconCheckEdit.Checked);
            ConfigurationService.SetIntegrateWithClaimPlugin(integrateWithClaimPluginCheckEdit.Checked);
            ConfigurationService.SetTreadUnstableAsFailed(notificationsSettingsControl.TreadUnstableAsFailed());
            ConfigurationService.SetSoundNotifications(notificationsSettingsControl.SoundNotificationsEnabled());
            ConfigurationService.SetShowFolders(showFoldersCheckEdit.Checked);
            ConfigurationService.SetCheckForUpdates(checkForUpdatesCheckEdit.Checked);
            notificationsSettingsControl.InvalidateData();
        }

        private void checkForUpdatesCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (applicationUpdateService == null)
            {
                applicationUpdateService =
                    (ApplicationUpdateService)ContextRegistry.GetContext().GetObject("ApplicationUpdateService");
            }
            applicationUpdateService.EnableTimer(checkForUpdatesCheckEdit.Checked);
        }

        private void showFoldersCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            ConfigurationService.SetShowFolders(showFoldersCheckEdit.Checked);
            ConfigurationService.SaveConfiguration();
        }
    }
}