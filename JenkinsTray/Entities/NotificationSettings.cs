using Newtonsoft.Json;
namespace JenkinsTray.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NotificationSettings
    {
        [JsonProperty("soundNotifications")]
        public bool SoundNotifications { get; set; }

        [JsonProperty("failedSoundPath")]
        public string FailedSoundPath { get; set; }

        [JsonProperty("succeededSoundPath")]
        public string SucceededSoundPath { get; set; }

        [JsonProperty("stillFailingSoundPath")]
        public string StillFailingSoundPath { get; set; }

        [JsonProperty("fixedSoundPath")]
        public string FixedSoundPath { get; set; }

        [JsonProperty("treatUnstableAsFailed")]
        public bool TreatUnstableAsFailed { get; set; }
    }
}
