using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewsAPI.Constants;
using NewsAPI.Models;
using System.Configuration;

namespace NewsAPI.Tests
{
    [TestClass]
    public class Tests
    {
        // FIRST: Set your API key in the config file

        private NewsApiClient NewsApiClient;

        [TestInitialize]
        public void Init()
        {
            // set this
            var apiKey = ConfigurationManager.AppSettings["apiKey"];
            NewsApiClient = new NewsApiClient(apiKey);
        }

        [TestMethod]
        public void BasicEverythingRequestWorks()
        {
            var everythingRequest = new EverythingRequest
            {
                Q = "bitcoin"
            };

            var result = NewsApiClient.GetEverything(everythingRequest);

            Assert.AreEqual(Statuses.Ok, result.Status);
            Assert.IsTrue(result.TotalResults > 0);
            Assert.IsTrue(result.Articles.Count > 0);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public void ComplexEverythingRequestWorks()
        {
            var everythingRequest = new EverythingRequest
            {
                Q = "space",
                SortBy = SortBys.PublishedAt,
                Language = Languages.EN,
                SearchIn = { NewsSections.Description, NewsSections.Title }
            };

            var result = NewsApiClient.GetEverything(everythingRequest);

            Assert.AreEqual(Statuses.Ok, result.Status);
            Assert.IsTrue(result.TotalResults > 0);
            Assert.IsTrue(result.Articles.Count > 0);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public void BadEverythingRequestReturnsError()
        {
            var everythingRequest = new EverythingRequest
            {
                Q = "bitcoin"
            };

            var brokenClient = new NewsApiClient("nokey");

            var result = brokenClient.GetEverything(everythingRequest);

            Assert.AreEqual(Statuses.Error, result.Status);
            Assert.IsNull(result.Articles);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual(ErrorCodes.ApiKeyInvalid, result.Error.Code);
        }

        [TestMethod]
        public void BasicTopHeadlinesRequestWorks()
        {
            var topHeadlinesRequest = new TopHeadlinesRequest();

            topHeadlinesRequest.Sources.Add("techcrunch");

            var result = NewsApiClient.GetTopHeadlines(topHeadlinesRequest);

            Assert.AreEqual(Statuses.Ok, result.Status);
            Assert.IsTrue(result.TotalResults > 0);
            Assert.IsTrue(result.Articles.Count > 0);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public void BadTopHeadlinesRequestReturnsError()
        {
            var topHeadlinesRequest = new TopHeadlinesRequest();

            var brokenClient = new NewsApiClient("nokey");

            topHeadlinesRequest.Sources.Add("techcrunch");

            var result = brokenClient.GetTopHeadlines(topHeadlinesRequest);

            Assert.AreEqual(Statuses.Error, result.Status);
            Assert.IsNull(result.Articles);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual(ErrorCodes.ApiKeyInvalid, result.Error.Code);
        }

        [TestMethod]
        public void BadTopHeadlinesRequestReturnsError2()
        {
            var topHeadlinesRequest = new TopHeadlinesRequest();

            topHeadlinesRequest.Sources.Add("techcrunch");
            topHeadlinesRequest.Country = Countries.AU;
            topHeadlinesRequest.Language = Languages.EN;

            var result = NewsApiClient.GetTopHeadlines(topHeadlinesRequest);

            Assert.AreEqual(Statuses.Error, result.Status);
            Assert.IsNull(result.Articles);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual(ErrorCodes.ParametersIncompatible, result.Error.Code);
        }
    }
}
