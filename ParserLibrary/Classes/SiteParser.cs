using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ParserLibrary
{
    public class SiteParser : ISiteParser
    {
        private WebClient Client { get; set; }
        private HtmlParser Parser { get; set; }

        public SiteParser()
        {
            Client = new WebClient();
            Client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:60.0) Gecko/20100101 Firefox/60.0");
            Client.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            Client.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");

            Parser = new HtmlParser();
        }

        public int GetPageNumber(string link, string request, int lotsOnPage, Encoding encoding = null)
        {
            var html = GetPage(link, encoding);

            if (html == null)
                return 0;

            var document = Parser.Parse(html);
            var countObj = document.QuerySelector(request);

            if (countObj == null)
                return 0;

            var lotsNumber = Int32.Parse(GetNumbers(countObj.TextContent));
            var pageCountD = (double)lotsNumber / lotsOnPage;

            // округляем в большую сторону
            return Convert.ToInt32(Math.Ceiling(pageCountD));
        }

        public string GetTextFromElement(IElement element, string request)
        {
            return element.QuerySelector(request).TextContent;
        }

        public string GetPriceFromElement(IElement element, string request)
        {
            return GetNumbers(element.QuerySelector(request).TextContent);
        }

        public string GetStringFromAttribute(IElement element, string request, string attribute, string prefix = "")
        {          
            return $"{prefix}{element.QuerySelector(request).GetAttribute(attribute).ToString()}";
        }

        public IHtmlCollection<IElement> GetAllElement(string link, string request, Encoding encoding = null)
        {
            var html = GetPage(link, encoding);

            if (html == null)
                return null;

            var document = Parser.Parse(html);

            return document.QuerySelectorAll(request);
        }

        public string GetCityFromLink(string link, int deleteSymbolsCount)
        {
            link = link.Remove(0, deleteSymbolsCount);

            // оставляем из ссылки только город
            return link.Remove(link.IndexOf("/"));
        }

        public string GetPage(string link, Encoding encoding)
        {
            string html = "";

            try
            {
                using (Stream stream = Client.OpenRead(link))
                {
                    if (encoding != null)
                    {
                        using (StreamReader reader = new StreamReader(stream, encoding))
                        {
                            html = reader.ReadToEnd();
                        }
                    }
                    else
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            html = reader.ReadToEnd();
                        }
                    }
                }                
            }
            catch
            {
                Console.WriteLine($"Не удалось подключиться к {link}\r\nВозможно, пропало соединение с интернетом или сайт недоступен. Выполнение данной очереди будет прекращено.");
                Thread.CurrentThread.Abort();
            }

            return html;
        }

        public string GetNumbers(string text)
        {
            return Regex.Replace(text, "[^0-9]+", "");
        }
    }
}
