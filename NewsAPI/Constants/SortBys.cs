namespace NewsAPI.Constants
{
    public enum SortBys
    {
        /// <summary>
        /// Sort by publisher popularity
        /// </summary>
        Popularity,
        /// <summary>
        /// Sort by article publish date (newest first)
        /// </summary>
        PublishedAt,
        /// <summary>
        /// Sort by relevancy to the Q param
        /// </summary>
        Relevancy
    }
}
