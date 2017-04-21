using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Common.Logging;
using JenkinsTray.Entities;
using JenkinsTray.Utils.IO;
using Newtonsoft.Json;
using Spring.Collections.Generic;

namespace JenkinsTray.BusinessComponents
{
    public class ConfigurationService
    {
        public delegate void ConfigurationUpdatedHandler();

        private const string JENKINS_TRAY_DIRECTORY = "Jenkins Tray";
        private const string CONFIGURATION_FILE = "jenkins.configuration";

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Configuration configuration;

        private string userConfigurationFile;

        public Spring.Collections.Generic.ISet<Server> Servers
        {
            get { return configuration.Servers; }
        }

        public NotificationSettings NotificationSettings
        {
            get { return configuration.NotificationSettings; }
        }

        public GeneralSettings GeneralSettings
        {
            get { return configuration.GeneralSettings; }
        }

        public event ConfigurationUpdatedHandler ConfigurationUpdated;

        public void Initialize()
        {
            var userAppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var userAppDataPath = PathHelper.Combine(userAppDataDir, JENKINS_TRAY_DIRECTORY);
            userConfigurationFile = PathHelper.Combine(userAppDataPath, CONFIGURATION_FILE);

            // create the directory in case it does not exist
            Directory.CreateDirectory(userAppDataPath);

            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            if (File.Exists(userConfigurationFile))
            {
                // read the JSON file
                var streamReader = File.OpenText(userConfigurationFile);
                var serializer = new JsonSerializer();
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    configuration = serializer.Deserialize<Configuration>(jsonReader);
                }

                // link back projects to their server
                foreach (var server in configuration.Servers)
                {
                    foreach (var project in server.Projects)
                        project.Server = server;
                }
            }
            else
            {
                // read the legacy properties file
                var legacyReader = new LegacyConfigurationService();
                configuration = legacyReader.LoadConfiguration();
            }
        }

        //  Save settings on application exit.
        public void SaveConfiguration()
        {
            var streamWriter = new StreamWriter(userConfigurationFile);
            var serializer = new JsonSerializer();
            using (var jsonWriter = new JsonTextWriter(streamWriter))
            {
                jsonWriter.Formatting = Formatting.Indented;
                serializer.Serialize(jsonWriter, configuration);
            }

            if (ConfigurationUpdated != null)
                ConfigurationUpdated();
        }

        public Server AddServer(string url, string displayName, string username, string password,
                                bool ignoreUntrustedCertificate)
        {
            var server = new Server();
            BindData(server, url, displayName, username, password, ignoreUntrustedCertificate);
            Servers.Add(server);
            SaveConfiguration();
            return server;
        }

        public void UpdateServer(Server server, string url, string displayName, string username, string password,
                                 bool ignoreUntrustedCertificate)
        {
            // note: we need to remove and re-add the server because its hash-code might change
            var oldServerUrl = server.Url;
            Servers.Remove(server);
            BindData(server, url, displayName, username, password, ignoreUntrustedCertificate);

            //  Update all projects with new server url
            if (server.Url.ToUpper().CompareTo(oldServerUrl.ToUpper()) != 0)
            {
                logger.Info("Server Url updated: " + oldServerUrl + " -> " + server.Url);
                foreach (var project in server.Projects)
                {
                    var updatedUrl = project.Url.Replace(oldServerUrl, server.Url);
                    logger.Info("Project Url updated: " + project.Url + " -> " + updatedUrl);
                    project.Url = updatedUrl;
                }
            }
            Servers.Add(server);
            SaveConfiguration();
        }

        private void BindData(Server server, string url, string displayName, string username, string password,
                              bool ignoreUntrustedCertificate)
        {
            server.Url = url;
            server.DisplayName = displayName;
            server.IgnoreUntrustedCertificate = ignoreUntrustedCertificate;
            if (string.IsNullOrEmpty(username) == false)
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
            foreach (var project in projects)
                DoAddProject(project);
            SaveConfiguration();
        }

