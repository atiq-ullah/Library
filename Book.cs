namespace Library
{
    public class Book
    {
        public Book() : this("", "") { }
        public Book(string title, string key)
        {
            Title = title;
            Key = key;
            AuthorNames = new List<string>();
        }

        public string Title { get; set; }
        public List<string> AuthorNames { get; set; }
        public string Key { get; set; }
    }

}
