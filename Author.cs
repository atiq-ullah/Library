namespace Library
{
  public class Author
  {
    public Author(string name, List<Book> books)
    {
      Name = name;
      Books = books; 
   }

    public string Name { get; set; }
    public List<Book> Books { get; set; }
  }

}
