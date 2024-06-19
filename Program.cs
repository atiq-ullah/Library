using Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


// Prepare parameters
List<string> fields = new List<string>() { "key", "title", "author_name" };
SearchParameters sp = new SearchParameters(fields, TUI.GetInput());

// Send request and process results
RequestClient client = new RequestClient(sp);
JObject data = JsonConvert.DeserializeObject<JObject>(await client.MakeSearchRequest())!;
string[] rows = RequestClient.ProcessRows(data);

// Output result
string selected = TUI.OutputResult(rows);
Console.WriteLine("Selected: " + selected);
/*
DbConnection conn = new DbConnection();
DbConnection.AddBook("money");
DbConnection.GetBooks();
DbConnection.RemoveBook("silo");
*/
