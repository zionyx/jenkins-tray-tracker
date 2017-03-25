using Newtonsoft.Json;
using Spring.Collections.Generic;

namespace JenkinsTray.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Configuration
    {
        public Configuration()
        {
            Servers = new HashedSet<Server>();
            NotificationSettings = new NotificationSettings();
            GeneralSettings = new GeneralSettings();
        }

        [JsonProperty("servers")]
        public ISet<Server> Servers { get; set; }

        [JsonProperty("notificationSettings")]
        public NotificationSettings NotificationSettings { get; set; }

        [JsonProperty("generalSettings")]
        public GeneralSettings GeneralSettings { get; set; }
    }
}