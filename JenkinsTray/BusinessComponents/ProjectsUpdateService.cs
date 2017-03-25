using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using Amib.Threading;
using Common.Logging;
using JenkinsTray.Entities;
using JenkinsTray.Utils.Logging;

namespace JenkinsTray.BusinessComponents
{
    public class ProjectsUpdateService
    {
        public enum UpdateSource
        {
            User,
            Timer,
            Program
        }

        public delegate void ProjectsUpdatedHandler();

        public event ProjectsUpdatedHandler ProjectsUpdated;

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

#if !DEBUG
        const int TOTAL_THREAD_COUNT = 8;
        const int THREAD_COUNT_BY_DOMAIN = 4;
#else
        private const int TOTAL_THREAD_COUNT = 1;
        private const int THREAD_COUNT_BY_DOMAIN = 1;
#endif

        private readonly SmartThreadPool threadPool = new SmartThreadPool(3600, TOTAL_THREAD_COUNT, TOTAL_THREAD_COUNT);

        private Timer timer;
        private bool updating;

        public ConfigurationService ConfigurationService { get; set; }
        public JenkinsService JenkinsService { get; set; }

        public void Initialize()
        {
            timer = new Timer(UpdateProjects, null, 0, Timeout.Infinite);
        }

        public void UpdateProjects()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate { DoUpdateProjects(UpdateSource.User); };
            worker.RunWorkerAsync();
        }

        private void UpdateProjects(object state)
        {
            DoUpdateProjects(UpdateSource.Timer);

            var timeBetweenUpdates = ConfigurationService.GeneralSettings.RefreshIntervalInSeconds*1000;
            timer.Change(timeBetweenUpdates, Timeout.Infinite);
        }

        private void DoUpdateProjects(UpdateSource source)
        {
            logger.Info("Running update from " + source);

            lock (this)
            {
                if (updating)
                {
                    logger.Info("Already in update: skipping");
                    return;
                }
                updating = true;
            }

            try
            {
                DoUpdateProjectsInternal();
                JenkinsService.RecycleCache();
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, ex);
                throw;
            }
            finally
            {
                lock (this)
                {
                    updating = false;
                }
            }

            logger.Info("Done");
        }

        private void DoUpdateProjectsInternal()
        {
            var projectsByServer = ConfigurationService.GetProjects();
            var allWorkItemsGroup = new List<IWorkItemsGroup>();
            var allFutureBuildDetails = new Dictionary<Project, IWorkItemResult>();

            foreach (var pair in projectsByServer)
            {
                var server = pair.Key;
                var projects = pair.Value;

                var workItemsGroup = threadPool.CreateWorkItemsGroup(THREAD_COUNT_BY_DOMAIN);
                allWorkItemsGroup.Add(workItemsGroup);

                foreach (var project in projects)
                {
                    WorkItemCallback work = delegate(object state)
                                            {
                                                AllBuildDetails newBuildDetail = null;
                                                try
                                                {
                                                    var project_ = (Project) state;
                                                    newBuildDetail = JenkinsService.UpdateProject(project_);
                                                }
                                                catch (Exception ex)
                                                {
                                                    LoggingHelper.LogError(logger, ex);
                                                }
                                                return newBuildDetail;
                                            };
                    var futureRes = workItemsGroup.QueueWorkItem(work, project);
                    allFutureBuildDetails[project] = futureRes;
                }
            }

            foreach (var workItemsGroup in allWorkItemsGroup)
            {
                workItemsGroup.WaitForIdle();
            }

            foreach (var projects in projectsByServer.Values)
            {
                foreach (var project in projects)
                {
                    IWorkItemResult newStatus;
                    allFutureBuildDetails.TryGetValue(project, out newStatus);
                    var previousAllBuildDetails = project.AllBuildDetails;
                    if (newStatus != null)
                    {
                        project.AllBuildDetails = (AllBuildDetails) newStatus.Result;
                        project.Activity.HasNewBuild = false;

                        if (previousAllBuildDetails != null && project.AllBuildDetails != null)
                        {
                            project.PreviousStatus = previousAllBuildDetails.Status;

                            if (previousAllBuildDetails.LastBuild != null && project.AllBuildDetails.LastBuild != null)
                            {
                                //  Has existing LastBuilds
                                if (previousAllBuildDetails.LastBuild.Number != project.AllBuildDetails.LastBuild.Number)
                                {
                                    project.Activity.HasNewBuild = true;
                                }
                            }
                            else if (previousAllBuildDetails.LastBuild == null &&
                                     project.AllBuildDetails.LastBuild != null)
                            {
                                //  1st new LastBuild is found
                                project.Activity.HasNewBuild = true;
                            }
                        }
                    }
                }
            }

            if (ProjectsUpdated != null)
                ProjectsUpdated();
        }
    }
}