using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using JenkinsTray.BusinessComponents;
using JenkinsTray.Entities;
using JenkinsTray.Utils.BackgroundProcessing;

namespace JenkinsTray.UI.Controls
{
    public partial class ProjectListControl : XtraUserControl
    {
        private List<Project> projectsDataSource;
        private Server server;

        public ProjectListControl()
        {
            InitializeComponent();
        }

        public ServersSettingsController Controller { get; set; }
        public ConfigurationService ConfigurationService { get; set; }
        public JenkinsService JenkinsService { get; set; }

        public void Initialize()
        {
        }

        public void UpdateProjectList(Server server)
        {
            this.server = server;
#if SYNCRHONOUS
            List<Project> dataSource = new List<Project>();

            if (server != null)
            {
                IList<Project> projects = jenkinsService.LoadProjects(server);
                foreach (Project project in projects)
                    dataSource.Add(project);
            }

            SetProjectsDataSource(dataSource);
#else
            // clear the view
            projectsGridControl.DataSource = null;

            if (server == null)
                return;

            // disable the window, change the cursor, update the status
            Cursor.Current = Cursors.WaitCursor;
            Enabled = false;
            var status = string.Format(JenkinsTrayResources.LoadingProjects_FormatString, server.Url);
            Controller.SetStatus(status, true);

            // run the process in background
            var process = new Process("Loading project " + server.Url);
            IList<Project> projects = null;
            process.DoWork += delegate { projects = JenkinsService.LoadProjects(server); };
            process.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                                          {
                                              var endStatus = "";

                                              if (e.Error == null)
                                              {
                                                  var dataSource = new List<Project>();
                                                  foreach (var project in projects)
                                                      dataSource.Add(project);
                                                  SetProjectsDataSource(dataSource);
                                              }
                                              else
                                              {
                                                  endStatus =
                                                      string.Format(
                                                          JenkinsTrayResources.FailedLoadingProjects_FormatString,
                                                          server.Url);
                                              }

                                              // enable the window, change the cursor, update the status
                                              Enabled = true;
                                              Cursor.Current = Cursors.Default;
                                              Controller.SetStatus(endStatus, false);
                                          };
            BackgroundProcessExecutor.Execute(process);
#endif
        }

        private void SetProjectsDataSource(List<Project> dataSource)
        {
            projectsDataSource = dataSource;
            projectsGridControl.DataSource = projectsDataSource;
        }

        private void projectsGridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                var project = projectsDataSource[e.ListSourceRowIndex];
                var selected = project.Server.Projects.Contains(project);
                e.Value = selected;
            }
            else if (e.IsSetData)
            {
                var project = projectsDataSource[e.ListSourceRowIndex];
                var selected = (bool) e.Value;
                if (selected)
                    ConfigurationService.AddProject(project);
                else
                    ConfigurationService.RemoveProject(project);
            }
        }

        private void projectSelectedCheckEdit_EditValueChanged(object sender, EventArgs e)
        {
            // validate the check box value as soon as it is clicked
            ((CheckEdit) sender).DoValidate();
            projectsGridView.CloseEditor();
        }

        private void selectAllProjectsMenuItem_Click(object sender, EventArgs e)
        {
            if (projectsDataSource == null)
                return;
            SelectAllProjects();
        }

        private void deselectAllProjectsMenuItem_Click(object sender, EventArgs e)
        {
            if (projectsDataSource == null)
                return;
            UnselectAllProjects();
        }

        private void toggleSelectionButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (projectsDataSource == null)
                return;
            if (server.Projects.Count < projectsDataSource.Count)
                SelectAllProjects();
            else
                UnselectAllProjects();
        }

        private void SelectAllProjects()
        {
            ConfigurationService.AddProjects(projectsDataSource);
            projectsGridView.RefreshData();
        }

        private void UnselectAllProjects()
        {
            ConfigurationService.RemoveProjects(projectsDataSource);
            projectsGridView.RefreshData();
        }

        private void ProjectListControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (char.ToUpper(e.KeyChar))
            {
                case (char) Keys.Space:
                    CheckSelectedProject();
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        private void CheckSelectedProject()
        {
            var selected = projectsGridView.GetSelectedRows();
            if (selected.Length == 1 && selected[0] <= projectsGridView.RowCount)
            {
                var isChecked = (bool) projectsGridView.GetRowCellValue(selected[0], projectSelectedGridColumn);
                projectsGridView.SetRowCellValue(selected[0], projectSelectedGridColumn, !isChecked);
            }
        }

        private void projectsGridControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CheckSelectedProject();
        }
    }
}