using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Common.Logging;
using System.Reflection;
using Jenkins.TrayTracker.Utils;

namespace Jenkins.TrayTracker.Entities
{
    public class CaptionAndMessage
    {
        public BuildTransition BuildTransition { set; get; }
        public string Caption { set; get; }
        public ToolTipIcon Icon { get { return this.BuildTransition.Icon; } }
        public string Message { set; get; }

        public void CleanUp()
        {
            Caption = Message = string.Empty;
            BuildTransition = BuildTransition.Successful;
        }
    }

    public class ProjectActivity
    {
        static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Project project;
        private CaptionAndMessage captionAndMessage;
        public bool HasNewBuild { get; set; }

        public ProjectActivity(Project project)
        {
            this.project = project;
            captionAndMessage = new CaptionAndMessage();
        }

        public bool IsBuilding
        {
            get { return project.Status.IsInProgress; }
        }

        public bool IsQueuing
        {
            get { return project.Queue.InQueue; }
        }

        public bool IsStuck
        {
            get { return !IsBuilding && project.Status.IsStuck; }
        }

        public bool HasStatusChanged
        {
            get { return project.PreviousStatus.Key != project.Status.Key; }
        }

        public bool HasNewBuildStarted
        {
            get { return HasNewBuild || ( HasStatusChanged && IsBuilding ); }
        }

        public bool HasBuildEnded
        {
            get { return HasStatusChanged && !IsBuilding; }
        }

        public bool IsAnotherBuildComplete
        {
            get { return ( HasBuildEnded && HasNewBuild ) || ( !IsBuilding && HasNewBuild ); }
        }

        public bool WasLastBuildSuccessful
        {
            get { return project.StatusValue == BuildStatusEnum.Successful; }
        }

        public bool HasBuildActivity
        {
            get
            {
                return ( HasBuildEnded || HasNewBuildStarted ) && 
                    (project.PreviousStatusValue > BuildStatusEnum.Disabled && project.StatusValue > BuildStatusEnum.Disabled);
            }
        }

#if false // tests
        public void DebugWriteLine()
        {
            Debug.WriteLine("\r\n-- " + project + " activity status --");
            Debug.WriteLine(" = Status = " + project.PreviousStatus.Key + " -> " + project.Status.Key);
            Debug.WriteLineIf(IsBuilding, " - IsBuilding");
            Debug.WriteLineIf(HasNewBuild, " - HasNewBuild");
            Debug.WriteLineIf(HasStatusChanged, " - HasStatusChanged");
            Debug.WriteLineIf(HasNewBuildStarted, " - HasNewBuildStarted");
            Debug.WriteLineIf(HasBuildEnded, " - HasBuildEnded");
            Debug.WriteLineIf(IsAnotherBuildComplete, " - IsAnotherBuildComplete");
            Debug.WriteLineIf(WasLastBuildSuccessful, " - WasLastBuildSuccessful");
            Debug.WriteLineIf(HasBuildActivity, " - HasBuildActivity");
            Debug.WriteLine(" - BuildTransition = " + BuildTransition);
            Debug.WriteLine(" - Time = " + DateTime.Now.ToLongTimeString() + "\r\n");
        }
#endif

