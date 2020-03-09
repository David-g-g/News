using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;

namespace Scrapper.Tests.Integration
{
    public class GetNewsTests
    {
        [Fact]
        public async Task NewsAreFetched()
        {
            var serviceProvider = IocStartup.RegisterServices();

            var query = serviceProvider.GetService<IQuery<GetNewsQueryParam, GetNewsQueryResponse>>();

            var queryResponse = await query.Execute(new GetNewsQueryParam { PostNumber = 10 });                           

            queryResponse.News.Count.Should().BeGreaterThan(0);
        }
    }
}
