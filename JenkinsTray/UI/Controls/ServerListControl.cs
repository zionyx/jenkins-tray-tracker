using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Jenkins.Tray.BusinessComponents;
using Jenkins.Tray.Entities;
using DevExpress.XtraBars;
using Jenkins.Tray.Utils.BackgroundProcessing;
using Spring.Context.Support;

namespace Jenkins.Tray.UI.Controls
{
    public partial class ServerListControl : DevExpress.XtraEditors.XtraUserControl
    {
        BindingList<Server> serversDataSource;

        bool initialized;

        public ServersSettingsController Controller { get; set; }
        public ConfigurationService ConfigurationService { get; set; }

        public ServerListControl()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            serversDataSource = new BindingList<Server>();
            foreach (Server server in ConfigurationService.Servers)
                serversDataSource.Add(server);
            serversGridControl.DataSource = serversDataSource;

            initialized = true;

            serversGridView_FocusedRowChanged(null, null);
        }

        private void addServerButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditServerForm namingForm = new EditServerForm();
            if (namingForm.ShowDialog() != DialogResult.OK)
                return;

            Server server = ConfigurationService.AddServer(
                namingForm.ServerAddress, namingForm.ServerName,
                namingForm.Username, namingForm.Password,
                namingForm.IgnoreUntrustedCertificate);
            if (server == null)
                return;

            serversDataSource.Add(server);
        }

        private void editServerButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditSelectedServer();
        }
        private void editServerMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedServer();
        }
        private void EditSelectedServer()
        {
            Server server = GetSelectedServer();
            if (server == null)
                return;

            EditServerForm namingForm = new EditServerForm(server);
            if (namingForm.ShowDialog() != DialogResult.OK)
                return;

            ConfigurationService.UpdateServer(server,
                namingForm.ServerAddress, namingForm.ServerName,
                namingForm.Username, namingForm.Password,
                namingForm.IgnoreUntrustedCertificate);

            serversGridView.RefreshData();
            Controller.UpdateProjectList(server);
        }

        private void removeServerButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RemoveSelectedServer();
        }
        private void removeServerMenuItem_Click(object sender, EventArgs e)
        {
            RemoveSelectedServer();
        }
        private void RemoveSelectedServer()
        {
            Server server = GetSelectedServer();
            if (server == null)
                return;

            serversDataSource.Remove(server);
            ConfigurationService.RemoveServer(server);
        }

        private void serversGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (initialized == false)
                return;

            Server server = GetSelectedServer();

            // update the toolbar
            editServerButtonItem.Enabled
                = removeServerButtonItem.Enabled
                = server != null;

            // update the project list
            Controller.UpdateProjectList(server);
        }

        private Server GetSelectedServer()
        {
            object row = serversGridView.GetFocusedRow();
            Server server = row as Server;
            return server;
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            Server server = GetSelectedServer();
            editServerMenuItem.Enabled
                = removeServerMenuItem.Enabled
                = (server != null);
        }
    }
}
