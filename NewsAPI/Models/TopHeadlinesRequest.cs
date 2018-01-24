using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPI.Models
{
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
    }
}