        public CaptionAndMessage ActivityDetails
        {
            get
            {
                string Source = string.Empty;
                BuildDetails lastBuild = project.LastBuild;
                BuildCause firstBuildCause = project.LastBuild != null && project.LastBuild.Causes != null ? project.LastBuild.Causes.FirstBuildCause : null;
                BuildCauseEnum buildCauseEnum = firstBuildCause != null ? firstBuildCause.Cause : BuildCauseEnum.Unknown;
                captionAndMessage.CleanUp();

                //  Filters out invalid data and ignores re-Enabled projects
                if (lastBuild != null && firstBuildCause != null && buildCauseEnum != BuildCauseEnum.Unknown)
                {
                    switch (buildCauseEnum)
                    {
                        case BuildCauseEnum.SCM:
                            if (lastBuild.Users.Count() == 1)
                            {
                                Source = string.Format(" (commits by {0})", StringUtils.Join(lastBuild.Users, JenkinsTrayTrackerResources.BuildDetails_UserSeparator));
                            }
                            else
                            {
                                Source = " (recent commits)";
                            }
                            break;
                        case BuildCauseEnum.User:
                            Source = !string.IsNullOrEmpty(firstBuildCause.UserID) ? string.Format(" (triggered by {0})", firstBuildCause.UserID) : " (forced)";
                            break;
                        case BuildCauseEnum.RemoteHost:
                            Source = " (started remotely)";
                            break;
                        case BuildCauseEnum.Timer:
                            Source = " (scheduled)";
                            break;
                        case BuildCauseEnum.UpstreamProject:
                            Source = " (by upstream project)";
                            break;
                    }

                    if (IsBuilding)
                    {
                        captionAndMessage.BuildTransition = BuildTransition.Successful;
                        captionAndMessage.Caption = "Project Name: " + project;
                        captionAndMessage.Message = buildCauseEnum == BuildCauseEnum.User && !string.IsNullOrEmpty(firstBuildCause.UserID) ?
                            string.Format(UserStartsBuild, firstBuildCause.UserID) : string.Format(BuildStarted, Source);
                    }
                    else
                    {
                        captionAndMessage.BuildTransition = BuildTransition.GetBuildTransition(project.StatusValue);
                        captionAndMessage.Caption = project + ": " + captionAndMessage.BuildTransition;
                        /*
                        if (IsAnotherBuildComplete)
                        {
                            captionAndMessage.Message = "The previous build" + Source + " was " + project.Status + "\n";
                        }
                        */

                        switch (project.StatusValue)
                        {
                            case BuildStatusEnum.Aborted:
                                captionAndMessage.Caption = "Project Name: " + project;
                                captionAndMessage.Message = Aborted;
                                break;
                            case BuildStatusEnum.Successful:
                            case BuildStatusEnum.Unstable:
                            case BuildStatusEnum.Failed:
                                {
                                    bool wasSuccessful = project.PreviousStatusValue == BuildStatusEnum.Successful;
                                    bool wasUnstable = project.PreviousStatusValue == BuildStatusEnum.Unstable;
                                    bool wasFailing = project.PreviousStatusValue == BuildStatusEnum.Failed;

                                    bool isSuccessful = project.StatusValue == BuildStatusEnum.Successful;
                                    bool isUnstable = project.StatusValue == BuildStatusEnum.Unstable;
                                    bool isFailing = project.StatusValue == BuildStatusEnum.Failed;

                                    if (wasSuccessful && isUnstable) captionAndMessage.Message = string.Format(Unstable, Source);
                                    else if (wasSuccessful && isFailing) captionAndMessage.Message = string.Format(Broken, Source);
                                    else if (wasSuccessful && isSuccessful) captionAndMessage.Message = StillSuccessful;
                                    else if (wasUnstable && isUnstable)
                                    {
                                        captionAndMessage.Message = string.Format(StillUnstable, Source);
                                    }
                                    else if (wasUnstable && isFailing)
                                    {
                                        if (buildCauseEnum == BuildCauseEnum.SCM)
                                        {
                                            captionAndMessage.Message = string.Format(BreakingChanges, Source);
                                        }
                                        else
                                        {
                                            captionAndMessage.Message = string.Format(Broken, Source);
                                        }
                                    }
                                    else if (wasUnstable && isSuccessful) captionAndMessage.Message = FixedUnstable;
                                    else if (wasFailing && isUnstable) captionAndMessage.Message = string.Format(FixedBrokenButUnstable, Source);
                                    else if (wasFailing && isFailing)
                                    {
                                        if (buildCauseEnum == BuildCauseEnum.SCM)
                                        {
                                            captionAndMessage.Message = string.Format(FailToFix, Source);
                                        }
                                        else
                                        {
                                            captionAndMessage.Message = string.Format(StillFailing, Source);
                                        }
                                    }
                                    else if (wasFailing && isSuccessful) captionAndMessage.Message = FixedBroken;
                                    else if (isUnstable) captionAndMessage.Message = string.Format(Unstable, Source);
                                    else if (isFailing) captionAndMessage.Message = Broken;
                                    else if (isSuccessful) captionAndMessage.Message = string.Format(SuccessfulBuild, Source);
                                    else
                                    {
                                        throw new Exception("Build Activity does not meet all conditions in the logic: " + project + ", status is " + project.Status.ToString());
                                    }
                                }
                                break;
                            default:
                                throw new Exception("Build Activity does not meet all conditions in the logic: " + project + ", status is " + project.Status.ToString());
                        }
                    }
                }
                return captionAndMessage;
            }
        }

        //  Build ended
        private static readonly string Aborted = "Build is aborted";                                    //  * -> Aborted.
        private static readonly string FailToFix = "Recent attempt{0} to fix build failed";             //  Failure -> Failure. Supports Cause.
        private static readonly string StillUnstable = "Build{0} is still unstable!";                   //  Unstable -> Unstable. Without Cause.
        private static readonly string StillSuccessful = "Yet another successful build!";               //  Successful -> Successful. Ignores Cause.
        private static readonly string StillFailing = "Build{0} is still failing!";                     //  Failure -> Failure. NOT SCM change.
        private static readonly string FixedBroken = "Recent commits have fixed the build";             //  Failure -> Success. Ignores Cause.
        private static readonly string FixedUnstable = "Recent commits have stablized the build";       //  Unstable -> Success. Has SCM change.
        private static readonly string BreakingChanges = "Recent commits{0} have failed the build";     //  Unstable -> Failure. Supports Cause.
        private static readonly string Broken = "Build{0} is broken";                                   //  Unstable, Success -> Failure. NOT by SCM change.
        private static readonly string FixedBrokenButUnstable = "Build{0} is fixed but remains unstable";  //  Failure -> Unstable. Supports SCM change.
        private static readonly string Unstable = "Build{0} is unstable";                               //  Success, Unknown -> Unstable. Supports SCM change.
        private static readonly string SuccessfulBuild = "Build{0} completes successfully";             //  * -> Success. Supports Cause.
        //  Build started
        private static readonly string BuildStarted = "Build{0} has started";                           //  * -> IsBuilding. Supports Cause.
        private static readonly string UserStartsBuild = "{0} started a build";                         //  * -> IsBuilding. By User.
    }
}
