using NewsAPI.Constants;

namespace NewsAPI.Models
{
    public class Error
    {
        public ErrorCodes Code { get; set; }
        public string Message { get; set; }
    }
}
