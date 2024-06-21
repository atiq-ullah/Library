namespace Library
{
    public class Book
    {
        public Book(string title, string authors, string key)
        {
            Title = title;
            Authors = authors;
            Key = key;
        }

        public string Title { get; set; }
        public string Authors { get; set; }
        public string Key { get; set; }
    }

}
