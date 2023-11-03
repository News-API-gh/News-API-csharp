using NewsAPI.Constants;
using NewsAPI.Models.Requests;
using System;
using System.Collections.Generic;

namespace NewsAPI.Models
{
    /// <summary>
    /// Params for making a request to the /everything endpoint.
    /// </summary>
    public class EverythingRequest : BaseRequest
    {
        /// <summary>
        /// If you want to restrict the search to specific web domains, add these here. Example: nytimes.com.
        /// </summary>
        public List<string> Domains = new List<string>();
        
        /// <summary>
        /// The earliest date to retrieve articles from. Note that how far back you can go is constrained by your plan type. See newsapi.org/pricing for plan details.
        /// </summary>
        public DateTime? From { get; set; }
        
        /// <summary>
        /// The latest date to retrieve articles from.
        /// </summary>
        public DateTime? To { get; set; }

        /// <summary>
        /// How should the results be sorted? Relevancy = articles relevant to the Q param come first. PublishedAt = most recent articles come first. Publisher = popular publishers come first.
        /// </summary>
        public SortBys? SortBy { get; set; }
    }
}
