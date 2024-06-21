using Library;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Spectre.Console;
using System;

namespace Library
{
    public class Handlers { 

        public Handlers()
        {
        }

        public static async Task AddHandler(string query)
        {
            List<string> fields = new List<string>() { "key", "title", "author_name" };
            SearchParameters sp = new SearchParameters(fields, query);
            RequestClient client = new RequestClient(sp);
            string result = await client.MakeSearchRequest();

            List<Book> rows = RequestClient.ProcessData(result);
            List<string> selected = TUI.OutputResultMultiSelect(rows);

            foreach (Book item in rows)
                if (selected.Contains($"{item.Title} ({item.Authors})"))
                    DbConnection.AddBook(item.Title, item.Authors);
            AnsiConsole.WriteLine("Successfully added the following books: ");

            foreach (string item in selected)
                AnsiConsole.WriteLine(item);
        }

        public static void RemoveHandler() {
            // Provide list of books with single select
            // Remove DB item on select
            List<Book> bookList = DbConnection.ListBooks();
            List<string> selectedToRemove = TUI.OutputResultMultiSelect(bookList);
            foreach (var item in selectedToRemove)
                DbConnection.RemoveBook(item);
            AnsiConsole.WriteLine("Successfully removed the following books: ");
            foreach (string item in selectedToRemove)
                AnsiConsole.WriteLine(item);
        }

        public static void ListHandler()
        {
            ShowTable();
        }

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
    }
}