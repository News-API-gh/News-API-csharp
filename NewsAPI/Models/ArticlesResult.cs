using System.Collections.Generic;
using NewsAPI.Constants;

namespace NewsAPI.Models
{
    public class ArticlesResult
    {
        public List<Article> Articles { get; set; }
        public Error Error { get; set; }
        public Statuses Status { get; set; }
        public int TotalResults { get; set; }
    }
}