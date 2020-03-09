using System;
using AngleSharp;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using AngleSharp.Html.Dom;
using AngleSharp.Dom;

namespace Scrapper
{
    public class AngleSharpPageLoader : IAngleSharpPageLoader
    {
        public async Task<IDocument> LoadPage(string url)
        {

            var config = Configuration.Default.WithDefaultLoader();
            var address = url;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);

            return document;
        }
    }
}