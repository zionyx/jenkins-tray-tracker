using JenkinsTray.BusinessComponents;
using Newtonsoft.Json;

namespace JenkinsTray.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GeneralSettings
    {
        // 15 seconds
        private const int DEFAULT_TIME_BETWEEN_UPDATES = 15;

        public GeneralSettings()
        {
            RefreshIntervalInSeconds = DEFAULT_TIME_BETWEEN_UPDATES;
            CheckForUpdates = true;
        }

        [JsonProperty("refreshIntervalInSeconds")]
        public int RefreshIntervalInSeconds { get; set; }

        [JsonProperty("updateMainWindowIcon")]
        public bool UpdateMainWindowIcon { get; set; }

        [JsonProperty("integrateWithClaimPlugin")]
        public bool IntegrateWithClaimPlugin { get; set; }

        [JsonProperty("showProjectDisplayNameInMainUI")]
        public bool ShowProjectDisplayNameInMainUI { get; set; }

        [JsonProperty("checkForUpdates")]
        public bool CheckForUpdates { get; set; }

        [JsonProperty("showFolders")]
        public bool ShowFolders { get; set; }
        
        public ApplicationUpdateService ApplicationUpdateService { get; set; }
    }
}