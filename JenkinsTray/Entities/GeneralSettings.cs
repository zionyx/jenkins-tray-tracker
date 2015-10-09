using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using JenkinsTray.BusinessComponents;

namespace JenkinsTray.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GeneralSettings
    {
        // 15 seconds
        const int DEFAULT_TIME_BETWEEN_UPDATES = 15;

        [JsonProperty("refreshIntervalInSeconds")]
        public int RefreshIntervalInSeconds { get; set; }

        [JsonProperty("updateMainWindowIcon")]
        public bool UpdateMainWindowIcon { get; set; }

        [JsonProperty("integrateWithClaimPlugin")]
        public bool IntegrateWithClaimPlugin { get; set; }

        [JsonProperty("checkForUpdates")]
        public bool CheckForUpdates { get; set; }

        public ApplicationUpdateService ApplicationUpdateService { get; set; }

        public GeneralSettings()
        {
            RefreshIntervalInSeconds = DEFAULT_TIME_BETWEEN_UPDATES;
            CheckForUpdates = true;
        }
    }
}
