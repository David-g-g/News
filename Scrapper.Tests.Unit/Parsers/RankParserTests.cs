using System;
using System.IO;
using Xunit;
using FluentAssertions;
using NSubstitute;
using AngleSharp;
using System.Threading.Tasks;

namespace Scrapper.Tests.Unit
{
    public class NewsParserTests 
    {
        private NewsParser parser;

        public NewsParserTests()
        {
            parser = new NewsParser();
        }
       
        [Fact]
        public void GivenNumericRank_WhenParseRankIsCalled_ThenIntegerRankIsReturned()
        {
            var isValid = parser.TryParseRank("10", out int rank);

            isValid.Should().BeTrue();
            rank.Should().Be(10);
        }

        [Fact]
        public void GivenNumericWithExtraDotRank_WhenParseRankIsCalled_ThenIntegerRankIsReturned()
        {
            var isValid = parser.TryParseRank("10.", out int rank);

            isValid.Should().BeTrue();
            rank.Should().Be(10);
        }

        [Theory]
        [InlineData("asda")]
        [InlineData("a10")]
        [InlineData("10a")]
        [InlineData("1a1")]
        public void GivenNotValidStringRank_WhenParseRankIsCalled_ThenFalseIsReturned(string rawRank)
        {
            var isValid = parser.TryParseRank(rawRank, out int rank);

            isValid.Should().BeFalse();
        }
    }
}
