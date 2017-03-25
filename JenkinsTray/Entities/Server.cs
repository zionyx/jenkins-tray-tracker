using JenkinsTray.Utils.Serialization;
using Newtonsoft.Json;
using Spring.Collections.Generic;

namespace JenkinsTray.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Server
    {
        public Server()
        {
            Projects = new HashedSet<Project>();
        }

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
            get { return string.IsNullOrEmpty(DisplayName) ? Url : DisplayName; }
        }

        public override int GetHashCode()
        {
            return Url.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Server;
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