using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JenkinsTray.BusinessComponents;
using Spring.Context.Support;
using JenkinsTray.Entities;
using DevExpress.XtraBars;

namespace JenkinsTray.UI.Controls
{
    public class ServersSettingsController
    {
        ConfigurationService configurationService;
        JenkinsService jenkinsService;

        ServerListControl serverListControl;
        ProjectListControl projectListControl;
        BarStaticItem statusTextItem;
        BarEditItem statusProgressItem;

        public ServersSettingsController(ServerListControl serverListControl,
            ProjectListControl projectListControl,
            BarStaticItem statusTextItem, 
            BarEditItem statusProgressItem)
        {
            this.serverListControl = serverListControl;
            this.projectListControl = projectListControl;
            this.statusTextItem = statusTextItem;
            this.statusProgressItem = statusProgressItem;

            configurationService = (ConfigurationService)ContextRegistry.GetContext().GetObject("ConfigurationService");
            jenkinsService = (JenkinsService)ContextRegistry.GetContext().GetObject("JenkinsService");
            serverListControl.ConfigurationService = configurationService;
            projectListControl.ConfigurationService = configurationService;
            projectListControl.JenkinsService = jenkinsService;
        }

        public void Initialize()
        {
            serverListControl.Initialize();
            projectListControl.Initialize();
        }

        public void UpdateProjectList(Server server)
        {
            projectListControl.UpdateProjectList(server);
        }

        internal void SetStatus(string status, bool updating)
        {
            statusTextItem.Caption = status;
            statusProgressItem.Visibility = updating ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
    }
}
