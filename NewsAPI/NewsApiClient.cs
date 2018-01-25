using NewsAPI.Constants;
using NewsAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPI
{
    /// <summary>
    /// Use this to get results from NewsAPI.org.
    /// </summary>
    public class NewsApiClient
    {
        private string BASE_URL = "https://newsapi.org/v2/";

        private HttpClient HttpClient;

        private string ApiKey;

        /// <summary>
        /// Use this to get results from NewsAPI.org.
        /// </summary>
        /// <param name="apiKey">Your News API key. You can create one for free at https://newsapi.org.</param>
        public NewsApiClient(string apiKey)
        {
            ApiKey = apiKey;

            HttpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            HttpClient.DefaultRequestHeaders.Add("user-agent", "News-API-csharp/0.1");
            HttpClient.DefaultRequestHeaders.Add("x-api-key", ApiKey);
        }

        /// <summary>
        /// Query the /v2/top-headlines endpoint for live top news headlines.
        /// </summary>
        /// <param name="request">The params and filters for the request.</param>
        /// <returns></returns>
        public async Task<ArticlesResult> GetTopHeadlinesAsync(TopHeadlinesRequest request)
        {
            // build the querystring
            var queryParams = new List<string>();

            // q
            if (!string.IsNullOrWhiteSpace(request.Q))
            {
                queryParams.Add("q=" + request.Q);
            }

            // sources
            if (request.Sources.Count > 0)
            {
                queryParams.Add("sources=" + string.Join(",", request.Sources));
            }

            if (request.Category.HasValue)
            {
                queryParams.Add("category=" + request.Category.Value.ToString().ToLowerInvariant());
            }

            if (request.Language.HasValue)
            {
                queryParams.Add("language=" + request.Language.Value.ToString().ToLowerInvariant());
            }

            if (request.Country.HasValue)
            {
                queryParams.Add("country=" + request.Country.Value.ToString().ToLowerInvariant());
            }

            // page
            if (request.Page > 1)
            {
                queryParams.Add("page=" + request.Page);
            }

            // page size
            if (request.PageSize > 0)
            {
                queryParams.Add("pageSize=" + request.PageSize);
            }

            // join them together
            var querystring = string.Join("&", queryParams.ToArray());

            return await MakeRequest("top-headlines", querystring);
        }

        /// <summary>
        /// Query the /v2/top-headlines endpoint for live top news headlines.
        /// </summary>
        /// <param name="request">The params and filters for the request.</param>
        /// <returns></returns>
        public ArticlesResult GetTopHeadlines(TopHeadlinesRequest request)
        {
            return GetTopHeadlinesAsync(request).Result;
        }

        /// <summary>
        /// Query the /v2/everything endpoint for recent articles all over the web.
        /// </summary>
        /// <param name="request">The params and filters for the request.</param>
        /// <returns></returns>
        public async Task<ArticlesResult> GetEverythingAsync(EverythingRequest request)
        {
            // build the querystring
            var queryParams = new List<string>();

            // q
            if (!string.IsNullOrWhiteSpace(request.Q))
            {
                queryParams.Add("q=" + request.Q);
            }

            // sources
            if (request.Sources.Count > 0)
            {
                queryParams.Add("sources=" + string.Join(",", request.Sources));
            }

            // domains
            if (request.Domains.Count > 0)
            {
                queryParams.Add("domains=" + string.Join(",", request.Sources));
            }

            // from
            if (request.From.HasValue)
            {
                queryParams.Add("from=" + string.Format("{0:s}", request.From.Value));
            }

            // to
            if (request.To.HasValue)
            {
                queryParams.Add("to=" + string.Format("{0:s}", request.To.Value));
            }

            // language
            if (request.Language.HasValue)
            {
                queryParams.Add("language=" + request.Language.Value.ToString().ToLowerInvariant());
            }

            // sortBy
            if (request.SortBy.HasValue)
            {
                queryParams.Add("sortBy=" + request.SortBy.Value.ToString());
            }

            // page
            if (request.Page > 1)
            {
                queryParams.Add("page=" + request.Page);
            }

            // page size
            if (request.PageSize > 0)
            {
                queryParams.Add("pageSize=" + request.PageSize);
            }

            // join them together
            var querystring = string.Join("&", queryParams.ToArray());

            return await MakeRequest("everything", querystring);
        }

        /// <summary>
        /// Query the /v2/everything endpoint for recent articles all over the web.
        /// </summary>
        /// <param name="request">The params and filters for the request.</param>
        /// <returns></returns>
        public ArticlesResult GetEverything(EverythingRequest request)
        {
            return GetEverythingAsync(request).Result;
        }

        // ***

        private async Task<ArticlesResult> MakeRequest(string endpoint, string querystring)
        {
            // here's the return obj
            var articlesResult = new ArticlesResult();

            // make the http request
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, BASE_URL + endpoint + "?" + querystring);
            var httpResponse = await HttpClient.SendAsync(httpRequest);            

            var json = await httpResponse.Content?.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(json))
            {
                // convert the json to an obj
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json);
                articlesResult.Status = apiResponse.Status;
                if (articlesResult.Status == Statuses.Ok)
                {
                    articlesResult.TotalResults = apiResponse.TotalResults;
                    articlesResult.Articles = apiResponse.Articles;
                }
                else
                {
                    ErrorCodes errorCode = ErrorCodes.UnknownError;
                    try
                    {
                        errorCode = (ErrorCodes)apiResponse.Code;
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("The API returned an error code that wasn't expected: " + apiResponse.Code);
                    }

                    articlesResult.Error = new Error
                    {
                        Code = errorCode,
                        Message = apiResponse.Message
                    };
                }
            }
            else
            {
                articlesResult.Status = Statuses.Error;
                articlesResult.Error = new Error
                {
                    Code = ErrorCodes.UnexpectedError,
                    Message = "The API returned an empty response. Are you connected to the internet?"
                };
            }

            return articlesResult;
        }
    }
}
