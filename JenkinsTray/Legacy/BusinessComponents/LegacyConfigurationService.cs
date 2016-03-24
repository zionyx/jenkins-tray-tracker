using System;
using System.Collections.Generic;
using System.Text;
using JenkinsTray.Entities;
using Iesi.Collections.Generic;
using JenkinsTray.Utils.IO;
using System.IO;
using System.Reflection;
using Common.Logging;

namespace JenkinsTray.BusinessComponents
{
    [Obsolete]
    public class LegacyConfigurationService
    {
        static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        const string JENKINS_TRAY_DIRECTORY = "Jenkins Tray";
        const string PROPERTIES_FILE = "jenkins.properties";
        // 15 seconds
        const int DEFAULT_TIME_BETWEEN_UPDATES = 15;

        public Configuration LoadConfiguration()
        {
            string userAppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string userAppDataPath = PathHelper.Combine(userAppDataDir, JENKINS_TRAY_DIRECTORY);
            string userConfigurationFile = PathHelper.Combine(userAppDataPath, PROPERTIES_FILE);

            // create the directory in case it does not exist
            Directory.CreateDirectory(userAppDataPath);

            // read the properties file
            PropertiesFile propertiesFile = PropertiesFile.ReadPropertiesFile(userConfigurationFile);

            // load the servers
            var servers = new HashedSet<Server>();
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

#if DEBUG//FIXME
                server.Credentials = new Credentials("plop", "bam");
#endif

                // keep the server
                servers.Add(server);

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

            var notificationSettings = new NotificationSettings();
            notificationSettings.FailedSoundPath = propertiesFile.GetStringValue("sounds.Failed");
            notificationSettings.FixedSoundPath = propertiesFile.GetStringValue("sounds.Fixed");
            notificationSettings.StillFailingSoundPath = propertiesFile.GetStringValue("sounds.StillFailing");
            notificationSettings.SucceededSoundPath = propertiesFile.GetStringValue("sounds.Succeeded");
            notificationSettings.TreatUnstableAsFailed = propertiesFile.GetBoolValue("sounds.TreatUnstableAsFailed") ?? true;

            var generalSettings = new GeneralSettings();
            generalSettings.RefreshIntervalInSeconds = propertiesFile.GetIntValue("general.RefreshTimeInSeconds", DEFAULT_TIME_BETWEEN_UPDATES);
            generalSettings.UpdateMainWindowIcon = propertiesFile.GetBoolValue("general.UpdateMainWindowIcon", true);
            generalSettings.IntegrateWithClaimPlugin = propertiesFile.GetBoolValue("general.IntegrateWithClaimPlugin", true);

            var res = new Configuration
            {
                Servers = servers,
                NotificationSettings = notificationSettings,
                GeneralSettings = generalSettings
            };
            return res;
        }
    }
}
