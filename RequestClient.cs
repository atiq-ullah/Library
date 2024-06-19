
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

        public static string ProcessDocument(JObject item)
        {
            var title = item.Property("title")?.Value;
            var authorNames = item.Property("author_name")?.Value;
            var key = item.Property("key")?.Value;
            var rowString = "";

            if (title != null) rowString += title.ToString();
            if (authorNames != null) rowString += " -- " + String.Join(", ", authorNames);

            return rowString;
        }

        public static string[] ProcessRows(JObject data)
        {

            JArray docs = (JArray)data["docs"]!;
            List<string> rows = new List<string>();

            // Output results
            foreach (JObject item in docs.Take(100))
            {
                rows.Add(ProcessDocument(item));
            }

            return rows.ToArray();
        }
    }
}
