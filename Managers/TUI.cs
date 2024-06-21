using Spectre.Console;

namespace Library
{
    public sealed class TUI
    {
        public static string DbActionChoice()
        {
            string[] choices = { "Add", "Remove", "List" };
            var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
              .Title("Choose an action: ")
              .AddChoices(choices)
              );
            return choice;
        }
        public static string GetInput()
        {
            return AnsiConsole.Ask<string>("Enter a book [green]name[/]: ");
        }

        public static List<string> OutputResultMultiSelect(List<Book> rows)
        {
            List<string> sRows = new List<string>();
            foreach (Book b in rows) sRows.Add($"{b.Title} ({b.Authors})");
            var selection = AnsiConsole.Prompt(
             new MultiSelectionPrompt<string>()
                  .Title("Choose one: ")
                  .PageSize(10)
                  .MoreChoicesText("Move up and down to reveal more")
                  .AddChoices(sRows.ToArray())
                );
            return selection;
        }

    }
}
