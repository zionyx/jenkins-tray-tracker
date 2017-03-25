using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using JenkinsTray.BusinessComponents;
using JenkinsTray.Entities;
using JenkinsTray.Utils;
using JenkinsTray.Utils.Logging;
using Spring.Context.Support;
using Spring.Collections.Generic;

namespace JenkinsTray.UI
{
    public partial class TrayNotifier : Component
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly int MAX_TOOLTIP_LENGTH = 127;
        public static readonly int BALLOON_TOOLTIP_TIMEOUT = 3000;

        public static TrayNotifier Instance
        {
            get
            {
                var instance = (TrayNotifier) ContextRegistry.GetContext().GetObject("TrayNotifier");
                return instance;
            }
        }

        private BuildStatus lastBuildStatus;

        private readonly IDictionary<Project, AllBuildDetails> lastProjectsBuildDetails =
            new Dictionary<Project, AllBuildDetails>();

        private readonly IDictionary<Project, BuildStatus> acknowledgedStatusByProject =
            new Dictionary<Project, BuildStatus>();

        private IDictionary<string, Icon> iconsByKey;

        public ConfigurationService ConfigurationService { get; set; }
        public JenkinsService JenkinsService { get; set; }
        public ProjectsUpdateService UpdateService { get; set; }
        public NotificationService NotificationService { get; set; }

        public TrayNotifier()
        {
            InitializeComponent();
            LoadIcons();
        }

        public void Initialize()
        {
            ConfigurationService.ConfigurationUpdated += configurationService_ConfigurationUpdated;
            UpdateService.ProjectsUpdated += updateService_ProjectsUpdated;

            Disposed += delegate
                        {
                            ConfigurationService.ConfigurationUpdated -= configurationService_ConfigurationUpdated;
                            UpdateService.ProjectsUpdated -= updateService_ProjectsUpdated;
                        };
        }

        private void configurationService_ConfigurationUpdated()
        {
            UpdateNotifier();
        }

#if false
        private delegate void ProjectsUpdatedDelegate();
        private void updateService_ProjectsUpdated()
        {
            Delegate del = new ProjectsUpdatedDelegate(OnProjectsUpdated);
            MainForm.Instance.BeginInvoke(del);
        }
        private void OnProjectsUpdated()
        {
            UpdateGlobalStatus();
        }
#else
        private void updateService_ProjectsUpdated()
        {
            UpdateNotifier();
        }
#endif

        // FIXME: the framework doesn't fire correctly MouseClick and MouseDoubleClick,
        // so this is deactivated
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            try
            {
                // order the projects by build status
                var projectsByStatus = new Dictionary<BuildStatusEnum, Spring.Collections.Generic.SortedSet<Project>>();
                foreach (var pair in lastProjectsBuildDetails)
                {
                    var status = BuildStatusEnum.Unknown;
                    if (pair.Value != null)
                        status = BuildStatusUtils.DegradeStatus(pair.Value.Status).Value;
                    var projects = new Spring.Collections.Generic.SortedSet<Project>();
                    if (projectsByStatus.TryGetValue(status, out projects) == false)
                    {
                        projects = new Spring.Collections.Generic.SortedSet<Project>();
                        projectsByStatus.Add(status, projects);
                    }
                    projects.Add(pair.Key);
                }

                var text = new StringBuilder();
                string prefix = null;
                foreach (var pair in projectsByStatus)
                {
                    // don't display successful projects unless this is the only status
                    if (pair.Key == BuildStatusEnum.Successful || projectsByStatus.Count == 1)
                        continue;

                    if (prefix != null)
                        text.Append(prefix);
                    var statusText = JenkinsTrayResources.ResourceManager
                                                         .GetString("BuildStatus_" + pair.Key);
                    text.Append(statusText);
                    foreach (var project in pair.Value)
                    {
                        text.Append("\n  - ").Append(project.Name);

                        var lastFailedBuild = project.LastFailedBuild;
                        if (lastFailedBuild != null && lastFailedBuild.Users != null && lastFailedBuild.Users.Count > 0)
                        {
                            var users = StringUtils.Join(lastFailedBuild.Users, ", ");
                            text.Append(" (").Append(users).Append(")");
                        }
                    }
                    prefix = "\n";
                }

                var textToDisplay = text.ToString();
                if (string.IsNullOrEmpty(textToDisplay))
                    textToDisplay = JenkinsTrayResources.DisplayBuildStatus_NoProjects;
                notifyIcon.ShowBalloonTip(BALLOON_TOOLTIP_TIMEOUT, JenkinsTrayResources.DisplayBuildStatus_Caption,
                                          textToDisplay, ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, ex);
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            MainForm.ShowOrFocus();
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.ShowOrFocus();
        }

