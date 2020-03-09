using System;
using AngleSharp;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using AngleSharp.Html.Dom;
using AngleSharp.Dom;

namespace Scrapper
{
    public class HackerNewsProvider : INewsProvider
    {
        private readonly IAngleSharpPageLoader pageLoader;
        private readonly IHtmlDocumentParser htmlDocumentParser;

        private const int PageLimit = 20;

        public HackerNewsProvider(IAngleSharpPageLoader pageLoader, IHtmlDocumentParser htmlDocumentParser)
        {
            this.pageLoader = pageLoader;
            this.htmlDocumentParser = htmlDocumentParser;
        }

        public async Task<IList<NewsItem>> GetNews(int postNumber)
        {
            var newsList = new List<NewsItem>();
            var pageNumber = 1;
            var moreNews = true;

            while (newsList.Count < postNumber && pageNumber < PageLimit && moreNews) 
            {
                var address = $"https://news.ycombinator.com/news?p={pageNumber}"; //Get from config

                var document = await pageLoader.LoadPage(address);

                var news = htmlDocumentParser.ParseNews(document);

                if (!news.Any())
                {
                    moreNews = false;
                    continue;
                }

                newsList.AddRange(news);

                pageNumber++;
            }

            return newsList.Take(postNumber).ToList();
        }       
    }
}
