
using Newtonsoft.Json.Linq;

namespace Library
{
    public class RequestClient
    {
        public RequestClient(SearchParameters sp)
        {
            Parameters = sp;
            Client = new HttpClient();
        }

        public Task<string> MakeSearchRequest()
        {
            return Client.GetStringAsync(GenerateUrl());
        }

        public string GenerateUrl()
        {

            string fields = Parameters.NormalizeFields();
            string query = Parameters.NormalizeQuery();

            return $"{BaseUrl}q={query}&fields={fields}";
        }

        private HttpClient Client;
        public SearchParameters Parameters { get; set; }
        private static string BaseUrl = "https://openlibrary.org/search.json?";

        public static Book ProcessDocument(JObject item)
        {
            Book newBook = new Book();
            var title = item.Property("title")?.Value;
            var authorNames = item.Property("author_name")?.Value;
            var key = item.Property("key")?.Value;

            string authors = "";

            if (title != null) newBook.Title = title.ToString();
            if (authorNames != null)
              foreach (var author in authorNames)
                authors += author.ToString() + ", ";
            if (key != null) newBook.Key = key.ToString();
            newBook.AuthorNames = authors.TrimEnd(',');
            return newBook;
        }

        public static List<Book> ProcessRows(JObject data)
        {
            JArray docs = (JArray)data["docs"]!;
            List<Book> rows = new List<Book>();
            foreach (JObject item in docs.Take(100))
            {
                Book b = ProcessDocument(item);
                rows.Add(b);
            }

            return rows;
        }
    }
}
