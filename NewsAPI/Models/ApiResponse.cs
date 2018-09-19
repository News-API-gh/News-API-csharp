using System.Collections.Generic;
using NewsAPI.Constants;

namespace NewsAPI.Models
{
    internal class ApiResponse
    {
        public List<Article> Articles { get; set; }
        public ErrorCodes? Code { get; set; }
        public string Message { get; set; }
        public Statuses Status { get; set; }
        public int TotalResults { get; set; }
    }
}