        private void DoAddProject(Project project)
        {
            var server = project.Server;
            server.Projects.Add(project);
        }

        public void RemoveProject(Project project)
        {
            DoRemoveProject(project);
            SaveConfiguration();
        }

        public void RemoveProjects(IList<Project> projects)
        {
            foreach (var project in projects)
                DoRemoveProject(project);
            SaveConfiguration();
        }

        private void DoRemoveProject(Project project)
        {
            var server = project.Server;
            server.Projects.Remove(project);
        }

        public IDictionary<Server, Spring.Collections.Generic.ISet<Project>> GetProjects()
        {
            var res = new Dictionary<Server, Spring.Collections.Generic.ISet<Project>>();
            foreach (var server in Servers)
            {
                var projects = new HashedSet<Project>();
                foreach (var project in server.Projects)
                    projects.Add(project);
                res[server] = projects;
            }
            return res;
        }

        public string GetSoundPath(string status)
        {
            var prop = NotificationSettings.GetType().GetProperty(status + "SoundPath");
            var res = (string) prop.GetValue(NotificationSettings, null);
            return res;
        }

        public void SetSoundPath(string status, string path)
        {
            var prop = NotificationSettings.GetType().GetProperty(status + "SoundPath");
            var obj = prop.GetValue(NotificationSettings, null);

            //  obj == null, is to allow NOTHING to be set to the status.
            if (obj == null || obj.ToString().CompareTo(path) != 0)
            {
                prop.SetValue(NotificationSettings, path, null);
                SaveConfiguration();
            }
        }

        public bool IsTreadUnstableAsFailed()
        {
            return NotificationSettings.TreatUnstableAsFailed;
        }

        public bool IsSoundNotificationsEnabled()
        {
            return NotificationSettings.SoundNotifications;
        }

        public bool IsBalloonNotificationsEnabled()
        {
            return NotificationSettings.BalloonNotifications;
        }

        public void SetTreadUnstableAsFailed(bool value)
        {
            if (NotificationSettings.TreatUnstableAsFailed != value)
            {
                NotificationSettings.TreatUnstableAsFailed = value;
                SaveConfiguration();
            }
        }

        public void SetRefreshIntervalInSeconds(int value)
        {
            if (GeneralSettings.RefreshIntervalInSeconds != value)
            {
                GeneralSettings.RefreshIntervalInSeconds = value;
                SaveConfiguration();
            }
        }

        public void SetUpdateMainWindowIcon(bool value)
        {
            if (GeneralSettings.UpdateMainWindowIcon != value)
            {
                GeneralSettings.UpdateMainWindowIcon = value;
                SaveConfiguration();
            }
        }

        public void SetIntegrateWithClaimPlugin(bool value)
        {
            if (GeneralSettings.IntegrateWithClaimPlugin != value)
            {
                GeneralSettings.IntegrateWithClaimPlugin = value;
                SaveConfiguration();
            }
        }

        public void SetShowProjectDisplayName(bool value)
        {
            if (GeneralSettings.ShowProjectDisplayNameInMainUI != value)
            {
                GeneralSettings.ShowProjectDisplayNameInMainUI = value;
                SaveConfiguration();
            }
        }

        public void SetCheckForUpdates(bool value)
        {
            if (GeneralSettings.CheckForUpdates != value)
            {
                GeneralSettings.CheckForUpdates = value;
                SaveConfiguration();
            }
        }

        public void SetSoundNotifications(bool value)
        {
            if (NotificationSettings.SoundNotifications != value)
            {
                NotificationSettings.SoundNotifications = value;
                SaveConfiguration();
            }
        }

        public void SetBalloonNotifications(bool value)
        {
            if (NotificationSettings.BalloonNotifications != value)
            {
                NotificationSettings.BalloonNotifications = value;
                SaveConfiguration();
            }
        }

        public void SetShowFolders(bool value)
        {
            if (GeneralSettings.ShowFolders != value)
            {
                GeneralSettings.ShowFolders = value;
                SaveConfiguration();
            }
        }
    }
}