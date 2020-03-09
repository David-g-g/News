using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace Scrapper
{
    public class HackerNewsHtmlDocumentParser : IHtmlDocumentParser
    {
        private readonly INewsParser newsFieldsParser;

        public HackerNewsHtmlDocumentParser(INewsParser newsFieldsParser)
        {
            this.newsFieldsParser = newsFieldsParser;
        }
        public IList<NewsItem> ParseNews(IDocument document)
        {            
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
                        Title = newsFieldsParser.ParseTitle(titleAnchor?.TextContent),
                        Rank = ParseRank(currentNew, row),
                        Url = ParseUrl(currentNew, titleAnchor),
                        Id = row.Id
                    };

                }

                if (currentNew != null && row.QuerySelectorAll($"#score_{currentNew.Id}").Any())
                {
                    currentNew.Points = ParsePoints(currentNew, row);
                    currentNew.Comments = ParseComments(currentNew, row);
                    currentNew.User = newsFieldsParser.ParseUser(row.QuerySelector($".hnuser").TextContent);

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
            if (newsFieldsParser.TryParseComments(row.QuerySelectorAll("a").LastOrDefault().TextContent, out int comments))
            {
                return comments;
            }

            return 0;
        }

        private int ParsePoints(NewsItem currentNew, IElement row)
        {
            if (newsFieldsParser.TryParsePoints(row.QuerySelector($".score").TextContent, out int points))
            {
                return points;
            }

            return 0;
        }

        private Uri ParseUrl(NewsItem currentNew, IHtmlAnchorElement titleAnchor)
        {
            if (newsFieldsParser.TryParseUrl(titleAnchor.Href, out Uri url))
            {
                return url;
            }

            return null;
        }

        private int ParseRank(NewsItem currentNew, IElement row)
        {
            if (newsFieldsParser.TryParseRank(row.QuerySelector(".rank")?.TextContent, out int rank))
            {
                return rank;
            }

            return 0;
        }
    }
}