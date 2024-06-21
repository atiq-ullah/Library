using Library;
using Library.Logging;
using Spectre.Console.Cli;
using System.Threading;

Thread loggingThread = new Thread(() =>
{
    ConsoleHelper.CreateNewConsole();
    using (var writer = new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true })
    {
        Console.SetOut(writer);
        Console.WriteLine("Logging to the new console");
        while (true)
        {
            Console.WriteLine($"Log message at {DateTime.Now}");
            Thread.Sleep(1000);
        }
    }
});

loggingThread.Start();
using (var mainWriter = new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true })
{
    Console.SetOut(mainWriter);
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine($"Main thread message {i}");
        Thread.Sleep(2000); // Simulate work in the main thread
    }
}

//  var app = new CommandApp<LibraryCommand>();
//    app.Run(args);
