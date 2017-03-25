using System;
using DevExpress.XtraEditors;
using JenkinsTray.BusinessComponents;
using Spring.Context.Support;

namespace JenkinsTray.UI.Controls
{
    public partial class NotificationsSettingsControl : XtraUserControl
    {
        private ConfigurationService configurationService;

        public NotificationsSettingsControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // designer mode
            if (DesignMode)
                return;

            configurationService = (ConfigurationService) ContextRegistry.GetContext().GetObject("ConfigurationService");
            treatUnstableAsFailedCheckBox.Checked = configurationService.IsTreadUnstableAsFailed();
            enableSoundCheckBox.Checked = configurationService.IsSoundNotificationsEnabled();
            enableSoundCheckBox_CheckedChanged(null, null);
            enableBalloonCheckBox.Checked = configurationService.IsBalloonNotificationsEnabled();
        }

        public void InitializeValues()
        {
            notificationSettingsControl1.Initialize();
            notificationSettingsControl2.Initialize();
            notificationSettingsControl3.Initialize();
            notificationSettingsControl4.Initialize();
        }

        public bool SoundNotificationsEnabled()
        {
            return enableSoundCheckBox.Checked;
        }

        private void enableSoundCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            notificationSettingsControl1.Enabled =
                notificationSettingsControl2.Enabled =
                    notificationSettingsControl3.Enabled =
                        notificationSettingsControl4.Enabled =
                            treatUnstableAsFailedCheckBox.Enabled =
                                enableSoundCheckBox.Checked;
            configurationService.SetSoundNotifications(enableSoundCheckBox.Checked);
        }

        public bool TreadUnstableAsFailed()
        {
            return treatUnstableAsFailedCheckBox.Checked;
        }

        public void InvalidateData()
        {
            notificationSettingsControl1.InvalidateData();
            notificationSettingsControl2.InvalidateData();
            notificationSettingsControl3.InvalidateData();
            notificationSettingsControl4.InvalidateData();
        }

        private void treatUnstableAsFailedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configurationService.SetTreadUnstableAsFailed(treatUnstableAsFailedCheckBox.Checked);
        }

        private void enableBalloonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            configurationService.SetBalloonNotifications(enableBalloonCheckBox.Checked);
        }
    }
}