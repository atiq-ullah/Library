using Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spectre.Console;
using Spectre.Console.Cli;

var app = new CommandApp<LibraryCommand>();
app.Run(args);
