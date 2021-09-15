using System;
using Spectre.Console;

namespace Shops
{
    public class AnsiConsoleUI : IUserInterface
    {
        public void WriteLine(string message)
        {
            AnsiConsole.WriteLine(message);
        }

        public void WriteError(string errorMessage)
        {
            AnsiConsole.Markup($"[red]{errorMessage}[/]");;
        }

        public void ShowTable(Table table)
        {
            AnsiConsole.Render(table);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}