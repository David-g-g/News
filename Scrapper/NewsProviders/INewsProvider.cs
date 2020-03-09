using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scrapper
{
    public interface INewsProvider
    {
        Task<IList<NewsItem>> GetNews(int limit);
    }
}