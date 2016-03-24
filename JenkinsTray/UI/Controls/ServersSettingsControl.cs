using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace JenkinsTray.UI.Controls
{
    public partial class ServersSettingsControl : DevExpress.XtraEditors.XtraUserControl
    {
        ServersSettingsController controller;

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
