using System.Collections.Generic;

namespace Scrapper
{
    public class GetNewsQueryParam
    {
        public int PostNumber { get; set; }
    }
    public class GetNewsQueryResponse
    {
        public IList<NewsItem> News { get; set; }
    }
}