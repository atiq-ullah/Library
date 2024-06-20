using Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spectre.Console;

var table = new Table();
table.AddColumn("Title");
table.AddColumn("Author(s)");
DbConnection tableConn = new DbConnection();
List<Book> displayBookList = DbConnection.ListBooks();
foreach (var item in displayBookList)
{
    table.AddRow(item.Title, item.AuthorNames);
}
AnsiConsole.Write(table);

string choice = TUI.DbActionChoice();
// string choice = "Add";

switch (choice)
{
    case "Add":
        List<string> fields = new List<string>() { "key", "title", "author_name" };
        SearchParameters sp = new SearchParameters(fields, TUI.GetInput());
        RequestClient client = new RequestClient(sp);
        JObject data = JsonConvert.DeserializeObject<JObject>(await client.MakeSearchRequest())!;

        List<Book> rows = RequestClient.ProcessRows(data);
        List<string> selected = TUI.OutputResultMultiSelect(rows);
        DbConnection conn = new DbConnection();
        foreach (Book item in rows)
          if (selected.Contains(item.Title)) {
            DbConnection.AddBook(item.Title, String.Join(", ", item.AuthorNames));
          }
        break;
    case "Remove":
        // Provide list of books with single select
        // Remove DB item on select
        List<Book> bookList = DbConnection.ListBooks();
        List<string> selectedToRemove = TUI.OutputResultMultiSelect(bookList);
        foreach (var item in selectedToRemove)
          DbConnection.RemoveBook(item);
        break;
    default:
        break;
        // Will be the same as list
}
/*
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

   DbConnection conn = new DbConnection();
   DbConnection.AddBook("money");
DbConnection.GetBooks();
DbConnection.RemoveBook("silo");

*/
