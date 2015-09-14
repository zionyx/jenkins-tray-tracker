using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Iesi.Collections.Generic;

namespace Jenkins.TrayTracker.Entities
{
    [DebuggerDisplay("BuildNumber={Number}, Result={Result}")]
    public class BuildDetails
    {
        public BuildCauses Causes { get; set; }
        public int Number { get; set; }
        public string Url { get; set; }
        public string DisplayName { get; set; }
        public DateTime Time { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public TimeSpan Duration { get; set; }
        public BuildStatusEnum Result { get; set; }
        public ISet<string> Users { get; set; }
        public ClaimDetails ClaimDetails { get; set; }
    }
}
