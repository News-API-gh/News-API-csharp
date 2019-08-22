using NewsAPI.Constants;
using NewsAPI.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace NewsAPI.UnitTests
{
    public class NewsAPITests
    {
        private NewsApiClient NewsApiClient;

        public NewsAPITests()
        {
            // set this
            var apiKey = Environment.GetEnvironmentVariable("NewsAPIKey");
            NewsApiClient = new NewsApiClient(apiKey);
        }

        [Fact]
        public void BasicEverythingRequestWorks()
        {
            var everythingRequest = new EverythingRequest
            {
                Q = "bitcoin"
            };

            var result = NewsApiClient.GetEverything(everythingRequest);

            Assert.Equal(Statuses.Ok, result.Status);
            Assert.True(result.TotalResults > 0);
            Assert.True(result.Articles.Count > 0);
            Assert.Null(result.Error);
        }

        [Fact]
        public void EverythingRequestWithDomainsWorks()
        {
            var everythingRequest = new EverythingRequest
            {
                Domains = new List<String>(new[] { "wsj.com", "nytimes.com" })
            };

            var result = NewsApiClient.GetEverything(everythingRequest);

            Assert.Equal(Statuses.Ok, result.Status);
            Assert.True(result.TotalResults > 0);
            Assert.True(result.Articles.Count > 0);
            Assert.Null(result.Error);
        }

        [Fact]
        public void ComplexEverythingRequestWorks()
        {
            var everythingRequest = new EverythingRequest
            {
                Q = "apple",
                SortBy = SortBys.PublishedAt,
                Language = Languages.EN
            };

            var result = NewsApiClient.GetEverything(everythingRequest);

            Assert.Equal(Statuses.Ok, result.Status);
            Assert.True(result.TotalResults > 0);
            Assert.True(result.Articles.Count > 0);
            Assert.Null(result.Error);
        }

        [Fact]
        public void BadEverythingRequestReturnsError()
        {
            var everythingRequest = new EverythingRequest
            {
                Q = "bitcoin"
            };

            var brokenClient = new NewsApiClient("nokey");

            var result = brokenClient.GetEverything(everythingRequest);

            Assert.Equal(Statuses.Error, result.Status);
            Assert.Null(result.Articles);
            Assert.NotNull(result.Error);
            Assert.Equal(ErrorCodes.ApiKeyInvalid, result.Error.Code);
        }

        [Fact]
        public void BasicTopHeadlinesRequestWorks()
        {
            var topHeadlinesRequest = new TopHeadlinesRequest();

            topHeadlinesRequest.Sources.Add("techcrunch");

            var result = NewsApiClient.GetTopHeadlines(topHeadlinesRequest);

            Assert.Equal(Statuses.Ok, result.Status);
            Assert.True(result.TotalResults > 0);
            Assert.True(result.Articles.Count > 0);
            Assert.Null(result.Error);
        }

        [Fact]
        public void BadTopHeadlinesRequestReturnsError()
        {
            var topHeadlinesRequest = new TopHeadlinesRequest();

            var brokenClient = new NewsApiClient("nokey");

            topHeadlinesRequest.Sources.Add("techcrunch");

            var result = brokenClient.GetTopHeadlines(topHeadlinesRequest);

            Assert.Equal(Statuses.Error, result.Status);
            Assert.Null(result.Articles);
            Assert.NotNull(result.Error);
            Assert.Equal(ErrorCodes.ApiKeyInvalid, result.Error.Code);
        }

        [Fact]
        public void BadTopHeadlinesRequestReturnsError2()
        {
            var topHeadlinesRequest = new TopHeadlinesRequest();

            topHeadlinesRequest.Sources.Add("techcrunch");
            topHeadlinesRequest.Country = Countries.AU;
            topHeadlinesRequest.Language = Languages.EN;

            var result = NewsApiClient.GetTopHeadlines(topHeadlinesRequest);

            Assert.Equal(Statuses.Error, result.Status);
            Assert.Null(result.Articles);
            Assert.NotNull(result.Error);
            Assert.Equal(ErrorCodes.ParametersIncompatible, result.Error.Code);
        }
    }
}
