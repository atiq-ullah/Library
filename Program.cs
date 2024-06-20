using Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

string choice = TUI.DbActionChoice();

switch (choice)
{
    case "Add":
        // Ask to input a name of a book
        // Provide list of search results of book
        // Allow for single selection
        // Save book in DB upon selection confirm

        // Prepare parameters
        List<string> fields = new List<string>() { "key", "title", "author_name" };
        SearchParameters sp = new SearchParameters(fields, TUI.GetInput());

        // Send request and process results
        RequestClient client = new RequestClient(sp);
        JObject data = JsonConvert.DeserializeObject<JObject>(await client.MakeSearchRequest())!;
        string[] rows = RequestClient.ProcessRows(data);

        // Output result
        List<string> selected = TUI.OutputResultMultiSelect(rows);
        Console.WriteLine("Selected: " + selected);

        DbConnection conn = new DbConnection();
        foreach (string item in selected)
            DbConnection.AddBook(item);

        break;
    case "Remove":
        // Provide list of books with single select
        // Remove DB item on select
        List<string> bookList = DbConnection.ListBooks();
        string selectedToRemove = TUI.OutputResult(bookList.ToArray());
        DbConnection.RemoveBook(selectedToRemove);
        break;
    case "List":
        // Provide list of books with single select that does nothing for now
        List<string> simpleList = DbConnection.ListBooks();
        string selectedToView = TUI.OutputResult(simpleList.ToArray());
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
