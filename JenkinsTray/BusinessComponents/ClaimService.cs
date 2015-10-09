using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web;
using JenkinsTray.Entities;
using JenkinsTray.Utils;
using System.Xml;

namespace JenkinsTray.BusinessComponents
{
    public class ClaimService
    {
        public JenkinsService JenkinsService { get; set; }

        public static void FillInBuildDetails(BuildDetails res, XmlDocument xml)
        {
            XmlNode claimedNode = xml.SelectSingleNode("/*/action[claimed/text() = 'true']");
            if (claimedNode == null)
                return;

            var reasonNode = claimedNode.SelectSingleNode("reason");
            var claimedByNode = claimedNode.SelectSingleNode("claimedBy");

            ClaimDetails claimDetails = new ClaimDetails();
            claimDetails.User = claimedByNode.InnerText;
            claimDetails.Reason = reasonNode != null ? reasonNode.InnerText : "";
            res.ClaimDetails = claimDetails;
        }

        public void ClaimBuild(Project project, BuildDetails buildDetails, string reason, bool sticky)
        {
            string url = NetUtils.ConcatUrls(project.Url, buildDetails.Number.ToString(), "/claim/claim");
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // we don't want to follow redirections
            request.AllowAutoRedirect = false;

            var credentials = project.Server.Credentials;
            if (credentials != null)
                request.Credentials = new NetworkCredential(credentials.Username, credentials.Password);

            using (Stream postStream = request.GetRequestStream())
            {
                var claim = new ClaimDetailsDto
                {
                    Reason = reason,
                    Sticky = sticky
                };

                var stream = new MemoryStream();
                var serializer = new DataContractJsonSerializer(typeof(ClaimDetailsDto));
                serializer.WriteObject(stream, claim);
                string json = Encoding.UTF8.GetString(stream.ToArray());

                string postData = "json=" + HttpUtility.UrlEncode(json, Encoding.UTF8);
                using (var writer = new StreamWriter(postStream))
                {
                    writer.Write(postData);
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.Found && response.StatusCode != HttpStatusCode.OK)
                    throw new Exception("Received response code " + response.StatusCode);
            }

            string buildUrl = NetUtils.ConcatUrls(project.Url, buildDetails.Number.ToString(), "/api/xml");
            JenkinsService.RemoveFromCache(buildUrl);
        }

        [DataContract]
        protected class ClaimDetailsDto
        {
            [DataMember(Name = "reason")]
            public string Reason { get; set; }
            [DataMember(Name = "sticky")]
            public bool Sticky { get; set; }
        }
    }
}
