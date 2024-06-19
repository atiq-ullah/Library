using Spectre.Console;

namespace Library
{
    public sealed class TUI
    {

        public static string GetInput()
        {
            return AnsiConsole.Ask<string>("Enter a book [green]name[/]: ");
        }

        public static void OutputResult(string[] rows)
        {
            var selection = AnsiConsole.Prompt(
             new SelectionPrompt<string>()
                  .Title("Choose one: ")
                  .PageSize(10)
                  .MoreChoicesText("Move up and down to reveal more")
                  .AddChoices(rows)
                );

        }

}
}
