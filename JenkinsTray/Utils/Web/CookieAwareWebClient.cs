using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Jenkins.Tray.Utils.Web
{
    public class CookieAwareWebClient : WebClient
    {
        private CookieContainer cookieContainer = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
                ((HttpWebRequest)request).CookieContainer = cookieContainer;
            return request;
        }
    }
}
