using NewsAPI.Constants;
using System.Collections.Generic;

namespace NewsAPI.Models.Requests
{
    /// <summary>
    /// Base class contains params /everything and /top-headlines endpoints.
    /// </summary>
    public class BaseRequest
    {
        /// <summary>
        /// The keyword or phrase to search for. Boolean operators are supported.
        /// </summary>
        public string Q { get; set; }
        
        /// <summary>
        /// The fields to restrict your 'Q' search to. The possible options are: title, description, content. Multiple options can be specified by separating them with a comma, for example: title, content.
        /// </summary>
        public List<NewsSections> SearchIn = new List<NewsSections>();
        
        /// <summary>
        /// If you want to restrict the results to specific sources, add their Ids here. You can find source Ids with the /sources endpoint or on newsapi.org.
        /// </summary>
        public List<string> Sources = new List<string>();

        /// <summary>
        /// The language to restrict articles to.
        /// </summary>
        public Languages? Language { get; set; }

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
