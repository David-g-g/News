using System;

namespace Scrapper
{
    public interface INewsParser
    {
        string ParseTitle(string title);
        bool TryParseRank(string rank, out int parsedRank);
        bool TryParseUrl(string url, out Uri parsedUrl);
        bool TryParsePoints (string point, out int parsedPoints);
        bool TryParseComments (string comments, out int parsedComments);
        string ParseUser (string user);
    }
}