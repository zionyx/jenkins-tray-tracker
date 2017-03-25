using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using JenkinsTray.BusinessComponents;
using JenkinsTray.Entities;

namespace JenkinsTray.UI.Controls
{
    public partial class ServerListControl : XtraUserControl
    {
        private bool initialized;
        private BindingList<Server> serversDataSource;

        public ServerListControl()
        {
            InitializeComponent();
        }

        public ServersSettingsController Controller { get; set; }
        public ConfigurationService ConfigurationService { get; set; }

        public void Initialize()
        {
            serversDataSource = new BindingList<Server>();
            foreach (var server in ConfigurationService.Servers)
                serversDataSource.Add(server);
            serversGridControl.DataSource = serversDataSource;

            initialized = true;

            serversGridView_FocusedRowChanged(null, null);
        }

        private void addServerButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            var namingForm = new EditServerForm();
            if (namingForm.ShowDialog() != DialogResult.OK)
                return;

            var server = ConfigurationService.AddServer(
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
            var server = GetSelectedServer();
            if (server == null)
                return;

            var namingForm = new EditServerForm(server);
            if (namingForm.ShowDialog() != DialogResult.OK)
                return;

            ConfigurationService.UpdateServer(server,
                                              namingForm.ServerAddress, namingForm.ServerName,
                                              namingForm.Username, namingForm.Password,
                                              namingForm.IgnoreUntrustedCertificate);

            serversGridView.RefreshData();
            Controller.UpdateProjectList(server);
        }

        private void removeServerButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            RemoveSelectedServer();
        }

        private void removeServerMenuItem_Click(object sender, EventArgs e)
        {
            RemoveSelectedServer();
        }

        private void RemoveSelectedServer()
        {
            var server = GetSelectedServer();
            if (server == null)
                return;

            serversDataSource.Remove(server);
            ConfigurationService.RemoveServer(server);
        }

        private void serversGridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (initialized == false)
                return;

            var server = GetSelectedServer();

            // update the toolbar
            editServerButtonItem.Enabled
                = removeServerButtonItem.Enabled
                    = server != null;

            // update the project list
            Controller.UpdateProjectList(server);
        }

        private Server GetSelectedServer()
        {
            var row = serversGridView.GetFocusedRow();
            var server = row as Server;
            return server;
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            var server = GetSelectedServer();
            editServerMenuItem.Enabled
                = removeServerMenuItem.Enabled
                    = server != null;
        }
    }
}