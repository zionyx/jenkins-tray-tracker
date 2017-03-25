using System;
using DevExpress.XtraEditors;

namespace JenkinsTray.UI.Controls
{
    public partial class ServersSettingsControl : XtraUserControl
    {
        private ServersSettingsController controller;

        public ServersSettingsControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // designer mode
            if (DesignMode)
                return;

            controller = new ServersSettingsController(serverListControl, projectListControl,
                                                       statusTextItem, statusProgressItem);
            serverListControl.Controller = controller;
            projectListControl.Controller = controller;
            controller.Initialize();
        }
    }
}