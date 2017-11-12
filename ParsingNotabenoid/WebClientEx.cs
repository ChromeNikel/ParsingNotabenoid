using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParsingNotabenoid
{
    class WebClientEx : WebClient
    {
        private CookieContainer _cookies;
        private string _ref;
        public WebClientEx()
        {
            _cookies = new CookieContainer();
        }
        public CookieContainer Cookies
        {
            get { return _cookies; }
            set { _cookies = value; }
        }
        protected override WebRequest GetWebRequest(System.Uri address)
        {
            var webReq = base.GetWebRequest(address);
            if (webReq is HttpWebRequest)
            {
                var req = (HttpWebRequest)webReq;
                req.CookieContainer = _cookies;
                if (_ref != null)
                {
                    req.Referer = _ref;
                }
            }
            _ref = address.ToString();
            return webReq;
        }
        protected override void Dispose(bool disposing)
        {
            _cookies = null;
            base.Dispose(disposing);
        }

    }
}
