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

        private BuildTransition(string caption, ToolTipIcon icon)
        {
            this.caption = caption;
            Icon = icon;
        }

        public ToolTipIcon Icon { get; }

        public override string ToString()
        {
            return caption;
        }

        public static BuildTransition GetBuildTransition(BuildStatusEnum buildStatusEnum)
        {
            BuildTransition buildTransition = null;

            switch (buildStatusEnum)
            {
                case BuildStatusEnum.Aborted:
                    buildTransition = Aborted;
                    break;
                case BuildStatusEnum.Failed:
                    buildTransition = Failed;
                    break;
                case BuildStatusEnum.Unstable:
                    buildTransition = Unstable;
                    break;
                case BuildStatusEnum.Successful:
                    buildTransition = Successful;
                    break;
                default:
                    buildTransition = Successful;
                    break;
            }
            return buildTransition;
        }
    }
}