using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using Common.Logging;
using JenkinsTray.Entities;
using JenkinsTray.Utils;
using JenkinsTray.Utils.Logging;

namespace JenkinsTray.BusinessComponents
{
    public class ClaimService
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public JenkinsService JenkinsService { get; set; }

        public static void FillInBuildDetails(BuildDetails res, XmlDocument xml)
        {
            var claimedNode = xml.SelectSingleNode("/*/action[claimed/text() = 'true']");
            if (claimedNode == null)
                return;

            var assignedByNode = claimedNode.SelectSingleNode("assignedBy");
            var reasonNode = claimedNode.SelectSingleNode("reason");
            var claimedByNode = claimedNode.SelectSingleNode("claimedBy");

            var claimDetails = new ClaimDetails();
            claimDetails.Assignor = assignedByNode.InnerText;
            claimDetails.Assignee = claimedByNode.InnerText;
            claimDetails.Reason = reasonNode != null ? reasonNode.InnerText : "";
            res.ClaimDetails = claimDetails;
        }

        public void ClaimBuild(Project project, BuildDetails buildDetails, string reason, bool sticky)
        {
            var url = NetUtils.ConcatUrls(project.Url, buildDetails.Number.ToString(), "/claim/claim");
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // we don't want to follow redirections
            request.AllowAutoRedirect = false;

            // currently supporting assigning to self
            var assignee = string.Empty;

            var credentials = project.Server.Credentials;
            if (credentials != null)
            {
                // claim plugin requests uses the username from the session cookie but not from request credentials
                request.Credentials = new NetworkCredential(credentials.Username, credentials.Password);
                assignee = credentials.Username;
            }

            try
            {
                using (var postStream = request.GetRequestStream())
                {
                    var claim = new ClaimDetailsDto
                    {
                        Assignee = assignee,
                        Reason = reason,
                        Sticky = sticky
                    };

                    var stream = new MemoryStream();
                    var serializer = new DataContractJsonSerializer(typeof(ClaimDetailsDto));
                    serializer.WriteObject(stream, claim);
                    var json = Encoding.UTF8.GetString(stream.ToArray());

                    var postData = "json=" + HttpUtility.UrlEncode(json, Encoding.UTF8);
                    using (var writer = new StreamWriter(postStream))
                    {
                        writer.Write(postData);
                    }
                }

                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.Found && response.StatusCode != HttpStatusCode.OK)
                        throw new Exception("Received response code " + response.StatusCode);
                }
            }
            catch (WebException webEx)
            {
                MessageBox.Show(webEx.Message, "Claim failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelper.LogError(logger, webEx);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, ex);
            }
            var buildUrl = NetUtils.ConcatUrls(project.Url, buildDetails.Number.ToString(), "/api/xml");
            JenkinsService.RemoveFromCache(buildUrl);
        }

        [DataContract]
        protected class ClaimDetailsDto
        {
            //  JENKINS-7824
            [DataMember(Name = "assignee")]
            public string Assignee { get; set; }

            [DataMember(Name = "reason")]
            public string Reason { get; set; }

            [DataMember(Name = "sticky")]
            public bool Sticky { get; set; }
        }
    }
}