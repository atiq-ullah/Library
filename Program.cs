using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spectre.Console;

string input = GetInput();

string[] split = input.Split(' ');
string joined = String.Join("+", split);
string json = await MakeRequest(joined);
JObject data = JsonConvert.DeserializeObject<JObject>(json)!;
JArray docs = (JArray)data["docs"]!;
JObject firstItem = (JObject)docs[0];

var rows = new List<string>();
foreach (JObject item in docs.Take(100))
{
  var title = item.Property("title")?.Value;
  var authorNames = item.Property("author_name")?.Value;
  var key = item.Property("key")?.Value;

  var rowString = "";

  if (title != null)
  {
    rowString += title.ToString();
  }

  if (authorNames != null)
  {
    rowString += " -- " + String.Join(", ", authorNames);
  }

  rows.Add(rowString);
  
}    
  AnsiConsole.Prompt(
      new SelectionPrompt<string>()
      .Title("Choose one: ")
      .PageSize(10)
      .MoreChoicesText("Move up and down to reveal more")
      .AddChoices(rows.ToArray<string>())
  );


static string GetInput()
{
  return AnsiConsole.Ask<string>("Enter a book [green]name[/]: ");
}

static async Task<string> MakeRequest(string query)
{
  string url = "https://openlibrary.org/search.json?fields=key,title,author_name&q=" + query;
  HttpClient client = new HttpClient();
  string result = await client.GetStringAsync(url);
  return result;
}
