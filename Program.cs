using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Library;
/*
// Prepare parameters
List<string> fields = new List<string>() { "key", "title", "author_name" };
SearchParameters sp = new SearchParameters(fields, TUI.GetInput());

// Send request and process results
RequestClient client = new RequestClient(sp);
JObject data = JsonConvert.DeserializeObject<JObject>(await client.MakeSearchRequest())!;
string[] rows = RequestClient.ProcessRows(data);

// Output result
TUI.OutputResult(rows);
*/

DbConnection conn = new DbConnection();
DbConnection.Connect();
