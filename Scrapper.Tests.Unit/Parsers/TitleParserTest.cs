using System;
using System.IO;
using Xunit;
using FluentAssertions;
using NSubstitute;
using AngleSharp;
using System.Threading.Tasks;

namespace Scrapper.Tests.Unit
{
    public class TitleParserTests 
    {
        private NewsParser parser;

        public TitleParserTests()
        {
            parser = new NewsParser();
        }
       
        [Fact]
        public void GivenTitleWithMoreThan256Characters_WhenParseTitleIsCalled_ThenTruncatedTitleIsReturned()
        {
            string title = new string('*', 257);
            var parsedTitle = parser.ParseTitle(title);
            
            parsedTitle.Length.Should().Be(256);
        }

        [Fact]
        public void GivenTitleWith256Characters_WhenParseTitleIsCalled_ThenUntouchedTitleIsReturned()
        {
            string title = new string('*', 256);
            var parsedTitle = parser.ParseTitle(title);
            
            parsedTitle.Should().Be(title);
        }

        [Fact]
        public void GivenTitleWithLessThan256Characters_WhenParseTitleIsCalled_ThenUntouchedTitleIsReturned()
        {
            string title = new string('*', 255);
            var parsedTitle = parser.ParseTitle(title);
            
            parsedTitle.Should().Be(title);
        }       
    }
}
