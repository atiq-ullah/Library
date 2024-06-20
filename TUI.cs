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

        public static string OutputResult(string[] rows)
        {
            var selection = AnsiConsole.Prompt(
             new SelectionPrompt<string>()
                  .Title("Choose one: ")
                  .PageSize(10)
                  .MoreChoicesText("Move up and down to reveal more")
                  .AddChoices(rows)
                );

            return selection;
        }
        public static List<string> OutputResultMultiSelect(string[] rows)
        {
            var selection = AnsiConsole.Prompt(
             new MultiSelectionPrompt<string>()
                  .Title("Choose one: ")
                  .PageSize(10)
                  .MoreChoicesText("Move up and down to reveal more")
                  .AddChoices(rows)
                );

            return selection;
        }

    }
}
