using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace JenkinsTray.Entities
{
    public enum BuildStatusEnum
    {
        Unknown,
        Disabled,
        Aborted,
        Successful,
        Indeterminate,
        Unstable,
        Failed,
    }

    [DebuggerDisplay("Status={Value}, InProgress={IsInProgress}, Stuck={IsStuck}")]
    public class BuildStatus
    {
        public static readonly BuildStatus UNKNOWN_BUILD_STATUS = new BuildStatus(BuildStatusEnum.Unknown, false, false);

        public readonly BuildStatusEnum Value;
        public readonly bool IsInProgress;
        public readonly bool IsStuck;

        private static readonly string SUCCESS = "success";
        private static readonly string FAILURE = "failure";
        private static readonly string UNSTABLE = "unstable";
        private static readonly string ABORTED = "aborted";

        public BuildStatus(BuildStatusEnum value, bool isInProgress, bool isStuck)
        {
            this.Value = value;
            this.IsInProgress = isInProgress;
            this.IsStuck = isStuck;
        }

        //  http://javadoc.jenkins-ci.org/hudson/model/Result.html
        public static BuildStatusEnum StringToBuildStatus(string result)
        {
            BuildStatusEnum status = BuildStatusEnum.Unknown;

            result = result.ToLower();
            if (result == SUCCESS)
            {
                status = BuildStatusEnum.Successful;
            }
            else if (result == FAILURE)
            {
                status = BuildStatusEnum.Failed;
            }
            else if (result == UNSTABLE)
            {
                status = BuildStatusEnum.Unstable;
            }
            else if (result == ABORTED)
            {
                status = BuildStatusEnum.Aborted;
            }
            return status;
        }

        public string Key
        {
            get
            {
                string res = Value.ToString();
                if (IsInProgress)
                    res += "_BuildInProgress";
                if (IsStuck)
                    res += "_Stuck";
                return res;
            }
        }

        public override string ToString()
        {
            string toString = null;

            switch (this.Value)
            {
                case BuildStatusEnum.Successful:
                    toString = SUCCESS;
                    break;
                case BuildStatusEnum.Failed:
                    toString = FAILURE;
                    break;
                case BuildStatusEnum.Unstable:
                    toString = UNSTABLE;
                    break;
                case BuildStatusEnum.Aborted:
                    toString = ABORTED;
                    break;
                default:
                    toString = base.ToString();
                    break;
            }
            return toString;
        }
    }

    public static class BuildStatusUtils
    {
        public static bool IsWorse(BuildStatus status, BuildStatus thanStatus)
        {
            bool res = status.Value > thanStatus.Value;
            return res;
        }

        public static BuildStatus DegradeStatus(BuildStatus status)
        {
            return new BuildStatus(status.Value, false, status.IsStuck);
        }

        public static bool IsErrorBuild(BuildStatus status)
        {
            return IsErrorBuild(status.Value);
        }
        public static bool IsErrorBuild(BuildStatusEnum status)
        {
            return status == BuildStatusEnum.Failed;
        }
    }
}
