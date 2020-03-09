using System;
using System.IO;
using Xunit;
using FluentAssertions;
using NSubstitute;
using AngleSharp;
using System.Threading.Tasks;
using System.Linq;
using AngleSharp.Dom;
using System.Collections.Generic;

namespace Scrapper.Tests.Unit
{
    public class HackerNewsProviderTests
    {
        private IAngleSharpPageLoader pageLoader;
        private IHtmlDocumentParser hackerNewsHtmlDocumentParser;
        private HackerNewsProvider newsProvider;

        public HackerNewsProviderTests()
        {
            pageLoader = NSubstitute.Substitute.For<IAngleSharpPageLoader>();
            hackerNewsHtmlDocumentParser = Substitute.For<IHtmlDocumentParser>();
            newsProvider = new HackerNewsProvider(pageLoader, hackerNewsHtmlDocumentParser);
        }

        [Fact]
        public async Task GivenNumberOfRequestedNews_WhenGetNewsIsCalled_ThenNumberOfRequestedNewsAreReturned()
        {
            var numberOfNews = 2;
            var document = Substitute.For<IDocument>();
            pageLoader.LoadPage(Arg.Is<string>(s => s.Contains("p=1"))).Returns(document);

            hackerNewsHtmlDocumentParser.ParseNews(document)
                                        .Returns(new List<NewsItem> { new NewsItem(), new NewsItem(), new NewsItem() });

            var news = await newsProvider.GetNews(numberOfNews);

            news.Count.Should().Be(numberOfNews);
        }

        [Fact]
        public async Task GivenNumberOfRequestedNewsIsMoreThanOnePage_WhenGetNewsIsCalled_ThenNumberOfRequestedNewsAreReturned()
        {
            var numberOfNews = 4;
            var document1 = Substitute.For<IDocument>();
            var document2 = Substitute.For<IDocument>();
            pageLoader.LoadPage(Arg.Is<string>(s => s.Contains("p=1"))).Returns(document1);
            pageLoader.LoadPage(Arg.Is<string>(s => s.Contains("p=2"))).Returns(document2);

            hackerNewsHtmlDocumentParser.ParseNews(document1)
                                        .Returns(new List<NewsItem> { new NewsItem { Id = "1" }, new NewsItem { Id = "2" }, new NewsItem { Id = "3" } });

            hackerNewsHtmlDocumentParser.ParseNews(document2)
                                        .Returns(new List<NewsItem> { new NewsItem { Id = "4" }, new NewsItem { Id = "5" }, new NewsItem { Id = "6" } });

            var news = await newsProvider.GetNews(numberOfNews);

            news.Count.Should().Be(numberOfNews);

            news.Select(n => n.Id).ToList().Should().BeEquivalentTo(new string[] { "1", "2", "3", "4" });
        }

         [Fact]
        public async Task GivenNotEnoughNews_WhenGetNewsIsCalled_ThenNumberOfAvailableNewsAreReturned()
        {
            var numberOfNews = 10;
            var document = Substitute.For<IDocument>();
            pageLoader.LoadPage(Arg.Is<string>(s => s.Contains("p=1"))).Returns(document);

            hackerNewsHtmlDocumentParser.ParseNews(document)
                                        .Returns(new List<NewsItem> { new NewsItem(), new NewsItem(), new NewsItem() });

            var news = await newsProvider.GetNews(numberOfNews);

            news.Count.Should().Be(3);
        }
    }
}
