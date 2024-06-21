
using Newtonsoft.Json;
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

            if (title != null && authorNames != null)
            {
                var authorList = new List<string>();
                foreach (var author in authorNames)
                    authorList.Add(author.ToString());
                return new Book(title, String.Join(", ", authorList));
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

        public static List<Book> ProcessData(string dataToProcess)
        {
            JObject data = JsonConvert.DeserializeObject<JObject>(dataToProcess)!;
            List<Book> rows = RequestClient.ProcessRows(data);
            return rows;
        }
    }
}
