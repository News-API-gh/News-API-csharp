using NewsAPI.Constants;
using System.Collections.Generic;

namespace NewsAPI.Models
{
    /// <summary>
    /// Params for making a request to the /top-headlines endpoint.
    /// </summary>
    public class TopHeadlinesRequest
    {
        /// <summary>
        /// The keyword or phrase to search for. Boolean operators are supported.
        /// </summary>
        public string Q { get; set; }
        /// <summary>
        /// If you want to restrict the results to specific sources, add their Ids here. You can find source Ids with the /sources endpoint or on newsapi.org.
        /// </summary>
        public List<string> Sources = new List<string>();
        /// <summary>
        /// If you want to restrict the headlines to a specific news category, add these here.
        /// </summary>
        public Categories? Category { get; set; }
        /// <summary>
        /// The language to restrict articles to.
        /// </summary>
        public Languages? Language { get; set; }
        /// <summary>
        /// The country of the source to restrict articles to.
        /// </summary>
        public Countries? Country { get; set; }
        /// <summary>
        /// Each request returns a fixed amount of results. Page through them by increasing this.
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Set the max number of results to retrieve per request. The max is 100.
        /// </summary>
        public int PageSize { get; set; }
    }
}
