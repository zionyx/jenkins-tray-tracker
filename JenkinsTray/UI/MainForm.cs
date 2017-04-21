using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;
using Common.Logging;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using JenkinsTray.BusinessComponents;
using JenkinsTray.Entities;
using JenkinsTray.Utils;
using JenkinsTray.Utils.Logging;
using Spring.Context.Support;

namespace JenkinsTray.UI
{
    public partial class MainForm : XtraForm
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private bool exiting;
        private IDictionary<string, byte[]> iconsByKey;
        private int lastHoveredDSRowIndex = -1;
        private readonly Font mainMenuItemFont;
        private readonly Font normalMenuItemFont;

        private BindingList<ProjectWrapper> projectsDataSource;

        public MainForm()
        {
            InitializeComponent();
            normalMenuItemFont = openProjectPageMenuItem.Font;
            mainMenuItemFont = new Font(openProjectPageMenuItem.Font, FontStyle.Bold);
        }

        public static MainForm Instance
        {
            get
            {
                var instance = (MainForm) ContextRegistry.GetContext().GetObject("MainForm");
                return instance;
            }
        }

        public ConfigurationService ConfigurationService { get; set; }
        public JenkinsService JenkinsService { get; set; }
        public ProjectsUpdateService ProjectsUpdateService { get; set; }
        public ApplicationUpdateService ApplicationUpdateService { get; set; }

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
            SetFoldersVisibility();
            LoadIcons();
            LoadProjects();
        }

        private void configurationService_ConfigurationUpdated()
        {
            UpdateClaimPluginIntegration();
            SetFoldersVisibility();
            LoadProjects();
            if (ConfigurationService.GeneralSettings.UpdateMainWindowIcon == false)
                ResetIcon();
        }

        private void updateService_ProjectsUpdated()
        {
            Delegate del = new ProjectsUpdatedDelegate(OnProjectsUpdated);
            BeginInvoke(del);
        }

        private void OnProjectsUpdated()
        {
            UpdateProjects();
        }

        private void settingsButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            SettingsForm.Instance.ShowDialog();
        }

        private void LoadProjects()
        {
            projectsDataSource = new BindingList<ProjectWrapper>();

            foreach (var server in ConfigurationService.Servers)
            {
                foreach (var project in server.Projects)
                {
                    var wrapper = new ProjectWrapper(project, 
                        ConfigurationService.GeneralSettings.ShowProjectDisplayNameInMainUI);
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

        private void refreshButtonItem_ItemClick(object sender, ItemClickEventArgs e)
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

        private void exitButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            Exit();
        }

        private void projectsGridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                if (e.Column == statusGridColumn)
                {
                    var projectWrapper = projectsDataSource[e.ListSourceRowIndex];
                    var imgBytes = iconsByKey[projectWrapper.Project.Status.Key];
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
            var status = new BuildStatus(statusValue, isInProgress, isStuck);
            if (iconsByKey.ContainsKey(status.Key))
                return;

            try
            {
                var resourceName = string.Format("JenkinsTray.Resources.StatusIcons.{0}.gif", status.Key);
                var img = ResourceImageHelper.CreateImageFromResources(resourceName, GetType().Assembly);
                var imgBytes = ByteImageConverter.ToByteArray(img, ImageFormat.Gif);
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

        private void aboutButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            AboutForm.Instance.ShowDialog();
        }

        private void projectsGridView_MouseMove(object sender, MouseEventArgs e)
        {
            var pt = new Point(e.X, e.Y);
            var ghi = projectsGridView.CalcHitInfo(pt);
            if (ghi.InRowCell)
            {
                var dsRowIndex = projectsGridView.GetDataSourceRowIndex(ghi.RowHandle);
                if (lastHoveredDSRowIndex != dsRowIndex)
                {
                    var project = projectsDataSource[dsRowIndex];
                    var message = JenkinsTrayResources.ResourceManager
                                                      .GetString("BuildStatus_" + project.Project.Status.Key);
                    if (project.Project.Status.IsStuck && project.Project.Queue.InQueue)
                    {
                        message += string.Format(JenkinsTrayResources.BuildDetails_InQueue_Since,
                                                 project.Project.Queue.InQueueSince);
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
            var mouseEventArgs = e as DXMouseEventArgs;
            var pt = new Point(mouseEventArgs.X, mouseEventArgs.Y);
            var hi = projectsGridView.CalcHitInfo(pt);

            if (hi.InRowCell)
            {
                var project = GetSelectedProject();
                if (project == null)
                    return;
                var shouldOpenConsolePage = ShouldOpenConsolePage(project);
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
            var status = project.Status;
            var res = BuildStatusUtils.IsErrorBuild(status) || status.IsInProgress;
            return res;
        }

        private void OpenSelectedProjectPage()
        {
            var project = GetSelectedProject();
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
            var project = GetSelectedProject();
            OpenProjectConsolePage(project);
        }

        private void OpenProjectConsolePage(Project project)
        {
            if (project == null)
                return;
            var url = JenkinsService.GetConsolePage(project);
            UIUtils.OpenWebPage(url, logger);
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
            var project = GetSelectedProject();
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

        private void stopBuildMenuItem_Click(object sender, EventArgs e)
        {
            var project = GetSelectedProject();
            if (project == null)
                return;
            try
            {
                if (project.AllBuildDetails != null && project.AllBuildDetails.Status != null &&
                    project.AllBuildDetails.Status.IsInProgress)
                {
                    JenkinsService.SafeStopBuild(project);
                }
                else if (project.Queue.InQueue && project.Queue.Id > 0)
                {
                    JenkinsService.SafeRemoveFromQueue(project);
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, ex);
                XtraMessageBox.Show(string.Format(JenkinsTrayResources.StopBuildFailed_Text, ex.Message),
                                    JenkinsTrayResources.StopBuildFailed_Caption);
            }
        }

        private Project GetSelectedProject()
        {
            var rowHandles = projectsGridView.GetSelectedRows();
            if (rowHandles.Length != 1)
                return null;

            var rowHandle = rowHandles[0];
            Project project = null;

            if (rowHandle >= 0)
            {
                var dsRowIndex = projectsGridView.GetDataSourceRowIndex(rowHandle);
                var projectWrapper = projectsDataSource[dsRowIndex];
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

        private void checkUpdatesButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool hasUpdates;
            try
            {
                hasUpdates = ApplicationUpdateService.CheckForUpdates_Synchronous(
                    ApplicationUpdateService.UpdateSource.User);
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(JenkinsTrayResources.ErrorBoxMessage, ex.Message);
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
            var menuItem = sender as ToolStripMenuItem;
            var project = GetSelectedProject();
            if (project == null)
                return;

            if (menuItem.Checked)
            {
                TrayNotifier.Instance.ClearAcknowledgedStatus(project);
            }
            else
            {
                var projectStatus = project.Status;
                if (projectStatus.IsStuck || projectStatus.Value >= BuildStatusEnum.Indeterminate)
                    TrayNotifier.Instance.AcknowledgeStatus(project, projectStatus);
            }
            menuItem.Checked = !menuItem.Checked;
        }

        private void stopAcknowledgingMenuItem_Click(object sender, EventArgs e)
        {
            var project = GetSelectedProject();
            if (project == null)
                return;
            TrayNotifier.Instance.ClearAcknowledgedStatus(project);
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            var project = GetSelectedProject();
            var isProjectSelected = project != null;

            openProjectPageMenuItem.Enabled
                = openConsolePageMenuItem.Enabled
                    = runBuildMenuItem.Enabled
                        = stopBuildMenuItem.Enabled
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
            var projectStatus = project.Status;

            stopBuildMenuItem.Text = "&Abort build";
            if (!projectStatus.IsInProgress && project.Queue.InQueue)
            {
                stopBuildMenuItem.Text = "&Cancel queue";
            }
            else if (!projectStatus.IsInProgress && !project.Queue.InQueue)
            {
                stopBuildMenuItem.Enabled = false;
            }

            stopBuildMenuItem.Visible = projectStatus.IsInProgress;
            runBuildMenuItem.Visible = !projectStatus.IsInProgress;

            acknowledgeProjectMenuItem.Checked = project.IsAcknowledged;
            acknowledgeStatusMenuItem.Checked = TrayNotifier.Instance.IsStatusAcknowledged(project);
            if (acknowledgeProjectMenuItem.Checked)
            {
                acknowledgeStatusMenuItem.Enabled = false;
            }
            else
            {
                acknowledgeStatusMenuItem.Enabled = projectStatus.IsStuck ||
                                                    projectStatus.Value >= BuildStatusEnum.Indeterminate;
            }

            var shouldOpenConsolePage = ShouldOpenConsolePage(project);
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
            var project = GetSelectedProject();
            if (project == null)
                return;
            ConfigurationService.RemoveProject(project);
        }

        private void claimBuildMenuItem_Click(object sender, EventArgs e)
        {
            var project = GetSelectedProject();
            if (project == null)
                return;
            var lastFailedBuild = project.LastFailedBuild;
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
            var res = DoCustomColumnSort(sender, e);
            if (res == null)
                return;
            e.Handled = true;
            e.Result = res.Value;
        }

        private int? DoCustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            var date1 = GetBuildDate(e.Column, e.ListSourceRowIndex1);
            var date2 = GetBuildDate(e.Column, e.ListSourceRowIndex2);
            if (date1 == null && date2 == null)
                return null;
            if (date1 == null)
                return -1;
            if (date2 == null)
                return 1;
            var res = date1.Value.CompareTo(date2.Value);
            return res;
        }

        private DateTime? GetBuildDate(GridColumn column, int listSourceRowIndex)
        {
            var buildDetails = GetBuildDetails(column, listSourceRowIndex);
            if (buildDetails == null)
                return null;
            return buildDetails.Time;
        }

        private BuildDetails GetBuildDetails(GridColumn column, int listSourceRowIndex)
        {
            var projectWrapper = projectsDataSource[listSourceRowIndex];
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

        public void UpdateIcon(Icon icon)
        {
            if (ConfigurationService.GeneralSettings.UpdateMainWindowIcon == false)
                return;

            if (InvokeRequired)
            {
                Delegate del = new UpdateIconDelegate(UpdateIcon);
                Instance.BeginInvoke(del, icon);
                return;
            }

            Icon = icon;
        }

        private void ResetIcon()
        {
            // copied from the designer code
            var resources = new ComponentResourceManager(typeof(MainForm));
            Icon = (Icon) resources.GetObject("$this.Icon");
        }

        private void UpdateClaimPluginIntegration()
        {
            var integrate = ConfigurationService.GeneralSettings.IntegrateWithClaimPlugin;
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

        private void SetFoldersVisibility()
        {
            folderGridColumn.Visible = ConfigurationService.GeneralSettings.ShowFolders;
        }

        private void acknowledgeProjectMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            var project = GetSelectedProject();
            if (project == null)
                return;

            menuItem.Checked = project.IsAcknowledged = !menuItem.Checked;
            TrayNotifier.Instance.AcknowledgedProject();
        }

        private void setAuthenticationTokenMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            var project = GetSelectedProject();
            if (project == null)
                return;

            AuthenticationTokenForm.ShowDialogOrFocus(project);
        }

        private delegate void ProjectsUpdatedDelegate();

        private class ProjectWrapper
        {
            private bool ShowDisplayname { get; set; }

            public ProjectWrapper(Project project, bool showDisplayName)
            {
                Project = project;
                ShowDisplayname = showDisplayName;
            }

            public Project Project { get; }

            public string Server
            {
                get { return Project.Server.DisplayText; }
            }

            public string Folder
            {
                get { return Project.Folder; }
            }

            public string Name
            {
                get { return ShowDisplayname && string.IsNullOrEmpty(Project.DisplayName) ? Project.Name : Project.DisplayName; }
            }

            public string Url
            {
                get { return Uri.UnescapeDataString(Project.Url); }
            }

            public string buildDetailsStr
            {
                get
                {
                    var details = string.Empty;
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
                    return Project.LastFailedBuild.ClaimDetails.Assignee;
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
                var details = string.Empty;
                var buildCausesSummary = string.Empty;

                // get a copy of the reference to avoid a race condition
                var projectStatus = Project.Status;
                var lastBuild = Project.LastBuild;

                if (lastBuild != null)
                {
                    // get a copy of the reference to avoid a race condition
                    var lastBuildCauses = lastBuild.Causes;

                    if (lastBuildCauses != null)
                    {
                        if (projectStatus.IsInProgress)
                        {
                            var progressts = lastBuild.EstimatedDuration;
                            var timeleft = FormatEstimatedDuration(lastBuild);

                            foreach (var cause in lastBuildCauses.Causes)
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
                                            buildCausesSummary =
                                                string.Format(JenkinsTrayResources.BuildDetails_Cause_SCM_Multiple,
                                                              lastBuild.Users.Count);
                                        }
                                        else
                                        {
                                            buildCausesSummary =
                                                string.Format(JenkinsTrayResources.BuildDetails_Cause_SCM_Single,
                                                              FormatUsers(lastBuild));
                                        }
                                    }
                                        break;
                                    case BuildCauseEnum.User:
                                    {
                                        buildCausesSummary = string.Format(
                                            JenkinsTrayResources.BuildDetails_Cause_User, cause.Starter);
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
                                details = projectStatus.Value + ".";
                            }
                            else if (projectStatus.Value == BuildStatusEnum.Disabled)
                            {
                                details = string.Format("{0}. ", JenkinsTrayResources.BuildStatus_Disabled);
                            }
                            else
                            {
                                details = projectStatus.Value + ". ";
                                if (lastBuild.Users != null && !lastBuild.Users.IsEmpty)
                                {
                                    details += string.Format(JenkinsTrayResources.BuildDetails_BrokenBy,
                                                             FormatUsers(lastBuild));
                                }
                            }
                        }
                        if (Project.Queue.InQueue)
                        {
                            if (!projectStatus.IsInProgress)
                            {
                                details += string.Format(JenkinsTrayResources.BuildDetails_InQueue_Why,
                                                         Project.Queue.Why);
                            }
                        }
                        if (projectStatus.IsStuck)
                        {
                            details = "Queued, possibly stuck. " +
                                      string.Format(JenkinsTrayResources.BuildDetails_InQueue_Why, Project.Queue.Why);
                        }
                    }
                }

                return details;
            }

            private string FormatBuildDetailsWithDisplayName(BuildDetails details)
            {
                if (details == null)
                    return "-";
                var shortDisplayName = details.DisplayName.Replace(Project.Name, string.Empty).Trim();

                var res = string.Empty;
                if (shortDisplayName.Equals(string.Concat("#", details.Number.ToString())))
                {
                    res = string.Format(JenkinsTrayResources.BuildDetails_Format_NumberDate,
                                        details.Number, details.Time.ToLocalTime());
                }
                else
                {
                    res = string.Format(JenkinsTrayResources.BuildDetails_Format_DisplayName_NumberDate,
                                        shortDisplayName, details.Number, details.Time.ToLocalTime());
                }
                return res;
            }

            private string FormatBuildDetails(BuildDetails details)
            {
                if (details == null)
                    return "-";
                
                DateTime buildDateTime = details.Time.ToLocalTime();
                string buildDateStr = "";

                if (buildDateTime.Date == DateTime.Today.Date)
                {
                    buildDateStr = JenkinsTrayResources.Today_Text;
                }
                else if (buildDateTime.Date == DateTime.Today.Date.AddDays(-1))
                {
                    buildDateStr = JenkinsTrayResources.Yesterday_Text;
                }
                else
                {
                    buildDateStr = buildDateTime.Date.ToString("d");   
                }

                var buildTimeStr = buildDateTime.ToString("t");

                var res = string.Format(JenkinsTrayResources.BuildDetails_Format_NumberDate,
                                        buildDateStr, buildTimeStr, details.Number);

                return res;
            }

            private string FormatUsers(BuildDetails details)
            {
                if (details == null)
                    return "-";
                var res = StringUtils.Join(details.Users, JenkinsTrayResources.BuildDetails_UserSeparator);
                return res;
            }

            private string FormatDuration(BuildDetails details)
            {
                if (details == null)
                    return string.Empty;
                var res = string.Empty;
                if (details.Duration.TotalHours > 1)
                {
                    res = string.Format(JenkinsTrayResources.BuildDetails_Duration_HHMM,
                                        details.Duration.Days*24 + details.Duration.Hours, details.Duration.Minutes);
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
                var res = string.Empty;
                var endtime = details.Time.Add(details.EstimatedDuration);
                var timeleft = TimeSpan.FromTicks(endtime.Subtract(DateTime.UtcNow).Ticks);

                if (timeleft.TotalHours >= 1)
                {
                    res = string.Format(JenkinsTrayResources.BuildDetails_EstimatedDuration_HHMM_Remaining,
                                        timeleft.Days*24 + timeleft.Hours, timeleft.Minutes);
                }
                else if (timeleft.TotalHours < -1)
                {
                    res = string.Format(JenkinsTrayResources.BuildDetails_EstimatedDuration_HHMM_LongerThanUsual,
                                        Math.Abs(timeleft.Days*24 + timeleft.Hours), Math.Abs(timeleft.Minutes));
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

        private delegate void UpdateIconDelegate(Icon icon);
    }
}