using System;
using System.Collections.Generic;
using System.Text;
using Hudson.TrayTracker.Entities;
using Iesi.Collections.Generic;
using Hudson.TrayTracker.Utils.IO;
using System.IO;
using System.Reflection;
using Common.Logging;

namespace Hudson.TrayTracker.BusinessComponents
{
    public class ConfigurationService
    {
        public delegate void ConfigurationUpdatedHandler();
        public event ConfigurationUpdatedHandler ConfigurationUpdated;

        static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        const string HUDSON_TRAY_TRACKER_DIRECTORY = "Hudson Tray Tracker";
        const string PROPERTIES_FILE = "hudson.properties";
        // 15 seconds
        const int DEFAULT_TIME_BETWEEN_UPDATES = 15;

        PropertiesFile propertiesFile;

        public ISet<Server> Servers { get; private set; }
        public NotificationSettings NotificationSettings { get; set; }
        public GeneralSettings GeneralSettings { get; set; }

        public void Initialize()
        {
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            string userAppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string userAppDataPath = PathHelper.Combine(userAppDataDir, HUDSON_TRAY_TRACKER_DIRECTORY);
            string userConfigurationFile = PathHelper.Combine(userAppDataPath, PROPERTIES_FILE);

            // create the directory in case it does not exist
            Directory.CreateDirectory(userAppDataPath);

            // read the properties file
            propertiesFile = PropertiesFile.ReadPropertiesFile(userConfigurationFile);

            // load the servers
            Servers = new HashedSet<Server>();
            var serverMap = new Dictionary<int, Server>();
            int serverCount = propertiesFile.GetGroupCount("servers");
            for (int serverId = 0; serverId < serverCount; serverId++)
            {
                // read the server configuration
                Server server = new Server();
                server.Url = propertiesFile.GetGroupRequiredStringValue("servers", serverId, "url");
                server.DisplayName = propertiesFile.GetGroupStringValue("servers", serverId, "displayName");
                server.IgnoreUntrustedCertificate = propertiesFile.GetGroupBoolValue("servers", serverId, "ignoreUntrustedCertificate", false);

                // credentials
                string username = propertiesFile.GetGroupStringValue("servers", serverId, "username");
                if (username != null)
                {
                    string passwordBase64 = propertiesFile.GetGroupRequiredStringValue("servers", serverId, "passwordBase64");
                    string password = Encoding.UTF8.GetString(Convert.FromBase64String(passwordBase64));
                    server.Credentials = new Credentials(username, password);
                }

                // keep the server
                Servers.Add(server);

                // temporary keep for projects loading
                serverMap.Add(serverId, server);
            }

            // load the projects
            int projectCount = propertiesFile.GetGroupCount("projects");
            for (int projectId = 0; projectId < projectCount; projectId++)
            {
                // read the project configuration
                int serverId = propertiesFile.GetGroupRequiredIntValue("projects", projectId, "server");
                Server server = serverMap[serverId];
                Project project = new Project();
                project.Server = server;
                project.Name = propertiesFile.GetGroupRequiredStringValue("projects", projectId, "name");
                project.Url = propertiesFile.GetGroupRequiredStringValue("projects", projectId, "url");

                // keep the project
                server.Projects.Add(project);
            }

            LoadNotificationSettings();
            LoadGeneralSettings();
        }

        private void LoadGeneralSettings()
        {
            GeneralSettings.RefreshIntervalInSeconds = propertiesFile.GetIntValue("general.RefreshTimeInSeconds", DEFAULT_TIME_BETWEEN_UPDATES);
            GeneralSettings.UpdateMainWindowIcon = propertiesFile.GetBoolValue("general.UpdateMainWindowIcon", true);
            GeneralSettings.IntegrateWithClaimPlugin = propertiesFile.GetBoolValue("general.IntegrateWithClaimPlugin", true);
        }

        private void LoadNotificationSettings()
        {
            NotificationSettings.FailedSoundPath = propertiesFile.GetStringValue("sounds.Failed");
            NotificationSettings.FixedSoundPath = propertiesFile.GetStringValue("sounds.Fixed");
            NotificationSettings.StillFailingSoundPath = propertiesFile.GetStringValue("sounds.StillFailing");
            NotificationSettings.SucceededSoundPath = propertiesFile.GetStringValue("sounds.Succeeded");
            NotificationSettings.TreatUnstableAsFailed = propertiesFile.GetBoolValue("sounds.TreatUnstableAsFailed") ?? true;
        }

        private void SaveConfiguration()
        {
            // clear to remove old values
            propertiesFile.Clear();

            // save the servers
            int serverId = 0;
            foreach (Server server in Servers)
            {
                propertiesFile.SetGroupStringValue("servers", serverId, "url", server.Url);
                propertiesFile.SetGroupStringValue("servers", serverId, "displayName", server.DisplayName);
                propertiesFile.SetGroupBoolValue("servers", serverId, "ignoreUntrustedCertificate", server.IgnoreUntrustedCertificate);
                Credentials credentials = server.Credentials;
                if (credentials != null)
                {
                    propertiesFile.SetGroupStringValue("servers", serverId, "username", credentials.Username);
                    string passwordBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials.Password));
                    propertiesFile.SetGroupStringValue("servers", serverId, "passwordBase64", passwordBase64);
                }
                serverId++;
            }
            if (serverId > 0)
                propertiesFile.SetGroupCount("servers", serverId);

