using System;
using System.IO;
using Xunit;
using FluentAssertions;
using NSubstitute;
using AngleSharp;
using System.Threading.Tasks;

namespace Scrapper.Tests.Unit
{
    public class PointsParserTests 
    {
        private NewsParser parser;

        public PointsParserTests()
        {
            parser = new NewsParser();
        }
       
        [Fact]
        public void GivenNumeriPoints_WhenParsePointsIsCalled_ThenIntegerPointsIsReturned()
        {
            var isValid = parser.TryParsePoints("10", out int points);

            isValid.Should().BeTrue();
            points.Should().Be(10);
        }

        [Fact]
        public void GivenValidFormatPoints_WhenParsePointsIsCalled_ThenIntegerRankIsReturned()
        {
            var isValid = parser.TryParsePoints("10 points", out int points);

            isValid.Should().BeTrue();
            points.Should().Be(10);
        }

        [Theory]
        [InlineData("")]
        [InlineData("as 10 points")]
        [InlineData("10 com")]
        [InlineData("nothing")]
        public void GivenNotValidStringPoints_WhenParsePointsIsCalled_ThenFalseIsReturned(string rawPoints)
        {
            var isValid = parser.TryParsePoints(rawPoints, out int points);

            isValid.Should().BeFalse();
        }
    }
}
