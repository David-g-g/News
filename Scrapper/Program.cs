using System;
using AngleSharp;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using AngleSharp.Html.Dom;
using CommandLine;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Scrapper
{
    public class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(o =>
            {
                _serviceProvider = IocStartup.RegisterServices();

                var query = _serviceProvider.GetService<IQuery<GetNewsQueryParam, GetNewsQueryResponse>>();

                var news = query.Execute(new GetNewsQueryParam { PostNumber = o.Posts })
                                .GetAwaiter()
                                .GetResult();

                Console.WriteLine(JsonSerializer.Serialize(news, new JsonSerializerOptions { WriteIndented = true }));
            });
        }        
    }
}

