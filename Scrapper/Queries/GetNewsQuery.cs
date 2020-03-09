using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scrapper
{
    public class GetNewsQuery : IQuery<GetNewsQueryParam, GetNewsQueryResponse>
    {
        private readonly INewsProvider newsProvider;

        public GetNewsQuery(INewsProvider newsProvider)
        {
            this.newsProvider = newsProvider;
        }
        public async Task<GetNewsQueryResponse> Execute(GetNewsQueryParam parameters)
        {
            return new GetNewsQueryResponse
            {
                News = await newsProvider.GetNews(parameters.PostNumber)
            };
        }
    }
}