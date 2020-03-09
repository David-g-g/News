using System.Collections.Generic;
using AngleSharp.Dom;

namespace Scrapper
{
    public interface IHtmlDocumentParser
    {
        IList<NewsItem> ParseNews(IDocument document);
    }
}