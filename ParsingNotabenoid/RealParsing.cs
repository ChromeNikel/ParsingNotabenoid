using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ParsingNotabenoid
{
    /// <summary>
    /// корректно работает только с завершенными переводами
    /// </summary>
    class RealParsing
    {   
        private List<string> lallstring = new List<string>();
        private List<string> listCode = new List<string>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">код первой страницы</param>
        /// <param name="selector">селектор до номера последней страницы</param>
        /// <returns></returns>
        public int getCountOfPage(string code, string selector)
        {
            var parser = new HtmlParser();
            IHtmlDocument doc = parser.Parse(code);
            var div0 = doc.QuerySelector(selector);
            if (div0 == null)
            {
                return 1;
            }
            else return Convert.ToInt32(div0.TextContent.Trim());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">код первой страницы</param>
        /// <param name="selector1">селектор до заголовка(общий)</param>
        /// <param name="selector2">селектор до абзаца в заголовке</param>
        /// <returns></returns>
        public string getName(string code, string selector1, string selector2)
        {
            var parser = new HtmlParser();
            IHtmlDocument doc = parser.Parse(code);
            var div0 = doc.QuerySelector(selector1);
            var div1 = doc.QuerySelector(selector2);
            string a0 = div0.TextContent.Trim();
            string a1 = div1.TextContent.Trim();
            byte[] bytes0 = Encoding.Default.GetBytes(a0);
            byte[] bytes1 = Encoding.Default.GetBytes(a1);
            a0 = Encoding.UTF8.GetString(bytes0);
            a1 = Encoding.UTF8.GetString(bytes1);
            
            return a0.Remove(0, a1.Length).Replace(":", "");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="URL">url первой страницы перевода</param>
        /// <param name="numberOfPage">номер страницы</param>
        /// <returns></returns>
        public string getURLNextPage(string URL, int numberOfPage)
        {
            return URL + "?Orig_page=" + numberOfPage.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wc">вебклиент с куками</param>
        /// <param name="URL">полный url текущей страницы перевода</param>
        /// <returns></returns>
        public string getCodePage(WebClientEx wc, string URL)
        {
            return wc.DownloadString(URL);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">код текущей страницы</param>
        /// <param name="selector">селектор текста перевода в одном блоке</param>
        public void Parse(string code, string selector)
        {
            var parser = new HtmlParser();
            IHtmlDocument doc = parser.Parse(code);

            var div0 = doc.QuerySelectorAll(selector).Where(item => item.ClassName.Contains("text"));

            foreach (var item in div0)
            {
                string a = item.TextContent.Trim();
                byte[] bytes = Encoding.Default.GetBytes(a);
                lallstring.Add(Encoding.UTF8.GetString(bytes));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">полный путь, включая имя файла</param>
        public void SaveText(string path)
        {
            File.WriteAllLines(path, lallstring);
        }
    }
}
