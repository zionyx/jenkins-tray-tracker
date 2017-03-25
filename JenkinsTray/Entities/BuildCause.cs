using System;
using System.Xml;
using Spring.Collections.Generic;

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
        public BuildCauseEnum Cause;

        public string ShortDescription { get; set; }
        public string Starter { get; set; }
        public string UserID { get; set; }
    }

    public class BuildCauses
    {
        public ISet<BuildCause> Causes;

        public BuildCauses()
        {
            Causes = new HashedSet<BuildCause>();
            HasUniqueCauses = null;
        }

        public bool? HasUniqueCauses { get; set; }

        public BuildCause FirstBuildCause
        {
            get
            {
                BuildCause first = null;
                if (HasUniqueCauses == true)
                {
                    using (var enumer = Causes.GetEnumerator())
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
            var causes = xml.SelectNodes("/*/action/cause");
            res.Causes = new BuildCauses();
            res.Causes.HasUniqueCauses = null;
            var causeEnum = BuildCauseEnum.Unknown;

            foreach (XmlNode causeNode in causes)
            {
                var causeShortDesc = causeNode["shortDescription"].InnerText;
                var cause = new BuildCause();
                cause.ShortDescription = causeShortDesc;

                if (causeShortDesc.StartsWith("Started by user"))
                {
                    cause.Cause = BuildCauseEnum.User;
                    var userName = causeNode["userName"];
                    if (userName != null && userName.InnerText.Length > 0)
                    {
                        cause.Starter = userName.InnerText;
                    }
                    var userID = causeNode["userId"];
                    if (userID != null && userID.InnerText.Length > 0)
                    {
                        cause.UserID = userID.InnerText;
                    }
                }
                else if (causeShortDesc.StartsWith("Started by Timer", StringComparison.CurrentCultureIgnoreCase))
                {
                    cause.Cause = BuildCauseEnum.Timer;
                }
                else if (causeShortDesc.StartsWith("Started by Upstream Project",
                                                   StringComparison.CurrentCultureIgnoreCase))
                {
                    cause.Cause = BuildCauseEnum.UpstreamProject;
                    var upstreamProject = causeNode["upstreamProject"];
                    if (upstreamProject != null && upstreamProject.InnerText.Length > 0)
                    {
                        cause.Starter = upstreamProject.InnerText;
                    }
                }
                else if (causeShortDesc.StartsWith("Started by an SCM change",
                                                   StringComparison.CurrentCultureIgnoreCase))
                {
                    cause.Cause = BuildCauseEnum.SCM;
                }
                else if (causeShortDesc.StartsWith("Started by remote host",
                                                   StringComparison.CurrentCultureIgnoreCase))
                {
                    cause.Cause = BuildCauseEnum.RemoteHost;
                    var startedBy = @"Started by remote host ";
                    var remoteHost = causeShortDesc.Remove(0, startedBy.Length);
                    var index = remoteHost.IndexOf(" with note: ");
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