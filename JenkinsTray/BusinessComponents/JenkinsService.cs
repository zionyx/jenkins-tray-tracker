using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using Common.Logging;
using JenkinsTray.Entities;
using JenkinsTray.Utils;
using JenkinsTray.Utils.Logging;
using JenkinsTray.Utils.Web;
using Spring.Collections.Generic;

namespace JenkinsTray.BusinessComponents
{
    public class JenkinsService
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ThreadStatic] private static WebClient threadWebClient;

        [ThreadStatic] private static bool ignoreUntrustedCertificate;

        // cache: key=url, value=xml
        private IDictionary<string, string> cache = new Dictionary<string, string>();
        // URLs visited between 2 calls to RecycleCache()
        private readonly Spring.Collections.Generic.ISet<string> visitedURLs = new HashedSet<string>();

        public JenkinsService()
        {
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
        }

        public ClaimService ClaimService { get; set; }

        public ConfigurationService ConfigurationService { get; set; }

        public IList<Project> LoadProjects(Server server)
        {
            var url = NetUtils.ConcatUrls(server.Url, "/api/xml?tree=jobs[name,url,color]");

            logger.Info("Loading projects from " + url);

            var xmlStr = DownloadString(server.Credentials, url, false, server.IgnoreUntrustedCertificate);

            if (logger.IsTraceEnabled)
                logger.Trace("XML: " + xmlStr);

            var xml = new XmlDocument();
            xml.LoadXml(xmlStr);

            var jobElements = xml.SelectNodes("/hudson/job");
            var projects = GetProjects(jobElements, server);

            logger.Info("Done loading projects");

            return projects;
        }

        public List<Project> GetProjects(XmlNodeList jobElements, Server server, string folder = "")
        {
            var projects = new List<Project>();

            var showFolders = ConfigurationService.GeneralSettings.ShowFolders;

            foreach (XmlNode jobElement in jobElements)
            {
                var projectName = jobElement.SelectSingleNode("name").InnerText;
                var projectUrl = jobElement.SelectSingleNode("url").InnerText;
                var projectColor = jobElement.SelectSingleNode("color");
                // If the job is a folder we need to recursively get the jobs within.
                if (projectColor == null)  //folders do not have a color node
                {
                    if (showFolders)
                    {
                        var url = NetUtils.ConcatUrls(projectUrl, "/api/xml?tree=jobs[name,url,color]");
                        var xmlStr = DownloadString(server.Credentials, url, false, server.IgnoreUntrustedCertificate);
                        var xml = new XmlDocument();
                        xml.LoadXml(xmlStr);
                        var nodes = xml.SelectNodes("/folder/job");
                        projects.AddRange(GetProjects(nodes, server, projectName));
                    }
                }
                else
                {
                    var project = new Project();
                    project.Server = server;
                    if (!string.IsNullOrEmpty(folder))
                    {
                        project.Folder = folder;
                    }
                    project.Name = projectName;
                    project.Url = projectUrl;

                    if (logger.IsDebugEnabled)
                        logger.Debug("Found project " + projectName + " (" + projectUrl + ")");

                    // Ensure only unique entries in the returned list.
                    if (!projects.Contains(project))
                    {
                        projects.Add(project);
                    }
                }
            }
            return projects;
        }

        public AllBuildDetails UpdateProject(Project project)
        {
            var url = NetUtils.ConcatUrls(project.Url, "/api/xml");

            //logger.Info("Updating project from " + url);

            var credentials = project.Server.Credentials;
            var ignoreUntrustedCertificate = project.Server.IgnoreUntrustedCertificate;
            var xmlStr = DownloadString(credentials, url, false, ignoreUntrustedCertificate);

            if (logger.IsTraceEnabled)
                logger.Trace("XML: " + xmlStr);

            var xml = new XmlDocument();
            xml.LoadXml(xmlStr);

            var displayName = XmlUtils.SelectSingleNodeText(xml, "/*/displayName");
            var inQueue = XmlUtils.SelectSingleNodeBoolean(xml, "/*/inQueue");
            var inQueueSince = XmlUtils.SelectSingleNodeText(xml, "/*/queueItem/inQueueSince");
            var queueId = XmlUtils.SelectSingleNodeText(xml, "/*/queueItem/id");
            var why = XmlUtils.SelectSingleNodeText(xml, "/*/queueItem/why");
            var stuck = XmlUtils.SelectSingleNodeBoolean(xml, "/*/queueItem/stuck");
            var status = xml.SelectSingleNode("/*/color").InnerText;
            var lastBuildUrl = XmlUtils.SelectSingleNodeText(xml, "/*/lastBuild/url");
            var lastCompletedBuildUrl = XmlUtils.SelectSingleNodeText(xml, "/*/lastCompletedBuild/url");
            var lastSuccessfulBuildUrl = XmlUtils.SelectSingleNodeText(xml, "/*/lastSuccessfulBuild/url");
            var lastFailedBuildUrl = XmlUtils.SelectSingleNodeText(xml, "/*/lastFailedBuild/url");

            project.DisplayName = displayName;
            project.Queue.InQueue = inQueue.HasValue && inQueue.Value;
            if (!string.IsNullOrEmpty(queueId))
            {
                project.Queue.Id = long.Parse(queueId);
            }
            if (!string.IsNullOrEmpty(inQueueSince))
            {
                var ts = TimeSpan.FromSeconds(long.Parse(inQueueSince)/1000);
                var date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                date = date.Add(ts);
                project.Queue.InQueueSince = date;
            }
            if (!string.IsNullOrEmpty(why))
            {
                project.Queue.Why = why;
            }

            var res = new AllBuildDetails();
            res.Status = GetStatus(status, stuck);
            res.LastBuild = GetBuildDetails(credentials, lastBuildUrl, ignoreUntrustedCertificate);
            res.LastCompletedBuild = GetBuildDetails(credentials, lastCompletedBuildUrl, ignoreUntrustedCertificate);
            res.LastSuccessfulBuild = GetBuildDetails(credentials, lastSuccessfulBuildUrl, ignoreUntrustedCertificate);
            res.LastFailedBuild = GetBuildDetails(credentials, lastFailedBuildUrl, ignoreUntrustedCertificate);

            //logger.Info("Done updating project");
            return res;
        }

        //  http://javadoc.jenkins-ci.org/hudson/model/BallColor.html
        private BuildStatus GetStatus(string status, bool? stuck)
        {
            BuildStatusEnum value;
            if (status.StartsWith("grey"))
                value = BuildStatusEnum.Indeterminate;
            else if (status.StartsWith("blue") || status.StartsWith("green"))
                value = BuildStatusEnum.Successful;
            else if (status.StartsWith("yellow"))
                value = BuildStatusEnum.Unstable;
            else if (status.StartsWith("red"))
                value = BuildStatusEnum.Failed;
            else if (status.StartsWith("aborted"))
                value = BuildStatusEnum.Aborted;
            else if (status.StartsWith("disabled"))
                value = BuildStatusEnum.Disabled;
            else
                value = BuildStatusEnum.Unknown;

            var isInProgress = status.EndsWith("_anime");
            var isStuck = stuck.HasValue && stuck.Value;
            return new BuildStatus(value, isInProgress, isStuck);
        }

        private BuildDetails GetBuildDetails(Credentials credentials, string buildUrl, bool ignoreUntrustedCertificate)
        {
            if (buildUrl == null)
                return null;

            var url = NetUtils.ConcatUrls(buildUrl, "/api/xml");

            if (logger.IsDebugEnabled)
                logger.Debug("Getting build details from " + url);

            var xmlStr = DownloadString(credentials, url, true, ignoreUntrustedCertificate);

            if (logger.IsTraceEnabled)
                logger.Trace("XML: " + xmlStr);

            var xml = new XmlDocument();
            xml.LoadXml(xmlStr);

            var number = xml.SelectSingleNode("/*/number").InnerText;
            var fullDisplayName = xml.SelectSingleNode("/*/fullDisplayName").InnerText;
            var timestamp = xml.SelectSingleNode("/*/timestamp").InnerText;
            var estimatedDuration = xml.SelectSingleNode("/*/estimatedDuration").InnerText;
            var duration = xml.SelectSingleNode("/*/duration").InnerText;
            var xmlResult = xml.SelectSingleNode("/*/result");
            var result = xmlResult == null ? string.Empty : xmlResult.InnerText;
            var userNodes = xml.SelectNodes("/*/culprit/fullName");

            var ts = TimeSpan.FromSeconds(long.Parse(timestamp)/1000);
            var date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            date = date.Add(ts);
            var estimatedts = TimeSpan.FromSeconds(long.Parse(estimatedDuration)/1000);
            var durationts = TimeSpan.FromSeconds(long.Parse(estimatedDuration)/1000);

            Spring.Collections.Generic.ISet<string> users = new HashedSet<string>();
            foreach (XmlNode userNode in userNodes)
            {
                var userName = StringUtils.ExtractUserName(userNode.InnerText);
                users.Add(userName);
            }

            var res = new BuildDetails();
            BuildCauses.FillInBuildCauses(res, xml);
            res.Number = int.Parse(number);
            res.DisplayName = fullDisplayName;
            res.Time = date;
            res.EstimatedDuration = estimatedts;
            res.Duration = durationts;
            res.Result = BuildStatus.StringToBuildStatus(result);
            res.Users = users;

            ClaimService.FillInBuildDetails(res, xml);

            if (logger.IsDebugEnabled)
                logger.Debug("Done getting build details");

            return res;
        }

        public void SafeRunBuild(Project project)
        {
            try
            {
                RunBuild(project);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, ex);
                throw ex;
            }
        }

        public void RunBuild(Project project)
        {
            var url = NetUtils.ConcatUrls(project.Url, "/build?delay=0sec");

            if (!string.IsNullOrEmpty(project.AuthenticationToken))
            {
                url = NetUtils.ConcatUrlsWithoutTrailingSlash(url, "&token=",
                                                              HttpUtility.UrlEncode(project.AuthenticationToken));
                if (!string.IsNullOrEmpty(project.CauseText))
                {
                    url = NetUtils.ConcatUrlsWithoutTrailingSlash(url, "&cause=",
                                                                  HttpUtility.UrlEncode(project.CauseText));
                }
            }
            logger.Info("Running build at " + url);

            var credentials = project.Server.Credentials;
            var str = UploadString(credentials, url, project.Server.IgnoreUntrustedCertificate);

            if (logger.IsTraceEnabled)
                logger.Trace("Result: " + str);

            logger.Info("Done running build");
        }

        public void SafeStopBuild(Project project)
        {
            try
            {
                StopBuild(project);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, ex);
                throw ex;
            }
        }

        public void StopBuild(Project project)
        {
            var url = NetUtils.ConcatUrls(project.Url, "/lastBuild/stop");

            if (!string.IsNullOrEmpty(project.AuthenticationToken))
            {
                url = NetUtils.ConcatUrlsWithoutTrailingSlash(url, "&token=",
                                                              HttpUtility.UrlEncode(project.AuthenticationToken));
                if (!string.IsNullOrEmpty(project.CauseText))
                {
                    url = NetUtils.ConcatUrlsWithoutTrailingSlash(url, "&cause=",
                                                                  HttpUtility.UrlEncode(project.CauseText));
                }
            }
            logger.Info("Stopping build at " + url);

            var credentials = project.Server.Credentials;
            var str = UploadString(credentials, url, project.Server.IgnoreUntrustedCertificate);

            if (logger.IsTraceEnabled)
                logger.Trace("Result: " + str);

            logger.Info("Done stopping build");
        }

        public void SafeRemoveFromQueue(Project project)
        {
            try
            {
                RemoveFromQueue(project.Server, project.Queue.Id);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, ex);
                throw ex;
            }
        }

        public void RemoveFromQueue(Server server, long queueId)
        {
            var url = NetUtils.ConcatUrls(server.Url, "/queue/cancelItem?id=" + queueId);

            logger.Info("Removing queue item at " + url);

            var credentials = server.Credentials;

            try
            {
                var str = UploadString(credentials, url, server.IgnoreUntrustedCertificate);

                if (logger.IsTraceEnabled)
                    logger.Trace("Result: " + str);
            }
            catch (WebException webEx)
            {
                //  Workaround for JENKINS-21311
                if (webEx.Status == WebExceptionStatus.ProtocolError &&
                    ((HttpWebResponse) webEx.Response).StatusCode == HttpStatusCode.NotFound)
                {
                    //  consume error 404
                }
                else
                    throw webEx;
            }
            logger.Info("Done removing queue item");
        }

        private string DownloadString(Credentials credentials, string url, bool cacheable,
                                      bool ignoreUntrustedCertificate)
        {
            string res;

            if (logger.IsTraceEnabled)
                logger.Trace("Downloading: " + url);

            if (cacheable)
            {
                lock (this)
                {
                    // mark the URL as visited
                    visitedURLs.Add(url);
                    // perform a lookup in the cache
                    if (cache.TryGetValue(url, out res))
                    {
                        if (logger.IsTraceEnabled)
                            logger.Trace("Cache hit: " + url);
                        return res;
                    }
                }

                if (logger.IsTraceEnabled)
                    logger.Trace("Cache miss: " + url);
            }

            // set the thread-static field
            JenkinsService.ignoreUntrustedCertificate = ignoreUntrustedCertificate;

            var webClient = GetWebClient(credentials);
            res = webClient.DownloadString(url);

            if (logger.IsTraceEnabled)
                logger.Trace("Downloaded: " + res);

            if (cacheable)
            {
                lock (this)
                {
                    // store in cache
                    cache[url] = res;
                }
            }

            return res;
        }

        private string UploadString(Credentials credentials, string url, bool ignoreUntrustedCertificate)
        {
            string res;

            if (logger.IsTraceEnabled)
                logger.Trace("Uploading: " + url);

            // set the thread-static field
            JenkinsService.ignoreUntrustedCertificate = ignoreUntrustedCertificate;

            var webClient = GetWebClient(credentials);
            webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            res = webClient.UploadString(url, "");

            if (logger.IsTraceEnabled)
                logger.Trace("Uploaded: " + res);

            return res;
        }

        private WebClient GetWebClient(Credentials credentials)
        {
            if (threadWebClient == null)
            {
                logger.Info("Creating web client in thread " + Thread.CurrentThread.ManagedThreadId
                            + " (" + Thread.CurrentThread.Name + ")");
                threadWebClient = new CookieAwareWebClient();
                threadWebClient.Encoding = Encoding.UTF8;
            }

            // reinitialize HTTP headers
            threadWebClient.Headers = new WebHeaderCollection();

            // credentials
            if (credentials != null)
            {
                var authentication = "Basic " + Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(credentials.Username + ":" + credentials.Password));
                threadWebClient.Headers.Add("Authorization", authentication);
            }

            return threadWebClient;
        }

        public void RecycleCache()
        {
            lock (this)
            {
                if (logger.IsTraceEnabled)
                    logger.Trace("Recycling cache: " + cache.Keys.Count + " items in cache");

                var newCache = new Dictionary<string, string>();

                foreach (var visitedURL in visitedURLs)
                {
                    string value;
                    if (cache.TryGetValue(visitedURL, out value))
                        newCache.Add(visitedURL, value);
                }

                cache = newCache;
                visitedURLs.Clear();

                if (logger.IsTraceEnabled)
                    logger.Trace("Recycling cache: " + cache.Keys.Count + " items in cache");
            }
        }

        public void RemoveFromCache(string url)
        {
            lock (this)
            {
                cache.Remove(url);
            }
        }

        public string GetConsolePage(Project project)
        {
            var res = project.Url;
            var hasBuild = HasBuild(project.AllBuildDetails);
            if (hasBuild)
                res += "lastBuild/console";
            return res;
        }

        private bool HasBuild(AllBuildDetails allBuildDetails)
        {
            // no details, there is no build
            if (allBuildDetails == null)
                return false;
            // if there is a completed build, there is a build
            if (allBuildDetails.LastCompletedBuild != null)
                return true;
            // if there is a build in progress, there is a build
            var buildInProgress = allBuildDetails.Status.IsInProgress;
            return buildInProgress;
        }

        private bool ValidateServerCertificate(object sender, X509Certificate certificate,
                                               X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (ignoreUntrustedCertificate)
                return true;
            return sslPolicyErrors == SslPolicyErrors.None;
        }
    }
}