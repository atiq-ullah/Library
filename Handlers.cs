using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Library
{
    class Handler
    {
        public Handler()
        {

        }

        public static async Task Add()
        {
            List<string> fields = new List<string>() { "key", "title", "author_name" };
            SearchParameters sp = new SearchParameters(fields, TUI.GetInput());
            RequestClient client = new RequestClient(sp);

            JObject data = JsonConvert.DeserializeObject<JObject>(await client.MakeSearchRequest())!;
            string[] rows = RequestClient.ProcessRows(data);
            List<string> selected = TUI.OutputResultMultiSelect(rows);
            DbConnection conn = new DbConnection();
            foreach (string item in selected)
                DbConnection.AddBook(item);
        }
        public static void List()
        {
            List<string> bookList = DbConnection.ListBooks();
            TUI.OutputResult(bookList.ToArray());
        }
        public static List<string> Remove()
        {
            List<string> bookList = DbConnection.ListBooks();
            List<string> selectedToRemove = TUI.OutputResultMultiSelect(bookList.ToArray());
            foreach (var item in selectedToRemove)
                DbConnection.RemoveBook(item);
            return selectedToRemove;
        }
    }
}