            // save the projects
            serverId = 0;
            int projectId = 0;
            foreach (Server server in Servers)
            {
                foreach (Project project in server.Projects)
                {
                    propertiesFile.SetGroupIntValue("projects", projectId, "server", serverId);
                    propertiesFile.SetGroupStringValue("projects", projectId, "name", project.Name);
                    propertiesFile.SetGroupStringValue("projects", projectId, "url", project.Url);
                    projectId++;
                }
                serverId++;
            }
            if (projectId > 0)
                propertiesFile.SetGroupCount("projects", projectId);

            SaveNotificationSettings();
            SaveGeneralSettings();

            propertiesFile.WriteProperties();

            if (ConfigurationUpdated != null)
                ConfigurationUpdated();
        }

        private void SaveGeneralSettings()
        {
            propertiesFile.SetIntValue("general.RefreshTimeInSeconds", GeneralSettings.RefreshIntervalInSeconds);
            propertiesFile.SetBoolValue("general.UpdateMainWindowIcon", GeneralSettings.UpdateMainWindowIcon);
            propertiesFile.SetBoolValue("general.IntegrateWithClaimPlugin", GeneralSettings.IntegrateWithClaimPlugin);
        }

        private void SaveNotificationSettings()
        {
            propertiesFile["sounds.Failed"] = NotificationSettings.FailedSoundPath;
            propertiesFile["sounds.Fixed"] = NotificationSettings.FixedSoundPath;
            propertiesFile["sounds.StillFailing"] = NotificationSettings.StillFailingSoundPath;
            propertiesFile["sounds.Succeeded"] = NotificationSettings.SucceededSoundPath;
            propertiesFile.SetBoolValue("sounds.TreatUnstableAsFailed", NotificationSettings.TreatUnstableAsFailed);
        }

        public Server AddServer(string url, string displayName, string username, string password, bool ignoreUntrustedCertificate)
        {
            Server server = new Server();
            BindData(server, url, displayName, username, password, ignoreUntrustedCertificate);
            Servers.Add(server);
            SaveConfiguration();
            return server;
        }

        public void UpdateServer(Server server, string url, string displayName, string username, string password, bool ignoreUntrustedCertificate)
        {
            // note: we need remove and re-add the server because its hash-code might change
            Servers.Remove(server);
            BindData(server, url, displayName, username, password, ignoreUntrustedCertificate);
            Servers.Add(server);
            SaveConfiguration();
        }

        private void BindData(Server server, string url, string displayName, string username, string password, bool ignoreUntrustedCertificate)
        {
            server.Url = url;
            server.DisplayName = displayName;
            server.IgnoreUntrustedCertificate = ignoreUntrustedCertificate;
            if (String.IsNullOrEmpty(username) == false)
                server.Credentials = new Credentials(username, password);
            else
                server.Credentials = null;
        }

        public void RemoveServer(Server server)
        {
            Servers.Remove(server);
            SaveConfiguration();
        }

        public void AddProject(Project project)
        {
            DoAddProject(project);
            SaveConfiguration();
        }

        public void AddProjects(IList<Project> projects)
        {
            foreach (Project project in projects)
                DoAddProject(project);
            SaveConfiguration();
        }

        private void DoAddProject(Project project)
        {
            Server server = project.Server;
            server.Projects.Add(project);
        }

        public void RemoveProject(Project project)
        {
            DoRemoveProject(project);
            SaveConfiguration();
        }

        public void RemoveProjects(IList<Project> projects)
        {
            foreach (Project project in projects)
                DoRemoveProject(project);
            SaveConfiguration();
        }

        private void DoRemoveProject(Project project)
        {
            Server server = project.Server;
            server.Projects.Remove(project);
        }

        public IDictionary<Server, ISet<Project>> GetProjects()
        {
            var res = new Dictionary<Server, ISet<Project>>();
            foreach (Server server in Servers)
            {
                var projects = new HashedSet<Project>();
                foreach (Project project in server.Projects)
                    projects.Add(project);
                res[server] = projects;
            }
            return res;
        }

        public string GetSoundPath(string status)
        {
            PropertyInfo prop = NotificationSettings.GetType().GetProperty(status + "SoundPath");
            string res = (string)prop.GetValue(NotificationSettings, null);
            return res;
        }

        public void SetSoundPath(string status, string path)
        {
            PropertyInfo prop = NotificationSettings.GetType().GetProperty(status + "SoundPath");
            prop.SetValue(NotificationSettings, path, null);

            SaveConfiguration();
        }

        public bool IsTreadUnstableAsFailed()
        {
            return NotificationSettings.TreatUnstableAsFailed;
        }

        public void SetTreadUnstableAsFailed(bool value)
        {
            NotificationSettings.TreatUnstableAsFailed = value;
            SaveConfiguration();
        }

        public void SetRefreshIntervalInSeconds(int value)
        {
            GeneralSettings.RefreshIntervalInSeconds = value;
            SaveConfiguration();
        }

        public void SetUpdateMainWindowIcon(bool value)
        {
            GeneralSettings.UpdateMainWindowIcon = value;
            SaveConfiguration();
        }

        public void SetIntegrateWithClaimPlugin(bool value)
        {
            GeneralSettings.IntegrateWithClaimPlugin = value;
            SaveConfiguration();
        }
    }
}
