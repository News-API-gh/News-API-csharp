# News API client library for .NET (C#)
News API is a simple HTTP REST API for searching and retrieving live articles from all over the web. It can help you answer questions like:

- What top stories is the NY Times running right now?
- What new articles were published about the next iPhone today?
- Has my company or product been mentioned or reviewed by any blogs recently?

You can search for articles with any combination of the following criteria:

- Keyword or phrase. Eg: find all articles containing the word 'Microsoft'.
- Date published. Eg: find all articles published yesterday.
- Source name. Eg: find all articles by 'TechCrunch'.
- Source domain name. Eg: find all articles published on nytimes.com.
- Language. Eg: find all articles written in English.

You can sort the results in the following orders:

- Date published
- Relevancy to search keyword
- Popularity of source

You need an API key to use the API - this is a unique key that identifies your requests. They're free for development, open-source, and non-commercial use. You can get one here: [https://newsapi.org](https://newsapi.org).

## Installation
The News API client library is available on Nuget. You can install it either through the Nuget package management window in Visual Studio by searching for NewsAPI, or by running the following in the package manager console:
```shell
PM> Install-Package NewsAPI
```

## Usage example
```csharp
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;

namespace MyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var newsApiClient = new NewsApiClient("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = "Apple",
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = new DateTime(2018, 1, 25)
            });

            if (articlesResponse.Status == Statuses.Ok)
            {
                // total results found
                Console.WriteLine(articlesResponse.TotalResults);

                // here's the first 20
                foreach (var article in articlesResponse.Articles)
                {
                    // title
                    Console.WriteLine(article.Title);
                    // author
                    Console.WriteLine(article.Author);
                    // description
                    Console.WriteLine(article.Description);
                    // url
                    Console.WriteLine(article.Url);
                    // image
                    Console.WriteLine(article.UrlToImage);
                    // published at
                    Console.WriteLine(article.PublishedAt);
                }
            }

            Console.ReadLine();
        }
    }
}
```
