using Library;
// using System.Net.Http.Json;

// const string bookSearchUrl = "https://openlibrary.org/search.json?q=the+lord+of+the+rings";
HttpClient sharedClient = new HttpClient();

string title = "Harry Potter and the Philosophers Stone";
List<Book> books = new List<Book>();
Author auth = new Author("J.K Rowling", books);
Book b = new Book(title , auth, "", "");
books.Append(b);

Console.WriteLine(books.Count());
