using System;

namespace Scrapper
{
    public class NewsItem
    {
        public string Id { get; set; }
        public int Rank { get; set; }
        public string Title { get; set; }
        public Uri Url { get; set; }
        public int Points { get; set; }
        public int Comments { get; set; }
        public string User { get; set; }

        public bool IsValid(){
            return !string.IsNullOrEmpty(Title) &&
                   !string.IsNullOrEmpty(User) &&
                   Url != null &&
                   Rank >= 0 && 
                   Points >= 0 &&
                   Comments >= 0; 
        }
    }
}
