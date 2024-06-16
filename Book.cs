using System;

namespace Library
{
  public class Book
  {
    public Book(string title, Author author, string notes)
    {
      Title = title;
      Author = author;
      Notes = notes;
    }

    public string Title { get; set; }
    public Author Author { get; set; }
    public string Notes { get; set; }
  }

}
