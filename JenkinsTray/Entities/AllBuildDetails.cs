using System;
using System.Collections.Generic;
using System.Text;

namespace JenkinsTray.Entities
{
    public class AllBuildDetails
    {
        public BuildStatus Status { get; set; }
        public BuildStatus PreviousStatus { get; set; }
        public BuildDetails LastBuild { get; set; }
        public BuildDetails LastCompletedBuild { get; set; }
        public BuildDetails LastSuccessfulBuild { get; set; }
        public BuildDetails LastFailedBuild { get; set; }
    }
}
