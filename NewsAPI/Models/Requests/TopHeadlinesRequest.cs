using NewsAPI.Constants;
using NewsAPI.Models.Requests;

namespace NewsAPI.Models
{
    /// <summary>
    /// Params for making a request to the /top-headlines endpoint.
    /// </summary>
    public class TopHeadlinesRequest : BaseRequest
    {
        /// <summary>
        /// If you want to restrict the headlines to a specific news category, add these here.
        /// </summary>
        public Categories? Category { get; set; }

        /// <summary>
        /// The country of the source to restrict articles to.
        /// </summary>
        public Countries? Country { get; set; }
    }
}
