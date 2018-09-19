using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NewsAPI.Constants;
using NewsAPI.Models;
using Newtonsoft.Json;

namespace NewsAPI
{
    /// <summary>
    ///     Use this to get results from NewsAPI.org.
    /// </summary>
    public class NewsApiClient
    {
        private const string BaseUrl = "https://newsapi.org/v2/";

        private readonly HttpClient _httpClient;

        /// <summary>
        ///     Use this to get results from NewsAPI.org.
        /// </summary>
        /// <param name="apiKey">Your News API key. You can create one for free at https://newsapi.org.</param>
        public NewsApiClient(string apiKey)
        {
            _httpClient = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });
            _httpClient.DefaultRequestHeaders.Add("user-agent", "News-API-csharp/0.1");
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }

        /// <summary>
        ///     Query the /v2/everything endpoint for recent articles all over the web.
        /// </summary>
        /// <param name="request">The params and filters for the request.</param>
        /// <returns></returns>
        public ArticlesResult GetEverything(EverythingRequest request)
        {
            return GetEverythingAsync(request).Result;
        }

        /// <summary>
        ///     Query the /v2/everything endpoint for recent articles all over the web.
        /// </summary>
        /// <param name="request">The params and filters for the request.</param>
        /// <returns></returns>
        public async Task<ArticlesResult> GetEverythingAsync(EverythingRequest request)
        {
            // build the queryString
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
                queryParams.Add("domains=" + string.Join(",", request.Domains));
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
                queryParams.Add("sortBy=" + request.SortBy.Value);
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
        ///     Query the /v2/top-headlines endpoint for live top news headlines.
        /// </summary>
        /// <param name="request">The params and filters for the request.</param>
        /// <returns></returns>
        public ArticlesResult GetTopHeadlines(TopHeadlinesRequest request)
        {
            return GetTopHeadlinesAsync(request).Result;
        }

        /// <summary>
        ///     Query the /v2/top-headlines endpoint for live top news headlines.
        /// </summary>
        /// <param name="request">The params and filters for the request.</param>
        /// <returns></returns>
        public async Task<ArticlesResult> GetTopHeadlinesAsync(TopHeadlinesRequest request)
        {
            // build the queryString
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

            return await MakeRequest("top-headlines", querystring).ConfigureAwait(false);
        }

        // ***

        private async Task<ArticlesResult> MakeRequest(string endpoint, string queryString)
        {
            // here's the return obj
            var articlesResult = new ArticlesResult();

            // make the http request
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, BaseUrl + endpoint + "?" + queryString);
            var httpResponse = await _httpClient.SendAsync(httpRequest).ConfigureAwait(false);

            if (httpResponse.Content != null)
            {
                var json = await (httpResponse.Content.ReadAsStringAsync()).ConfigureAwait(false);

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
                        var errorCode = ErrorCodes.UnknownError;

                        try
                        {
                            if (apiResponse.Code != null)
                            {
                                errorCode = (ErrorCodes) apiResponse.Code;
                            }
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
            }

            return articlesResult;
        }
    }
}