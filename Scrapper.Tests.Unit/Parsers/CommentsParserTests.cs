using System;
using System.IO;
using Xunit;
using FluentAssertions;
using NSubstitute;
using AngleSharp;
using System.Threading.Tasks;

namespace Scrapper.Tests.Unit
{
    public class CommentsParserTests 
    {
        private NewsParser parser;

        public CommentsParserTests()
        {
            parser = new NewsParser();
        }
       
        [Fact]
        public void GivenNumericomments_WhenParseCommentsIsCalled_ThenIntegerCommentsIsReturned()
        {
            var isValid = parser.TryParseComments("10", out int comments);

            isValid.Should().BeTrue();
            comments.Should().Be(10);
        }

        [Fact]
        public void GivenValidFormatPoints_WhenParsePointsIsCalled_ThenIntegerRankIsReturned()
        {
            var isValid = parser.TryParseComments("10 comments", out int comments);

            isValid.Should().BeTrue();
            comments.Should().Be(10);
        }

        [Theory]
        [InlineData("")]
        [InlineData("as 10 comments")]
        [InlineData("10 com")]
        [InlineData("nothing")]
        public void GivenNotValidStringPoints_WhenParsePointsIsCalled_ThenFalseIsReturned(string rawComments)
        {
            var isValid = parser.TryParseComments(rawComments, out int comments);

            isValid.Should().BeFalse();
        }
    }
}
