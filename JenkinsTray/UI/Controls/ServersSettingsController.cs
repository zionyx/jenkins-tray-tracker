using DevExpress.XtraBars;
using JenkinsTray.BusinessComponents;
using JenkinsTray.Entities;
using Spring.Context.Support;

namespace JenkinsTray.UI.Controls
{
    public class ServersSettingsController
    {
        private readonly ConfigurationService configurationService;
        private readonly JenkinsService jenkinsService;
        private readonly ProjectListControl projectListControl;

        private readonly ServerListControl serverListControl;
        private readonly BarEditItem statusProgressItem;
        private readonly BarStaticItem statusTextItem;

        public ServersSettingsController(ServerListControl serverListControl,
                                         ProjectListControl projectListControl,
                                         BarStaticItem statusTextItem,
                                         BarEditItem statusProgressItem)
        {
            this.serverListControl = serverListControl;
            this.projectListControl = projectListControl;
            this.statusTextItem = statusTextItem;
            this.statusProgressItem = statusProgressItem;

            configurationService = (ConfigurationService) ContextRegistry.GetContext().GetObject("ConfigurationService");
            jenkinsService = (JenkinsService) ContextRegistry.GetContext().GetObject("JenkinsService");
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