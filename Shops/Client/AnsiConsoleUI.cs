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

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}