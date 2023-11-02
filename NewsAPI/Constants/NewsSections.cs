namespace NewsAPI.Constants
{
    public sealed class NewsSections
    {
        public readonly string m_Name;

        private NewsSections(string name)
        {
            this.m_Name = name;
        }

        public static NewsSections Title = new NewsSections("title");
        public static NewsSections Description = new NewsSections("description");
        public static NewsSections Content = new NewsSections("content");
    }
}
