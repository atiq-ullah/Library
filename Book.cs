namespace Library
{
    public class Book
    {
        public Book() : this("", "", "") { }
        public Book(string title, string author, string key)
        {
            Title = title;
            AuthorNames = author;
            Key = key;
        }

        public string Title { get; set; }
        public string AuthorNames { get; set; }
        public string Key { get; set; }
    }

}
