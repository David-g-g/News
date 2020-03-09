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
        private readonly INewsParser newsParser;

        private const int PageLimit = 20;

        public HackerNewsProvider(IAngleSharpPageLoader pageLoader, INewsParser newsParser)
        {
            this.pageLoader = pageLoader;
            this.newsParser = newsParser;
        }

        public async Task<IList<NewsItem>> GetNews(int postNumber)
        {
            var news = new List<NewsItem>();
            var pageNumber = 1;
        
            while (news.Count < postNumber && pageNumber < PageLimit)
            {
                var address = $"https://news.ycombinator.com/news?p={pageNumber}"; //Get from config

                news.AddRange(await GetNewsFromUrl(address));
                pageNumber++;
            }

            return news.Take(postNumber).ToList();
        }

        public async Task<IList<NewsItem>> GetNewsFromUrl(string url)
        {          
            var document = await pageLoader.LoadPage(url);
            
            var rows = document.QuerySelectorAll("table.itemlist tr");
            var news = new List<NewsItem>();
            NewsItem currentNew = null;

            foreach (var row in rows)
            {
                if (row.ClassName == "athing")
                {
                    IHtmlAnchorElement titleAnchor = row.QuerySelector("a.storylink") as IHtmlAnchorElement;

                    currentNew = new NewsItem
                    {
                        Title = newsParser.ParseTitle(titleAnchor?.TextContent),
                        Rank = ParseRank(currentNew, row),
                        Url = ParseUrl(currentNew, titleAnchor),
                        Id = row.Id
                    };

                }

                if (currentNew != null && row.QuerySelectorAll($"#score_{currentNew.Id}").Any())
                {
                    currentNew.Points = ParsePoints(currentNew, row);
                    currentNew.Comments = ParseComments(currentNew, row);
                    currentNew.User = newsParser.ParseUser(row.QuerySelector($".hnuser").TextContent);

                    if (currentNew.IsValid())
                    {
                        news.Add(currentNew);
                    }                    
                }
            }

            return news;

        }

        private int ParseComments(NewsItem currentNew, IElement row)
        {
            if (newsParser.TryParseComments(row.QuerySelectorAll("a").LastOrDefault().TextContent, out int comments))
            {
                return comments;
            }

            return 0;
        }

        private int ParsePoints(NewsItem currentNew, IElement row)
        {
            if (newsParser.TryParsePoints(row.QuerySelector($".score").TextContent, out int points))
            {
                return points;
            }

            return 0;
        }

        private Uri ParseUrl(NewsItem currentNew, IHtmlAnchorElement titleAnchor)
        {
            if (newsParser.TryParseUrl(titleAnchor.Href, out Uri url))
            {
                return url;
            }

            return null;
        }

        private int ParseRank(NewsItem currentNew, IElement row)
        {
            if (newsParser.TryParseRank(row.QuerySelector(".rank")?.TextContent, out int rank))
            {
                return rank;
            }

            return 0;
        }
    }
}
