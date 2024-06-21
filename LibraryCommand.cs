using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spectre.Console;
using Spectre.Console.Cli;
using Library;

namespace Library
{
    internal sealed class LibraryCommand : AsyncCommand<LibraryCommand.Actions>
    {
        public sealed class Actions : CommandSettings
        {
            [CommandOption("-a|--add")]
            public string? AddAction { get; init; }

            [CommandOption("-d|--delete")]
            public string? DeleteAction { get; init; }

            [CommandOption("-l|--list")]
            [DefaultValue(false)]
            public bool ListAction { get; init; }

        }

        public override async Task<int> ExecuteAsync([NotNull] CommandContext context,
                                                     Actions actions)
        {
            string? bookToAdd = actions.AddAction ?? "";
            string? bookToRemove = actions.DeleteAction ?? "";

            if (!String.IsNullOrWhiteSpace(bookToAdd))
            {
                AnsiConsole.WriteLine($"Searching for {bookToAdd}...");
                await Run("Add", bookToAdd);
            }
            if (!String.IsNullOrWhiteSpace(bookToRemove))
            {
                AnsiConsole.WriteLine($"Deleting {bookToRemove}...");
                await Run("Remove", null);
            }
            if (actions.ListAction)
            {
                AnsiConsole.WriteLine("Outputting Library...");
                await Run("List", null);
            }

            return 0;
        }


        static async Task Run(string choice, string? query)
        {
            switch (choice)
            {
                case "Add":
                    await Handlers.AddHandler(query!);
                    break;
                case "Remove":

                    break;
                case "List":
                    Handlers.ListHandler();
                    break;
                default:
                    break;
            }
        }
    }
}
