using System;
using System.Collections.Generic;
using System.Text;
using JenkinsTray.Entities;
using Iesi.Collections.Generic;
using JenkinsTray.Utils.IO;
using System.IO;
using System.Reflection;
using Common.Logging;
using Newtonsoft.Json;

namespace JenkinsTray.BusinessComponents
{
    public class ConfigurationService
    {
        public delegate void ConfigurationUpdatedHandler();
        public event ConfigurationUpdatedHandler ConfigurationUpdated;

        static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        const string JENKINS_TRAY_DIRECTORY = "Jenkins Tray";
        const string CONFIGURATION_FILE = "jenkins.configuration";

        string userConfigurationFile;
        Configuration configuration;

        public ISet<Server> Servers { get { return configuration.Servers; } }
        public NotificationSettings NotificationSettings { get { return configuration.NotificationSettings; } }
        public GeneralSettings GeneralSettings { get { return configuration.GeneralSettings; } }

        public void Initialize()
        {
            string userAppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string userAppDataPath = PathHelper.Combine(userAppDataDir, JENKINS_TRAY_DIRECTORY);
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
                foreach (Server server in configuration.Servers)
                {
                    foreach (Project project in server.Projects)
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
            // note: we need to remove and re-add the server because its hash-code might change
            string oldServerUrl = server.Url;
            Servers.Remove(server);
            BindData(server, url, displayName, username, password, ignoreUntrustedCertificate);

            //  Update all projects with new server url
            if (server.Url.ToUpper().CompareTo(oldServerUrl.ToUpper()) != 0)
            {
                logger.Info("Server Url updated: " + oldServerUrl + " -> " + server.Url);
                foreach (Project project in server.Projects)
                {
                    string updatedUrl = project.Url.Replace(oldServerUrl, server.Url);
                    logger.Info("Project Url updated: " + project.Url + " -> " + updatedUrl);
                    project.Url = updatedUrl;
                }
            }
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
            object obj = prop.GetValue(NotificationSettings, null);

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
    }
}
