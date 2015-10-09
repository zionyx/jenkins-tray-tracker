using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JenkinsTray.Entities
{
    public class BuildTransition
    {
        //  Build ended
        public static readonly BuildTransition Failed = new BuildTransition("Build failed", ToolTipIcon.Error);
        public static readonly BuildTransition Unstable = new BuildTransition("Build unstable", ToolTipIcon.Warning);
        public static readonly BuildTransition Aborted = new BuildTransition("Build aborted", ToolTipIcon.Warning);
        public static readonly BuildTransition Successful = new BuildTransition("Build successful", ToolTipIcon.Info);

        private readonly string caption;
        private readonly ToolTipIcon icon;

        private BuildTransition(string caption, ToolTipIcon icon)
        {
            this.caption = caption;
            this.icon = icon;
        }

        public ToolTipIcon Icon
        {
            get { return icon; }
        }

        public override string ToString()
        {
            return caption;
        }

        public static BuildTransition GetBuildTransition(BuildStatusEnum buildStatusEnum)
        {
            BuildTransition buildTransition = null;

            switch(buildStatusEnum)
            {
                case BuildStatusEnum.Aborted:
                    buildTransition = BuildTransition.Aborted;
                    break;
                case BuildStatusEnum.Failed:
                    buildTransition = BuildTransition.Failed;
                    break;
                case BuildStatusEnum.Unstable:
                    buildTransition = BuildTransition.Unstable;
                    break;
                case BuildStatusEnum.Successful:
                    buildTransition = BuildTransition.Successful;
                    break;
                default:
                    buildTransition = BuildTransition.Successful;
                    break;
            }
            return buildTransition;
        }
    }
}
