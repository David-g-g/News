using System;
using System.IO;
using Xunit;
using FluentAssertions;
using NSubstitute;
using AngleSharp;
using System.Threading.Tasks;
using System.Linq;

namespace Scrapper.Tests.Unit
{
    public class HackerNewsProviderTests: IAsyncLifetime
    {
        private IAngleSharpPageLoader pageLoader;
        private INewsParser hackerNewsFieldsParser;

         HackerNewsProvider newsProvider;

        public Task DisposeAsync()
        {
            return Task.FromResult(true);           
        }

        public async Task InitializeAsync()
        {
            var html = await File.ReadAllTextAsync("./hackernewsHtml.html");
            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(html));
            pageLoader = NSubstitute.Substitute.For<IAngleSharpPageLoader>();
            pageLoader.LoadPage(Arg.Any<string>()).Returns(document);
            hackerNewsFieldsParser = Substitute.For<INewsParser>();
            hackerNewsFieldsParser.ParseTitle(Arg.Any<string>()).Returns("Title");
            hackerNewsFieldsParser.ParseUser(Arg.Any<string>()).Returns("User");
            hackerNewsFieldsParser.TryParseUrl(Arg.Any<string>(), out Arg.Any<Uri>())
                                   .Returns(x => {  
                                       x[1] = new Uri("http://test.com");
                                       return true; });
            hackerNewsFieldsParser.TryParsePoints(Arg.Any<string>(), out Arg.Any<int>())
                                   .Returns(x => {  
                                       x[1] = 10;
                                       return true; });
            hackerNewsFieldsParser.TryParseRank(Arg.Any<string>(), out Arg.Any<int>())
                                   .Returns(x => {  
                                       x[1] = 20;
                                       return true; });
            hackerNewsFieldsParser.TryParseComments(Arg.Any<string>(), out Arg.Any<int>())
                                   .Returns(x => {  
                                       x[1] = 30;
                                       return true; });

            newsProvider = new HackerNewsProvider(pageLoader, hackerNewsFieldsParser);

        }

        [Fact]
        public async Task GivenNews_WhenGetNewsIsCalled_ThenNewsAreReturned()
        {
            var newsProvider = new HackerNewsProvider(pageLoader, hackerNewsFieldsParser);
            var news = await newsProvider.GetNews(1);

            news.First().Should().BeEquivalentTo (new NewsItem{
                Id="22504106",
                Title = "Title",
                User = "User",
                Points = 10,
                Rank = 20,
                Comments = 30,
                Url = new Uri("http://test.com") 
            });
        }

        [Fact]
        public async Task GivenNumberOfRequestedNews_WhenGetNewsIsCalled_ThenNumberOfRequestedNewsAreReturned()
        {
            var numberOfNews = 20;
            var news = await newsProvider.GetNews(numberOfNews);
            
            news.Count.Should().Be(20);
        }

        [Fact]
        public async Task GivenNumberOfRequestedNewsIsMoreThanOnePage_WhenGetNewsIsCalled_ThenNumberOfRequestedNewsAreReturned()
        {
            var numberOfNews = 60;
            var news = await newsProvider.GetNews(numberOfNews);
            
            news.Count.Should().Be(numberOfNews);
        }
    }
}
