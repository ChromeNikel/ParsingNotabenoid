using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParsingNotabenoid
{
    class Settings
    {   
    /// <summary>
    /// просто переменная измененного клиента
    /// </summary>
        private WebClientEx wc;
        private string nameCookie;
        private string valueCookie;
        /// <summary>
        /// клиент с куками
        /// </summary>
        public WebClientEx webcl
        {
            set
            {
                wc = value;
            }
            get
            {
                return SetCookies();
            }
        }
        /// <summary>
        /// настраиваем куки для доступа к сайту
        /// </summary>
        /// <returns></returns>
        private WebClientEx SetCookies()
        {
            wc = new WebClientEx();
            CookieContainer cc = new CookieContainer();
            using (StreamReader sr = new StreamReader("Cookies.txt"))
            {
                while (!sr.EndOfStream)
                {
                    var str = sr.ReadLine();
                    nameCookie = str.Substring(0, str.IndexOf(','));
                    str = str.Remove(0, str.IndexOf(',') + 1);
                    valueCookie = str;
                    cc.Add(new Uri("http://notabenoid.org/"), new Cookie(nameCookie, valueCookie));
                }
            }           
            wc.Cookies = cc;
            return wc;
        }
    }
}
