using System;
using System.IO;
using Xunit;
using FluentAssertions;
using NSubstitute;
using AngleSharp;
using System.Threading.Tasks;

namespace Scrapper.Tests.Unit
{
    public class UserParserTests 
    {
        private NewsParser parser;

        public UserParserTests()
        {
            parser = new NewsParser();
        }
       
        [Fact]
        public void GivenUserithMoreThan256Characters_WhenParseUserIsCalled_ThenTruncatedTitleIsReturned()
        {
            string user = new string('*', 257);
            var parsedUser = parser.ParseUser(user);
            
            parsedUser.Length.Should().Be(256);
        }

        [Fact]
        public void GivenTitleWith256Characters_WhenParseUserIsCalled_ThenUntouchedTitleIsReturned()
        {
            string title = new string('*', 256);
            var parsedUser = parser.ParseTitle(title);
            
            parsedUser.Should().Be(title);
        }

        [Fact]
        public void GivenTitleWithLessThan256Characters_WhenParseUserIsCalled_ThenUntouchedTitleIsReturned()
        {
            string title = new string('*', 255);
            var parsedUser = parser.ParseTitle(title);
            
            parsedUser.Should().Be(title);
        }       
    }
}
