using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Common.Logging;
using JenkinsTray.Entities;
using JenkinsTray.Utils.IO;
using Spring.Collections.Generic;

namespace JenkinsTray.BusinessComponents
{
    [Obsolete]
    public class LegacyConfigurationService
    {
        private const string JENKINS_TRAY_DIRECTORY = "Jenkins Tray";
        private const string PROPERTIES_FILE = "jenkins.properties";
        // 15 seconds
        private const int DEFAULT_TIME_BETWEEN_UPDATES = 15;
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Configuration LoadConfiguration()
        {
            var userAppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var userAppDataPath = PathHelper.Combine(userAppDataDir, JENKINS_TRAY_DIRECTORY);
            var userConfigurationFile = PathHelper.Combine(userAppDataPath, PROPERTIES_FILE);

            // create the directory in case it does not exist
            Directory.CreateDirectory(userAppDataPath);

            // read the properties file
            var propertiesFile = PropertiesFile.ReadPropertiesFile(userConfigurationFile);

            // load the servers
            var servers = new HashedSet<Server>();
            var serverMap = new Dictionary<int, Server>();
            var serverCount = propertiesFile.GetGroupCount("servers");
            for (var serverId = 0; serverId < serverCount; serverId++)
            {
                // read the server configuration
                var server = new Server();
                server.Url = propertiesFile.GetGroupRequiredStringValue("servers", serverId, "url");
                server.DisplayName = propertiesFile.GetGroupStringValue("servers", serverId, "displayName");
                server.IgnoreUntrustedCertificate = propertiesFile.GetGroupBoolValue("servers", serverId,
                                                                                     "ignoreUntrustedCertificate", false);

                // credentials
                var username = propertiesFile.GetGroupStringValue("servers", serverId, "username");
                if (username != null)
                {
                    var passwordBase64 = propertiesFile.GetGroupRequiredStringValue("servers", serverId,
                                                                                    "passwordBase64");
                    var password = Encoding.UTF8.GetString(Convert.FromBase64String(passwordBase64));
                    server.Credentials = new Credentials(username, password);
                }

#if DEBUG //FIXME
                server.Credentials = new Credentials("plop", "bam");
#endif

                // keep the server
                servers.Add(server);

                // temporary keep for projects loading
                serverMap.Add(serverId, server);
            }

            // load the projects
            var projectCount = propertiesFile.GetGroupCount("projects");
            for (var projectId = 0; projectId < projectCount; projectId++)
            {
                // read the project configuration
                var serverId = propertiesFile.GetGroupRequiredIntValue("projects", projectId, "server");
                var server = serverMap[serverId];
                var project = new Project();
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
            notificationSettings.TreatUnstableAsFailed = propertiesFile.GetBoolValue("sounds.TreatUnstableAsFailed") ??
                                                         true;

            var generalSettings = new GeneralSettings();
            generalSettings.RefreshIntervalInSeconds = propertiesFile.GetIntValue("general.RefreshTimeInSeconds",
                                                                                  DEFAULT_TIME_BETWEEN_UPDATES);
            generalSettings.UpdateMainWindowIcon = propertiesFile.GetBoolValue("general.UpdateMainWindowIcon", true);
            generalSettings.IntegrateWithClaimPlugin = propertiesFile.GetBoolValue("general.IntegrateWithClaimPlugin",
                                                                                   true);

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