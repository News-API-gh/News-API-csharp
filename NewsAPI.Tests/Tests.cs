﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewsAPI.Models;
using System.Configuration;

namespace NewsAPI.Tests
{
    [TestClass]
    public class Tests
    {
        // FIRST: Set your API key in the config file

        private Client NewsApiClient;

        [TestInitialize]
        public void Init()
        {
            // set this
            var apiKey = Environment.GetEnvironmentVariable("NewsAPIKey");
            NewsApiClient = new Client(apiKey);
        }

        [TestMethod]
        public void BasicEverythingRequestWorks()
        {
            var everythingRequest = new EverythingRequest
            {
                Q = "bitcoin"
            };

            var result = NewsApiClient.Everything(everythingRequest);

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

            var brokenClient = new Client("nokey");

            var result = brokenClient.Everything(everythingRequest);

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

            var result = NewsApiClient.TopHeadlines(topHeadlinesRequest);

            Assert.AreEqual(Statuses.Ok, result.Status);
            Assert.IsTrue(result.TotalResults > 0);
            Assert.IsTrue(result.Articles.Count > 0);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public void BadTopHeadlinesRequestReturnsError()
        {
            var topHeadlinesRequest = new TopHeadlinesRequest();

            var brokenClient = new Client("nokey");

            topHeadlinesRequest.Sources.Add("techcrunch");

            var result = brokenClient.TopHeadlines(topHeadlinesRequest);

            Assert.AreEqual(Statuses.Error, result.Status);
            Assert.IsNull(result.Articles);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual(ErrorCodes.ApiKeyInvalid, result.Error.Code);
        }
    }
}
