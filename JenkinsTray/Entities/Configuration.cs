using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;
using Newtonsoft.Json;
using Jenkins.Tray.Utils.Serialization;

namespace Jenkins.Tray.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Configuration
    {
        [JsonProperty("servers")]
        public ISet<Server> Servers { get; set; }

        [JsonProperty("notificationSettings")]
        public NotificationSettings NotificationSettings { get; set; }

        [JsonProperty("generalSettings")]
        public GeneralSettings GeneralSettings { get; set; }

        public Configuration()
        {
            Servers = new HashedSet<Server>();
            NotificationSettings = new NotificationSettings();
            GeneralSettings = new GeneralSettings();
        }
    }
}
