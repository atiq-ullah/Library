namespace Library
{
    public class Book
    {
        public Book(string title, string authors)
        {
            Title = title;
            Authors = authors;
        }

        public string Title { get; set; }
        public string Authors { get; set; }
    }

}
