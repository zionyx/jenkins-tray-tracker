using System;
using System.Collections.Generic;
using System.Text;
using Iesi.Collections.Generic;
using Newtonsoft.Json;
using Jenkins.Tray.Utils.Serialization;

namespace Jenkins.Tray.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Server
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("credentials")]
        [JsonConverter(typeof(CredentialsJsonConverter))]
        public Credentials Credentials { get; set; }

        [JsonProperty("ignoreUntrustedCertificate")]
        public bool IgnoreUntrustedCertificate { get; set; }

        [JsonProperty("projects")]
        public ISet<Project> Projects { get; private set; }

        public string DisplayText
        {
            get { return String.IsNullOrEmpty(DisplayName) ? Url : DisplayName; }
        }

        public Server()
        {
            Projects = new HashedSet<Project>();
        }

        public override int GetHashCode()
        {
            return Url.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Server other = obj as Server;
            if (other == null)
                return false;
            return other.Url == Url;
        }

        public override string ToString()
        {
            return Url;
        }
    }
}