        private void refreshMenuItem_Click(object sender, EventArgs e)
        {
            UpdateService.UpdateProjects();
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.Instance.Show();
            SettingsForm.ShowDialogOrFocus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.Instance.Exit();
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.Instance.Show();
            AboutForm.ShowDialogOrFocus();
        }

        public void UpdateNotifier()
        {
            try
            {
                DoUpdateNotifier();
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, ex);
                UpdateIcon(BuildStatus.UNKNOWN_BUILD_STATUS);
            }
        }

        public void UpdateNotifierStartup()
        {
            UpdateIcon(BuildStatus.UNKNOWN_BUILD_STATUS);
            notifyIcon.Text = Assembly.GetExecutingAssembly().GetName().Name;
        }

        private void DoUpdateNotifier()
        {
            BuildStatusEnum? worstBuildStatus = null;
            var buildInProgress = false;
            var buildIsStuck = false;
            var errorProjects = new HashedSet<Project>();
            var regressingProjects = new HashedSet<Project>();
            var progressingAndErrorProjects = new HashedSet<Project>();
            var interestingProjects = new HashedSet<Project>();
            var totalProjectCount = 0;

            foreach (var server in ConfigurationService.Servers)
            {
                foreach (var project in server.Projects)
                {
                    totalProjectCount++;

                    var status = GetProjectStatus(project);
                    if (worstBuildStatus == null || status.Value > worstBuildStatus)
                        worstBuildStatus = status.Value;
                    if (status.Value >= BuildStatusEnum.Failed)
                        errorProjects.Add(project);
                    if (status.Value > BuildStatusEnum.Successful)
                        progressingAndErrorProjects.Add(project);
                    if (status.IsInProgress)
                    {
                        buildInProgress = true;
                        progressingAndErrorProjects.Add(project);
                    }
                    if (status.IsStuck)
                        buildIsStuck = true;
                    if (IsRegressing(project))
                        regressingProjects.Add(project);
                    lastProjectsBuildDetails[project] = project.AllBuildDetails;

                    if (project.Activity.HasBuildActivity)
                    {
                        interestingProjects.Add(project);
                    }
                }
            }

            if (worstBuildStatus == null)
                worstBuildStatus = BuildStatusEnum.Unknown;

#if false // tests
            lastBuildStatus++;
            if (lastBuildStatus > BuildStatus.Failed_BuildInProgress)
                lastBuildStatus = 0;
            worstBuildStatus = lastBuildStatus;
            Console.WriteLine("tray:"+lastBuildStatus);
#endif

            var buildStatus = new BuildStatus(worstBuildStatus.Value, buildInProgress, buildIsStuck);

            UpdateIcon(buildStatus);
            UpdateTrayTooltip(progressingAndErrorProjects, totalProjectCount);
            if (ConfigurationService.NotificationSettings.BalloonNotifications)
            {
                UpdateBalloonTip(errorProjects, regressingProjects);
                ShowBallowTip(interestingProjects);
            }

            lastBuildStatus = buildStatus;
        }

        private BuildStatus GetProjectStatus(Project project)
        {
            var status = project.Status;
            var acknowledgedStatus = GetAcknowledgedStatus(project);
            if (project.IsAcknowledged || (acknowledgedStatus != null))
            {
                if (project.IsAcknowledged || (status.Value == acknowledgedStatus.Value))
                    return new BuildStatus(BuildStatusEnum.Successful, false, false);
                if (status.Value != BuildStatusEnum.Unknown && BuildStatusUtils.IsWorse(acknowledgedStatus, status))
                    ClearAcknowledgedStatus(project);
            }
            return status;
        }

        private bool IsRegressing(Project project)
        {
            AllBuildDetails lastBuildDetails;
            if (lastProjectsBuildDetails.TryGetValue(project, out lastBuildDetails) == false
                || lastBuildDetails == null)
                return false;
            var newBuildDetails = project.AllBuildDetails;
            if (newBuildDetails == null)
                return false;

            // moving from unknown/aborted to successful should not be considered as a regression
            if (newBuildDetails.Status.Value <= BuildStatusEnum.Successful)
                return false;

            var res = BuildStatusUtils.IsWorse(newBuildDetails.Status, lastBuildDetails.Status);
            return res;
        }

        private void UpdateTrayTooltip(ICollection<Project> progressingAndErrorProjects, int totalProjectCount)
        {
            var tooltipText = new StringBuilder();
            string prefix = null;

            if (totalProjectCount == 0)
            {
                tooltipText.Append(JenkinsTrayResources.Tooltip_NoProjects);
            }
            else if (progressingAndErrorProjects != null && progressingAndErrorProjects.Count > 0)
            {
                foreach (var project in progressingAndErrorProjects)
                {
                    if (project.IsAcknowledged)
                        continue;
                    lock (acknowledgedStatusByProject)
                    {
                        if (acknowledgedStatusByProject.ContainsKey(project))
                            continue;
                    }
                    if (prefix != null)
                        tooltipText.Append(prefix);
                    var status = GetProjectStatus(project);
                    var buildDetails = project.LastBuild;
                    if (status.IsInProgress && (status.Value == BuildStatusEnum.Failed))
                    {
                        tooltipText.Append(string.Format(JenkinsTrayResources.Tooltip_Failed_And_InProgress,
                                                         project.Name));
                    }
                    else if (status.IsInProgress)
                    {
                        tooltipText.Append(string.Format(JenkinsTrayResources.Tooltip_InProgress, project.Name));
                    }
                    else
                    {
                        tooltipText.Append(string.Format(JenkinsTrayResources.Tooltip_BuildStatus, project.Name,
                                                         status.Value));
                    }
                    prefix = "\n";
                    if (tooltipText.ToString().Length > MAX_TOOLTIP_LENGTH)
                        break;
                }
            }
            else
            {
                tooltipText.Append(JenkinsTrayResources.Tooltip_AllGood);
            }
            prefix = tooltipText.ToString();
            SetNotifyIconText(notifyIcon, prefix);
        }

        public void ShowBallowTip(ICollection<Project> interestingProjects)
        {
            foreach (var project in interestingProjects)
            {
                try
                {
                    var info = project.Activity.ActivityDetails;

                    //  Need a queue so no events are skipped
                    notifyIcon.ShowBalloonTip(BALLOON_TOOLTIP_TIMEOUT, info.Caption, info.Message, info.Icon);
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogError(logger, ex);
                }
            }
        }

        private void UpdateBalloonTip(ICollection<Project> errorProjects, ICollection<Project> regressingProjects)
        {
            if (lastBuildStatus != null && lastBuildStatus.Value < BuildStatusEnum.Failed
                && errorProjects != null && errorProjects.Count > 0)
            {
                var errorProjectsText = new StringBuilder();
                string prefix = null;
                foreach (var project in errorProjects)
                {
                    if (prefix != null)
                        errorProjectsText.Append(prefix);
                    var buildDetails = project.LastFailedBuild;
                    if (buildDetails == null)
                        logger.Warn("No details for the last failed build of project in error: " + project.Url);
                    var users = buildDetails != null ? buildDetails.Users : null;
                    FormatProjectDetails(project.Name, users, errorProjectsText);
                    prefix = "\n";
                }

                notifyIcon.ShowBalloonTip(BALLOON_TOOLTIP_TIMEOUT, JenkinsTrayResources.BuildFailed_Caption,
                                          errorProjectsText.ToString(), ToolTipIcon.Error);
            }
            else if (regressingProjects != null && regressingProjects.Count > 0)
            {
                var regressingProjectsText = new StringBuilder();
                string prefix = null;
                foreach (var project in regressingProjects)
                {
                    if (prefix != null)
                        regressingProjectsText.Append(prefix);
                    var buildDetails = project.AllBuildDetails.LastCompletedBuild;
                    if (buildDetails == null)
                        logger.Warn("No details for the last failed build of project in error: " + project.Url);
                    var users = buildDetails != null ? buildDetails.Users : null;
                    FormatProjectDetails(project.Name, users, regressingProjectsText);
                    prefix = "\n";
                }

                notifyIcon.ShowBalloonTip(10000, JenkinsTrayResources.BuildRegressions_Caption,
                                          regressingProjectsText.ToString(), ToolTipIcon.Warning);
            }
        }

        private void FormatProjectDetails(string projectName, Spring.Collections.Generic.ISet<string> users, StringBuilder builder)
        {
            builder.Append(projectName);

            if (users != null && users.Count > 0)
            {
                var userString = StringUtils.Join(users, ", ");
                builder.Append(" (").Append(userString).Append(")");
            }
        }

        private void UpdateIcon(BuildStatus buildStatus)
        {
            var icon = iconsByKey[buildStatus.Key];
            notifyIcon.Icon = icon;

            // update the main window's icon
            if (ConfigurationService.GeneralSettings.UpdateMainWindowIcon)
                MainForm.Instance.UpdateIcon(icon);
        }

        private void LoadIcons()
        {
            iconsByKey = new Dictionary<string, Icon>();

            foreach (BuildStatusEnum statusValue in Enum.GetValues(typeof(BuildStatusEnum)))
            {
                LoadIcon(statusValue, false, false);
                LoadIcon(statusValue, false, true);
                LoadIcon(statusValue, true, false);
                LoadIcon(statusValue, true, true);
            }
        }

        private void LoadIcon(BuildStatusEnum statusValue, bool isInProgress, bool isStuck)
        {
            var status = new BuildStatus(statusValue, isInProgress, isStuck);
            if (iconsByKey.ContainsKey(status.Key))
                return;

            try
            {
                var resourceName = string.Format("JenkinsTray.Resources.TrayIcons.{0}.ico", status.Key);
                var icon = ResourceImageHelper.CreateIconFromResources(resourceName, GetType().Assembly);
                iconsByKey.Add(status.Key, icon);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(JenkinsTrayResources.FailedLoadingIcons_Text,
                                    JenkinsTrayResources.FailedLoadingIcons_Caption,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelper.LogError(logger, ex);
                throw new Exception("Failed loading icon: " + statusValue, ex);
            }
        }

        private void notifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine(e.Clicks);
        }

        public void AcknowledgedProject()
        {
            UpdateNotifier();
        }

        public void AcknowledgeStatus(Project project, BuildStatus currentStatus)
        {
            lock (acknowledgedStatusByProject)
            {
                acknowledgedStatusByProject[project] = currentStatus;
            }
            UpdateNotifier();
        }

        public void ClearAcknowledgedStatus(Project project)
        {
            lock (acknowledgedStatusByProject)
            {
                acknowledgedStatusByProject.Remove(project);
            }
            UpdateNotifier();
        }

        private BuildStatus GetAcknowledgedStatus(Project project)
        {
            BuildStatus status;
            lock (acknowledgedStatusByProject)
            {
                if (acknowledgedStatusByProject.TryGetValue(project, out status) == false)
                    return null;
            }
            return status;
        }

        public bool IsStatusAcknowledged(Project project)
        {
            lock (acknowledgedStatusByProject)
            {
                return acknowledgedStatusByProject.ContainsKey(project);
            }
        }

        public static void SetNotifyIconText(NotifyIcon notifyIcon, string text)
        {
            if (text.Length > MAX_TOOLTIP_LENGTH)
            {
                text = text.Remove(MAX_TOOLTIP_LENGTH - 4) + " ...";
            }
            var t = typeof(NotifyIcon);
            var hidden = BindingFlags.NonPublic | BindingFlags.Instance;
            t.GetField("text", hidden).SetValue(notifyIcon, text);
            if ((bool) t.GetField("added", hidden).GetValue(notifyIcon))
                t.GetMethod("UpdateIcon", hidden).Invoke(notifyIcon, new object[] {true});
        }
    }
}