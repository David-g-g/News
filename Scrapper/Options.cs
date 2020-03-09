using System;
using CommandLine;

namespace Scrapper
{
    public class Options
    {
        private int posts;

        [Option('p', "posts", Required = true, HelpText = "Number of posts to display (between 1 and 100)")]
        public int Posts
        {
            get { return posts; }
            set
            {
                if (value <= 0 || value > 100){
                    throw new ArgumentException($"Invalid posts number: {value}");
                }

                posts = value;
            }
        }

    }
}

