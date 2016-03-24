using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Iesi.Collections.Generic;

namespace JenkinsTray.Entities
{
    public enum BuildCauseEnum
    {
        Unknown,
        SCM,
        Timer,
        User,
        UpstreamProject,
        RemoteHost
    }

    public class BuildCause
    {
        public string ShortDescription { get; set; }
        public string Starter { get; set; }
        public string UserID { get; set; }
        public BuildCauseEnum Cause;

        public BuildCause()
        {

        }
    }

    public class BuildCauses
    {
        public ISet<BuildCause> Causes;
        public bool? HasUniqueCauses { get; set; }

        public BuildCauses()
        {
            Causes = new HashedSet<BuildCause>();
            HasUniqueCauses = null;
        }

        public BuildCause FirstBuildCause
        {
            get
            {
                BuildCause first = null;
                if (HasUniqueCauses == true)
                {
                    using (IEnumerator<BuildCause> enumer = Causes.GetEnumerator())
                    {
                        if (enumer.MoveNext())
                            first = enumer.Current;
                    }
                }
                return first;
            }
        }

        public static void FillInBuildCauses(BuildDetails res, XmlDocument xml)
        {
            XmlNodeList causes = xml.SelectNodes("/*/action/cause");
            res.Causes = new BuildCauses();
            res.Causes.HasUniqueCauses = null;
            BuildCauseEnum causeEnum = BuildCauseEnum.Unknown;

            foreach (XmlNode causeNode in causes)
            {
                string causeShortDesc = causeNode["shortDescription"].InnerText;
                BuildCause cause = new BuildCause();
                cause.ShortDescription = causeShortDesc;

                if (causeShortDesc.StartsWith("Started by user"))
                {
                    cause.Cause = BuildCauseEnum.User;
                    var userName = causeNode["userName"];
                    if (userName != null && userName.InnerText.Length > 0)
                    {
                        cause.Starter = userName.InnerText.ToString();
                    }
                    var userID = causeNode["userId"];
                    if (userID != null && userID.InnerText.Length > 0)
                    {
                        cause.UserID = userID.InnerText.ToString();
                    }
                }
                else if (causeShortDesc.StartsWith("Started by Timer", StringComparison.CurrentCultureIgnoreCase))
                {
                    cause.Cause = BuildCauseEnum.Timer;
                }
                else if (causeShortDesc.StartsWith("Started by Upstream Project", StringComparison.CurrentCultureIgnoreCase))
                {
                    cause.Cause = BuildCauseEnum.UpstreamProject;
                    var upstreamProject = causeNode["upstreamProject"];
                    if (upstreamProject != null && upstreamProject.InnerText.Length > 0)
                    {
                        cause.Starter = upstreamProject.InnerText.ToString();
                    }
                }
                else if (causeShortDesc.StartsWith("Started by an SCM change", StringComparison.CurrentCultureIgnoreCase))
                {
                    cause.Cause = BuildCauseEnum.SCM;
                }
                else if (causeShortDesc.StartsWith("Started by remote host", StringComparison.CurrentCultureIgnoreCase))
                {
                    cause.Cause = BuildCauseEnum.RemoteHost;
                    string startedBy = @"Started by remote host ";
                    string remoteHost = causeShortDesc.Remove(0, startedBy.Length);
                    int index = remoteHost.IndexOf(" with note: ");
                    cause.Starter = index > 0 ? remoteHost.Remove(index) : remoteHost; 
                }
                else
                {
                    cause.Cause = BuildCauseEnum.Unknown;
                }
                if (res.Causes.HasUniqueCauses == null)
                {
                    causeEnum = cause.Cause;
                    res.Causes.HasUniqueCauses = true;
                }
                else
                {
                    if (cause.Cause != causeEnum)
                        res.Causes.HasUniqueCauses = false;
                }
                res.Causes.Causes.Add(cause);
            }
        }
    }
}
