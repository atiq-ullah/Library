
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

        public static Book? ProcessDocument(JObject item)
        {
            var title = item.Property("title")?.Value.ToString();
            var authorNames = item.Property("author_name")?.Value;
            var key = item.Property("key")?.Value.ToString();
            if (title != null && authorNames != null && key != null)
            {
              var authorList = new List<string>();
              foreach (var author in authorNames)
                authorList.Add(author.ToString());
              return new Book(title, String.Join(", ", authorList), key);
            }
            return null;
        }

        public static List<Book> ProcessRows(JObject data)
        {
            JArray docs = (JArray)data["docs"]!;
            List<Book> rows = new List<Book>();
            foreach (JObject item in docs.Take(100))
            {
                Book? b = ProcessDocument(item);
                if (b != null)
                  rows.Add(b);
            }

            return rows;
        }
    }
}
