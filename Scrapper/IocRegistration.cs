using Microsoft.Extensions.DependencyInjection;

namespace Scrapper
{
    public class IocStartup
    {

        public static ServiceProvider RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<INewsParser, NewsParser>();
            collection.AddScoped<IAngleSharpPageLoader, AngleSharpPageLoader>();
            collection.AddScoped<INewsProvider, HackerNewsProvider>();
            collection.AddScoped<IQuery<GetNewsQueryParam, GetNewsQueryResponse>, GetNewsQuery>();

            return collection.BuildServiceProvider();
        }
    }
}