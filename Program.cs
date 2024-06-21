using Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spectre.Console;

static Table CreateTable(List<string> cols)
{
    Table tb = new Table();
    foreach (var col in cols)
        tb.AddColumn(col);
    return tb;
}

static List<Book> GrabBooks()
{
    DbConnection tableConn = new DbConnection();
    List<Book> books = DbConnection.ListBooks();
    return books;
}

static void ShowTable()
{
    List<Book> displayBookList = GrabBooks();
    var tb = CreateTable(new List<string>() { "Title", "Author(s)" });
    foreach (var item in displayBookList)
        tb.AddRow(item.Title, item.Authors);
    AnsiConsole.Write(tb);
}

static async Task Run()
{
    string choice = TUI.DbActionChoice();
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
                if (selected.Contains($"{item.Title} ({item.Authors})"))
                    DbConnection.AddBook(item.Title, item.Authors);
            AnsiConsole.WriteLine("Successfully added the following books: ");
            foreach (string item in selected)
                AnsiConsole.WriteLine(item);
            break;
        case "Remove":
            // Provide list of books with single select
            // Remove DB item on select
            List<Book> bookList = DbConnection.ListBooks();
            List<string> selectedToRemove = TUI.OutputResultMultiSelect(bookList);
            foreach (var item in selectedToRemove)
                DbConnection.RemoveBook(item);
            AnsiConsole.WriteLine("Successfully removed the following books: ");
            foreach (string item in selectedToRemove)
                AnsiConsole.WriteLine(item);
            break;
        case "List":
            ShowTable();
            break;
        default:
            break;
    }
}


await Run();
