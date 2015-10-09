using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JenkinsTray.BusinessComponents;
using JenkinsTray.Entities;
using JenkinsTray.Properties;
using DevExpress.XtraGrid.Views.Base;
using System.Drawing.Imaging;
using Common.Logging;
using System.Reflection;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Diagnostics;
using JenkinsTray.Utils.Logging;
using DevExpress.Utils.Controls;
using JenkinsTray.Utils;
using DevExpress.XtraGrid.Columns;
using Spring.Context.Support;
using DevExpress.Utils;

namespace JenkinsTray.UI
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static MainForm Instance
        {
            get
            {
                MainForm instance = (MainForm)ContextRegistry.GetContext().GetObject("MainForm");
                return instance;
            }
        }

        BindingList<ProjectWrapper> projectsDataSource;
        bool exiting;
        int lastHoveredDSRowIndex = -1;
        IDictionary<string, byte[]> iconsByKey;
        Font normalMenuItemFont;
        Font mainMenuItemFont;

        public ConfigurationService ConfigurationService { get; set; }
        public JenkinsService JenkinsService { get; set; }
        public ProjectsUpdateService ProjectsUpdateService { get; set; }
        public ApplicationUpdateService ApplicationUpdateService { get; set; }

        public MainForm()
        {
            InitializeComponent();
            normalMenuItemFont = openProjectPageMenuItem.Font;
            mainMenuItemFont = new Font(openProjectPageMenuItem.Font, FontStyle.Bold);
        }

        private void Initialize()
        {
            ConfigurationService.ConfigurationUpdated += configurationService_ConfigurationUpdated;
            ProjectsUpdateService.ProjectsUpdated += updateService_ProjectsUpdated;

            Disposed += delegate
            {
                ConfigurationService.ConfigurationUpdated -= configurationService_ConfigurationUpdated;
                ProjectsUpdateService.ProjectsUpdated -= updateService_ProjectsUpdated;
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Initialize();
            UpdateClaimPluginIntegration();
            LoadIcons();
            LoadProjects();
        }

        void configurationService_ConfigurationUpdated()
        {
            UpdateClaimPluginIntegration();
            LoadProjects();
            if (ConfigurationService.GeneralSettings.UpdateMainWindowIcon == false)
                ResetIcon();
        }

        private delegate void ProjectsUpdatedDelegate();
        private void updateService_ProjectsUpdated()
        {
            Delegate del = new ProjectsUpdatedDelegate(OnProjectsUpdated);
            BeginInvoke(del);
        }
        private void OnProjectsUpdated()
        {
            UpdateProjects();
        }

        private void settingsButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SettingsForm.Instance.ShowDialog();
        }

        private void LoadProjects()
        {
            projectsDataSource = new BindingList<ProjectWrapper>();

            foreach (Server server in ConfigurationService.Servers)
            {
                foreach (Project project in server.Projects)
                {
                    ProjectWrapper wrapper = new ProjectWrapper(project);
                    projectsDataSource.Add(wrapper);
                }
            }

            projectsGridControl.DataSource = projectsDataSource;
            projectsGridView.BestFitColumns();

            UpdateStatusBar();
        }

        private void UpdateProjects()
        {
            projectsGridView.RefreshData();

            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            lastCheckBarStaticItem.Caption = string.Format(JenkinsTrayResources.LastCheck_Format, DateTime.Now);
        }

        private void refreshButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProjectsUpdateService.UpdateProjects();
        }

        private void JenkinsTrayForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exiting == false && e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }

        public void Exit()
        {
            exiting = true;
            Close();
            Application.Exit();
        }

        private void exitButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Exit();
        }

        private void projectsGridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                if (e.Column == statusGridColumn)
                {
                    ProjectWrapper projectWrapper = (ProjectWrapper)projectsDataSource[e.ListSourceRowIndex];
                    byte[] imgBytes = iconsByKey[projectWrapper.Project.Status.Key];
                    e.Value = imgBytes;
                }
            }
        }

        private void LoadIcons()
        {
            iconsByKey = new Dictionary<string, byte[]>();

            foreach (BuildStatusEnum statusValue in Enum.GetValues(typeof(BuildStatusEnum)))
            {
                LoadIcon(statusValue, false, false);
                LoadIcon(statusValue, false, true);
                LoadIcon(statusValue, true, false);
            }
        }

        private void LoadIcon(BuildStatusEnum statusValue, bool isInProgress, bool isStuck)
        {
            BuildStatus status = new BuildStatus(statusValue, isInProgress, isStuck);
            if (iconsByKey.ContainsKey(status.Key))
                return;

            try
            {
                string resourceName = string.Format("JenkinsTray.Resources.StatusIcons.{0}.gif", status.Key);
                Image img = ResourceImageHelper.CreateImageFromResources(resourceName, GetType().Assembly);
                byte[] imgBytes = DevExpress.XtraEditors.Controls.ByteImageConverter.ToByteArray(img, ImageFormat.Gif);
                iconsByKey.Add(status.Key, imgBytes);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(JenkinsTrayResources.FailedLoadingIcons_Text,
                    JenkinsTrayResources.FailedLoadingIcons_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelper.LogError(logger, ex);
                throw new Exception("Failed loading icon: " + status, ex);
            }
        }

        private void aboutButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AboutForm.Instance.ShowDialog();
        }

        private void projectsGridView_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            GridHitInfo ghi = projectsGridView.CalcHitInfo(pt);
            if (ghi.InRowCell)
            {
                int dsRowIndex = projectsGridView.GetDataSourceRowIndex(ghi.RowHandle);
                if (lastHoveredDSRowIndex != dsRowIndex)
                {
                    ProjectWrapper project = projectsDataSource[dsRowIndex];
                    string message = JenkinsTrayResources.ResourceManager
                        .GetString("BuildStatus_" + project.Project.Status.Key);
                    if (project.Project.Status.IsStuck && project.Project.Queue.InQueue)
                    {
                        message += string.Format(JenkinsTrayResources.BuildDetails_InQueue_Since, project.Project.Queue.InQueueSince);
                    }
                    toolTip.SetToolTip(projectsGridControl, message);
                }
                lastHoveredDSRowIndex = dsRowIndex;
            }
            else
            {
                lastHoveredDSRowIndex = -1;
            }
        }

        private void projectsGridView_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs mouseEventArgs = e as DXMouseEventArgs;
            Point pt = new Point(mouseEventArgs.X, mouseEventArgs.Y);
            GridHitInfo hi = projectsGridView.CalcHitInfo(pt);

            if (hi.InRowCell)
            {
                Project project = GetSelectedProject();
                if (project == null)
                    return;
                bool shouldOpenConsolePage = ShouldOpenConsolePage(project);
                if (shouldOpenConsolePage)
                    OpenProjectConsolePage(project);
                else
                    OpenProjectPage(project);
            }
        }

        private bool ShouldOpenConsolePage(Project project)
        {
            if (project == null)
                return false;
            BuildStatus status = project.Status;
            bool res = BuildStatusUtils.IsErrorBuild(status) || status.IsInProgress;
            return res;
        }

        private void OpenSelectedProjectPage()
        {
            Project project = GetSelectedProject();
            OpenProjectPage(project);
        }

        private void OpenProjectPage(Project project)
        {
            if (project == null)
                return;
            UIUtils.OpenWebPage(project.Url, logger);
        }

        private void OpenSelectedProjectConsolePage()
        {
            Project project = GetSelectedProject();
            OpenProjectConsolePage(project);
        }

        private void OpenProjectConsolePage(Project project)
        {
            if (project == null)
                return;
            string url = JenkinsService.GetConsolePage(project);
            UIUtils.OpenWebPage(url, logger);
        }

        private class ProjectWrapper
        {
            public ProjectWrapper(Project project)
            {
                this.Project = project;
            }

            public Project Project { get; private set; }

            public string Server
            {
                get { return Project.Server.DisplayText; }
            }
            public string Name
            {
                get { return Project.Name; }
            }
            public string Url
            {
                get { return Uri.UnescapeDataString(Project.Url); }
            }

            public string buildDetailsStr
            {
                get
                {
                    string details = string.Empty;
                    try
                    {
                        details = FormatBuildDetailsAndSummary();
                    }
                    catch (Exception ex)
                    {
                        LoggingHelper.LogError(logger, ex);
                        XtraMessageBox.Show(string.Format(JenkinsTrayResources.RunBuildFailed_Text, ex.Message),
                            JenkinsTrayResources.RunBuildFailed_Caption);
                    }
                    return details;
                }
            }
            public string lastBuildStr
            {
                get { return FormatBuildDetails(LastBuild); }
            }

            public BuildDetails LastBuild
            {
                get { return Project.LastBuild; }
            }

            public BuildDetails LastSuccessBuild
            {
                get { return Project.LastSuccessfulBuild; }
            }
            public string LastSuccessBuildStr
            {
                get { return FormatBuildDetails(LastSuccessBuild); }
            }
            public BuildDetails LastFailureBuild
            {
                get { return Project.LastFailedBuild; }
            }
            public string LastFailureBuildStr
            {
                get { return FormatBuildDetails(LastFailureBuild); }
            }
            public string LastSuccessUsers
            {
                get { return FormatUsers(Project.LastSuccessfulBuild); }
            }
            public string LastFailureUsers
            {
                get { return FormatUsers(Project.LastFailedBuild); }
            }
            public string ClaimedBy
            {
                get
                {
                    // get a copy of the reference to avoid a race condition
                    var lastFailedBuild = Project.LastFailedBuild;
                    if (lastFailedBuild == null || lastFailedBuild.ClaimDetails == null)
                        return "";
                    return Project.LastFailedBuild.ClaimDetails.User;
                }
            }
            public string ClaimReason
            {
                get
                {
                    // get a copy of the reference to avoid a race condition
                    var lastFailedBuild = Project.LastFailedBuild;
                    if (lastFailedBuild == null || lastFailedBuild.ClaimDetails == null)
                        return "";
                    return Project.LastFailedBuild.ClaimDetails.Reason;
                }
            }

            private string FormatBuildDetailsAndSummary()
            {
                string details = string.Empty;
                string buildCausesSummary = string.Empty;

                // get a copy of the reference to avoid a race condition
                BuildStatus projectStatus = Project.Status;
                BuildDetails lastBuild = Project.LastBuild;

                if (lastBuild != null)
                {
                    // get a copy of the reference to avoid a race condition
                    BuildCauses lastBuildCauses = lastBuild.Causes;

                    if (lastBuildCauses != null)
                    {
                        if (projectStatus.IsInProgress)
                        {
                            TimeSpan progressts = lastBuild.EstimatedDuration;
                            string timeleft = FormatEstimatedDuration(lastBuild);

                            foreach (BuildCause cause in lastBuildCauses.Causes)
                            {
                                if (lastBuildCauses.HasUniqueCauses == false)
                                {
                                    buildCausesSummary = JenkinsTrayResources.BuildDetails_Cause_MultipleSources;
                                    break;
                                }

                                switch (cause.Cause)
                                {
                                    case BuildCauseEnum.SCM:
                                        {
                                            if (lastBuild.Users.Count == 0)
                                            {
                                                buildCausesSummary = JenkinsTrayResources.BuildDetails_Cause_SCM;
                                            }
                                            else if (lastBuild.Users.Count > 1)
                                            {
                                                buildCausesSummary = string.Format(JenkinsTrayResources.BuildDetails_Cause_SCM_Multiple, lastBuild.Users.Count);
                                            }
                                            else
                                            {
                                                buildCausesSummary = string.Format(JenkinsTrayResources.BuildDetails_Cause_SCM_Single, FormatUsers(lastBuild));
                                            }
                                        }
                                        break;
                                    case BuildCauseEnum.User:
                                        {
                                            buildCausesSummary = string.Format(JenkinsTrayResources.BuildDetails_Cause_User, cause.Starter);
                                        }
                                        break;
                                    case BuildCauseEnum.RemoteHost:
                                        {
                                            buildCausesSummary = JenkinsTrayResources.BuildDetails_Cause_RemoteHost;
                                        }
                                        break;
                                    case BuildCauseEnum.Timer:
                                        {
                                            buildCausesSummary = JenkinsTrayResources.BuildDetails_Cause_Timer;
                                        }
                                        break;
                                    case BuildCauseEnum.UpstreamProject:
                                        {
                                            buildCausesSummary = JenkinsTrayResources.BuildDetails_Cause_UpstreamProject;
                                        }
                                        break;
                                }
                            }
                            details = timeleft + buildCausesSummary;
                        }
                        else
                        {
                            if (projectStatus.Value == BuildStatusEnum.Successful)
                            {
                                details = FormatDuration(lastBuild);
                            }
                            else if (projectStatus.Value == BuildStatusEnum.Unstable)
                            {
                                details = projectStatus.Value.ToString() + ".";
                            }
                            else if (projectStatus.Value == BuildStatusEnum.Disabled)
                            {
                                details = string.Format("{0}. ", JenkinsTrayResources.BuildStatus_Disabled);
                            }
                            else
                            {
                                details = projectStatus.Value.ToString() + ". ";
                                if (lastBuild.Users != null && !lastBuild.Users.IsEmpty)
                                {
                                    details += string.Format(JenkinsTrayResources.BuildDetails_BrokenBy, FormatUsers(lastBuild));
                                }
                            }
                        }
                        if (Project.Queue.InQueue)
                        {
                            if (!projectStatus.IsInProgress)
                            {
                                details += string.Format(JenkinsTrayResources.BuildDetails_InQueue_Why, Project.Queue.Why);
                            }
                        }
                        if (projectStatus.IsStuck)
                        {
                            details = "Queued, possibly stuck. " + string.Format(JenkinsTrayResources.BuildDetails_InQueue_Why, Project.Queue.Why);
                        }
                    }
                }

                return details;
            }

            private string FormatBuildDetailsWithDisplayName(BuildDetails details)
            {
                if (details == null)
                    return "-";
                string shortDisplayName = details.DisplayName.Replace(Project.Name, string.Empty).Trim();

                string res = string.Empty;
                if (shortDisplayName.Equals(string.Concat("#", details.Number.ToString())))
                {
                    res = string.Format(JenkinsTrayResources.BuildDetails_Format_NumberDate,
                           details.Number.ToString(), details.Time.ToLocalTime());
                }
                else
                {
                    res = string.Format(JenkinsTrayResources.BuildDetails_Format_DisplayName_NumberDate,
                           shortDisplayName, details.Number.ToString(), details.Time.ToLocalTime());
                }
                return res;
            }

            private string FormatBuildDetails(BuildDetails details)
            {
                if (details == null)
                    return "-";
                string res = string.Format(JenkinsTrayResources.BuildDetails_Format_NumberDate,
                       details.Number, details.Time.ToLocalTime());
                return res;
            }

            private string FormatUsers(BuildDetails details)
            {
                if (details == null)
                    return "-";
                string res = StringUtils.Join(details.Users, JenkinsTrayResources.BuildDetails_UserSeparator);
                return res;
            }

            private string FormatDuration(BuildDetails details)
            {
                if (details == null)
                    return string.Empty;
                string res = string.Empty;
                if (details.Duration.TotalHours > 1)
                {
                    res = string.Format(JenkinsTrayResources.BuildDetails_Duration_HHMM, 
                        details.Duration.Days * 24 + details.Duration.Hours, details.Duration.Minutes);
                }
                else if (details.Duration.TotalMinutes < 1)
                {
                    res = JenkinsTrayResources.BuildDetails_Duration_0M;
                }
                else
                {
                    res = string.Format(JenkinsTrayResources.BuildDetails_Duration_MM,
                        Math.Max(details.Duration.Minutes + (details.Duration.Seconds >= 30 ? 1 : 0), 1));
                }
                return res;
            }

            private string FormatEstimatedDuration(BuildDetails details)
            {
                if (details == null)
                    return string.Empty;
                string res = string.Empty;
                DateTime endtime = details.Time.Add(details.EstimatedDuration);
                TimeSpan timeleft = TimeSpan.FromTicks(endtime.Subtract(DateTime.UtcNow).Ticks);

                if (timeleft.TotalHours >= 1)
                {
                    res = string.Format(JenkinsTrayResources.BuildDetails_EstimatedDuration_HHMM_Remaining, 
                        timeleft.Days * 24 + timeleft.Hours, timeleft.Minutes);
                }
                else if (timeleft.TotalHours < -1)
                {
                    res = string.Format(JenkinsTrayResources.BuildDetails_EstimatedDuration_HHMM_LongerThanUsual, 
                        Math.Abs(timeleft.Days * 24 + timeleft.Hours), Math.Abs(timeleft.Minutes));
                }
                else if (timeleft.TotalHours < 0)
                {
                    res = string.Format(JenkinsTrayResources.BuildDetails_EstimatedDuration_MM_LongerThanUsual, 
                        Math.Abs(timeleft.Minutes));
                }
                else
                {
                    res = string.Format(JenkinsTrayResources.BuildDetails_EstimatedDuration_MM_Remaining, 
                        timeleft.Minutes);
                }
                return res;
            }
        }

        private void openProjectPageMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedProjectPage();
        }

        private void openConsolePageMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedProjectConsolePage();
        }

        private void runBuildMenuItem_Click(object sender, EventArgs e)
        {
            Project project = GetSelectedProject();
            if (project == null)
                return;
            try
            {
                JenkinsService.SafeRunBuild(project);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, ex);
                XtraMessageBox.Show(string.Format(JenkinsTrayResources.RunBuildFailed_Text, ex.Message),
                    JenkinsTrayResources.RunBuildFailed_Caption);
            }
        }

        private Project GetSelectedProject()
        {
            int[] rowHandles = projectsGridView.GetSelectedRows();
            if (rowHandles.Length != 1)
                return null;

            int rowHandle = rowHandles[0];
            Project project = null;

            if (rowHandle >= 0)
            {
                int dsRowIndex = projectsGridView.GetDataSourceRowIndex(rowHandle);
                ProjectWrapper projectWrapper = projectsDataSource[dsRowIndex];
                project = projectWrapper.Project;
            }
            return project;
        }

        public static void ShowOrFocus()
        {
            if (Instance.WindowState == FormWindowState.Minimized)
                PInvokeUtils.RestoreForm(Instance);
            else if (Instance.Visible)
                Instance.Activate();
            else
                Instance.Show();
        }

        private void checkUpdatesButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool hasUpdates;
            try
            {
                hasUpdates = ApplicationUpdateService.CheckForUpdates_Synchronous(
                    ApplicationUpdateService.UpdateSource.User);
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format(JenkinsTrayResources.ErrorBoxMessage, ex.Message);
                XtraMessageBox.Show(errorMessage, JenkinsTrayResources.ErrorBoxCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (hasUpdates == false)
            {
                XtraMessageBox.Show(JenkinsTrayResources.ApplicationUpdates_NoUpdate_Text,
                    JenkinsTrayResources.ApplicationUpdates_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void acknowledgeMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            Project project = GetSelectedProject();
            if (project == null)
                return;

            if (menuItem.Checked)
            {
                TrayNotifier.Instance.ClearAcknowledgedStatus(project);
            }
            else
            {
                BuildStatus projectStatus = project.Status;
                if (projectStatus.IsStuck || projectStatus.Value >= BuildStatusEnum.Indeterminate)
                    TrayNotifier.Instance.AcknowledgeStatus(project, projectStatus);
            }
            menuItem.Checked = !menuItem.Checked;
        }

        private void stopAcknowledgingMenuItem_Click(object sender, EventArgs e)
        {
            Project project = GetSelectedProject();
            if (project == null)
                return;
            TrayNotifier.Instance.ClearAcknowledgedStatus(project);
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            Project project = GetSelectedProject();
            bool isProjectSelected = project != null;

            openProjectPageMenuItem.Enabled
                = openConsolePageMenuItem.Enabled
                = runBuildMenuItem.Enabled
                = acknowledgeStatusMenuItem.Enabled
                = claimBuildMenuItem.Enabled
                = acknowledgeProjectMenuItem.Enabled
                = setAuthenticationTokenMenuItem.Enabled
                = removeProjectMenuItem.Enabled
                = isProjectSelected;

            if (!isProjectSelected)
            {
                return;
            }

            // get a copy of the reference to avoid a race condition
            BuildStatus projectStatus = project.Status;

            acknowledgeProjectMenuItem.Checked = project.IsAcknowledged;
            acknowledgeStatusMenuItem.Checked = TrayNotifier.Instance.IsStatusAcknowledged(project);
            if (acknowledgeProjectMenuItem.Checked)
            {
                acknowledgeStatusMenuItem.Enabled = false;
            }
            else
            {
                acknowledgeStatusMenuItem.Enabled = (projectStatus.IsStuck || projectStatus.Value >= BuildStatusEnum.Indeterminate);
            }

            bool shouldOpenConsolePage = ShouldOpenConsolePage(project);
            if (shouldOpenConsolePage)
            {
                openProjectPageMenuItem.Font = normalMenuItemFont;
                openConsolePageMenuItem.Font = mainMenuItemFont;
            }
            else
            {
                openConsolePageMenuItem.Font = normalMenuItemFont;
                openProjectPageMenuItem.Font = mainMenuItemFont;
            }

            // Claim
            claimBuildMenuItem.Visible
                = toolStripSeparator2.Visible
                = ConfigurationService.GeneralSettings.IntegrateWithClaimPlugin;
            claimBuildMenuItem.Enabled
                = toolStripSeparator2.Enabled
                = ConfigurationService.GeneralSettings.IntegrateWithClaimPlugin && project.LastFailedBuild != null;
        }

        private void removeProjectMenuItem_Click(object sender, EventArgs e)
        {
            Project project = GetSelectedProject();
            if (project == null)
                return;
            ConfigurationService.RemoveProject(project);
        }

        private void claimBuildMenuItem_Click(object sender, EventArgs e)
        {
            Project project = GetSelectedProject();
            if (project == null)
                return;
            BuildDetails lastFailedBuild = project.LastFailedBuild;
            if (lastFailedBuild == null)
                return;

            var form = new ClaimBuildForm();
            form.Initialize(project, lastFailedBuild);

            var res = form.ShowDialog();
            if (res != DialogResult.OK)
                return;
        }

        private void projectsGridView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            int? res = DoCustomColumnSort(sender, e);
            if (res == null)
                return;
            e.Handled = true;
            e.Result = res.Value;
        }

        private int? DoCustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            DateTime? date1 = GetBuildDate(e.Column, e.ListSourceRowIndex1);
            DateTime? date2 = GetBuildDate(e.Column, e.ListSourceRowIndex2);
            if (date1 == null && date2 == null)
                return null;
            if (date1 == null)
                return -1;
            if (date2 == null)
                return 1;
            int res = date1.Value.CompareTo(date2.Value);
            return res;
        }

        private DateTime? GetBuildDate(GridColumn column, int listSourceRowIndex)
        {
            BuildDetails buildDetails = GetBuildDetails(column, listSourceRowIndex);
            if (buildDetails == null)
                return null;
            return buildDetails.Time;
        }

        private BuildDetails GetBuildDetails(GridColumn column, int listSourceRowIndex)
        {
            ProjectWrapper projectWrapper = (ProjectWrapper)projectsDataSource[listSourceRowIndex];
            if (column == lastSuccessGridColumn)
                return projectWrapper.LastSuccessBuild;
            if (column == lastFailureGridColumn)
                return projectWrapper.LastFailureBuild;
            if (column == lastBuildGridColumn)
                return projectWrapper.LastBuild;
            throw new Exception("Column not supported: " + column.Caption);
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                ProjectsUpdateService.UpdateProjects();
        }

        private delegate void UpdateIconDelegate(Icon icon);
        public void UpdateIcon(Icon icon)
        {
            if (ConfigurationService.GeneralSettings.UpdateMainWindowIcon == false)
                return;

            if (InvokeRequired)
            {
                Delegate del = new UpdateIconDelegate(UpdateIcon);
                MainForm.Instance.BeginInvoke(del, icon);
                return;
            }

            this.Icon = icon;
        }

        private void ResetIcon()
        {
            // copied from the designer code
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
        }

        private void UpdateClaimPluginIntegration()
        {
            bool integrate = ConfigurationService.GeneralSettings.IntegrateWithClaimPlugin;
            if (integrate)
            {
                if (claimedByGridColumn.VisibleIndex == -1)
                {
                    claimedByGridColumn.Visible = true;
                    claimedByGridColumn.VisibleIndex = projectsGridView.Columns.Count - 2;
                }
                if (claimReasonGridColumn.VisibleIndex == -1)
                {
                    claimReasonGridColumn.Visible = true;
                    claimReasonGridColumn.VisibleIndex = 8;
                }
            }
            else
            {
                claimedByGridColumn.Visible = false;
                claimReasonGridColumn.Visible = false;
            }
        }

        private void acknowledgeProjectMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            Project project = GetSelectedProject();
            if (project == null)
                return;

            menuItem.Checked = project.IsAcknowledged = !menuItem.Checked;
            TrayNotifier.Instance.AcknowledgedProject();
        }

        private void setAuthenticationTokenMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            Project project = GetSelectedProject();
            if (project == null)
                return;

            AuthenticationTokenForm.ShowDialogOrFocus(project);
        }
    }
}
