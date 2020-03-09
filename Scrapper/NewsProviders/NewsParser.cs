using System;

namespace Scrapper
{

    public class NewsParser : INewsParser
    {
        const int maxStringLegth = 256;

        public string ParseTitle(string title)
        {            
            return title.Length <= maxStringLegth ? title : title.Substring(0, maxStringLegth);
        }

        public string ParseUser(string user)
        {
            return user.Length <= maxStringLegth ? user : user.Substring(0, maxStringLegth);
        }

        public bool TryParseComments(string comments, out int parsedComments)
        {
            return int.TryParse(comments.Replace("comments","").Trim(), out parsedComments);
        }

        public bool TryParsePoints(string point, out int parsedPoints)
        {
            return int.TryParse(point.Replace("points","").Trim(), out parsedPoints);
        }

        public bool TryParseRank(string rank, out int parsedRank)
        {
            return int.TryParse(rank.Replace(".","").Trim(), out parsedRank);
        }

        public bool TryParseUrl(string url, out Uri parsedUrl)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out parsedUrl) && (parsedUrl.Scheme == Uri.UriSchemeHttp || parsedUrl.Scheme == Uri.UriSchemeHttps);
        }
    }
}