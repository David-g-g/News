# Introduction
Console app to pull [HackerNews](https://news.ycombinator.com/) and display in Json.

## Packages used:
[AgleSharp](https://anglesharp.github.io/):

Utility for parsing html documents. Not too popular but was easy to use with the css selector.

[CommandLineParser](https://github.com/commandlineparser/commandline):

Utility for parsing command line. Parsing command parameters is complicated, this tool simplify it.

[CommandLineParser](https://github.com/dotnet/extensions):

Ioc container.

[FluentAssertions](https://fluentassertions.com/):

Test assertions.I like to do the assertions in this format.

[Nsubstitute](https://nsubstitute.github.io/):

Mock framework. Avoids having to create your own mocks.

# Requirements
.net core >= 3.1

# Test
In root folder, execute:

dotnet test 

# Run
In folder Scrapper execute:

dotnet run --posts n 

Where n is number of posts to pull (between 1 and 100).
