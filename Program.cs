// See https://aka.ms/new-console-template for more information
using Library;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;

Console.WriteLine("Hello, World!");
Author auth = new Author("JK Rowling");
Book book = new Book("Harry Potter and the Philosophers Stone", auth, "");
Console.WriteLine(book.Title);
Console.WriteLine(book.Author.Name);

string bookSearchUrl = "https://openlibrary.org/search.json?q=the+lord+of+the+rings";

HttpClient sharedClient = new();

Task<string> result = sharedClient.GetStringAsync(bookSearchUrl);
Console.WriteLine(result.Result.ToString());
