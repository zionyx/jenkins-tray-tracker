using System;
using System.Net;

namespace JenkinsTray.Utils.Web
{
    public class CookieAwareWebClient : WebClient
    {
        private readonly CookieContainer cookieContainer = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
                ((HttpWebRequest) request).CookieContainer = cookieContainer;
            return request;
        }
    }
}