using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace lab2_html_mailto_parser.Handlers
{
    class WebPageHandler
    {
        public string Url { get; }

        public WebPageHandler(string url)
        {
            Url = url;
        }

        public string GetPageHtml()
        {
            using WebClient client = new WebClient();
            return client.DownloadString(Url);
        }

        public List<string> GetHrefMailToAddresses()
        {
            List<string> emailAddresses = new List<string>();

            var pageHtml = GetPageHtml();
            var htmlParses = new HtmlParser();
            var document = htmlParses.ParseDocument(pageHtml);

            foreach (IElement element in document.QuerySelectorAll("a"))
            {
                string link = element.GetAttribute("href");

                if (!string.IsNullOrEmpty(link) && link.Contains("mailto:"))
                {
                    string email = link.Substring(link.IndexOf(':') + 1);

                    if (!emailAddresses.Contains(email))
                    {
                        emailAddresses.Add(email);
                    }
                }
            }

            return emailAddresses;
        }
    }
}
