namespace Library
{
  public class Book
  {
    public Book(string title, Author author, string notes, string workId)
    {
      Title = title;
      Author = author;
      Notes = notes;
      WorkId = workId;
    }

    public string Title { get; set; }
    public Author Author { get; set; }
    public string Notes { get; set; }
    public string WorkId { get; set; }
  }

}
