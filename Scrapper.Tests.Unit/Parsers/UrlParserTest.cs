using System;
using System.IO;
using Xunit;
using FluentAssertions;
using NSubstitute;
using AngleSharp;
using System.Threading.Tasks;

namespace Scrapper.Tests.Unit
{
    public class UrlParserTests 
    {
        private NewsParser parser;

        public UrlParserTests()
        {
            parser = new NewsParser();
        }
       
        [Theory]
        [InlineData("http://www.test.com")]
        [InlineData("https://www.test.com")]
        [InlineData("https://www.test.com/page1")]
        public void GivenValidUrl_WhenParseUrlIsCalled_ThenUrlIsReturned(string validUrl)
        {
            var isValid = parser.TryParseUrl(validUrl, out Uri uri);

            isValid.Should().BeTrue();
            uri.Should().BeEquivalentTo(new Uri(validUrl));
        }

        [Theory]
        [InlineData("")]
        [InlineData("www.test.com")]
        [InlineData("ftp://www.test.com")]
        [InlineData("testns")]
        public void GivenNotValidStringPoints_WhenParsePointsIsCalled_ThenFalseIsReturned(string invalidUrl)
        {
            var isValid = parser.TryParseUrl(invalidUrl, out Uri uri);

            isValid.Should().BeFalse();
        }
    }
}
