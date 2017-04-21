using System;
using Newtonsoft.Json;

namespace JenkinsTray.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Project : IComparable<Project>
    {
        public Project()
        {
            Queue = new QueueItem();
            Activity = new ProjectActivity(this);
        }

        public Server Server { get; set; }

        [JsonProperty("folder")]
        public string Folder { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("acknowledged")]
        public bool IsAcknowledged { get; set; }

        [JsonProperty("token")]
        public string AuthenticationToken { get; set; }

        [JsonProperty("causetext")]
        public string CauseText { get; set; }

        public AllBuildDetails AllBuildDetails { get; set; }
        public ProjectActivity Activity { get; set; }
        public QueueItem Queue { get; set; }

        public BuildStatus Status
        {
            get
            {
                // get a copy of the reference to avoid a race condition
                var details = AllBuildDetails;
                if (details == null)
                    return BuildStatus.UNKNOWN_BUILD_STATUS;
                return details.Status;
            }
        }

        public BuildStatusEnum StatusValue
        {
            get { return Status.Value; }
        }

        public BuildStatus PreviousStatus
        {
            set
            {
                if (AllBuildDetails != null)
                {
                    AllBuildDetails.PreviousStatus = value;
                }
            }
            get
            {
                // get a copy of the reference to avoid a race condition
                var details = AllBuildDetails;
                if (details == null)
                    return BuildStatus.UNKNOWN_BUILD_STATUS;
                return details.PreviousStatus;
            }
        }

        public BuildStatusEnum PreviousStatusValue
        {
            get { return PreviousStatus.Value; }
        }

        public BuildDetails LastBuild
        {
            get
            {
                // get a copy of the reference to avoid a race condition
                var details = AllBuildDetails;
                if (details == null)
                    return null;
                return details.LastBuild;
            }
        }

        public BuildDetails LastSuccessfulBuild
        {
            get
            {
                // get a copy of the reference to avoid a race condition
                var details = AllBuildDetails;
                if (details == null)
                    return null;
                return details.LastSuccessfulBuild;
            }
        }

        public BuildDetails LastFailedBuild
        {
            get
            {
                // get a copy of the reference to avoid a race condition
                var details = AllBuildDetails;
                if (details == null)
                    return null;
                return details.LastFailedBuild;
            }
        }

        public int CompareTo(Project other)
        {
            return Name.CompareTo(other.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Project;
            if (other == null)
                return false;
            if (Server == null)
                return false;
            return other.Server.Equals(Server)
                   && other.Name == Name
                   && other.Folder == Folder;
        }

        public override string ToString()
        {
            return Name;
        }

        public class QueueItem
        {
            public bool InQueue { get; set; }
            public long Id { get; set; }
            public DateTime InQueueSince { get; set; }
            public string Why { get; set; }
        }
    }
}