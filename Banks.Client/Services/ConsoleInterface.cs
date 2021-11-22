using System;
using Spectre.Console;

namespace Banks
{
    public class ConsoleInterface : IUserInterface
    {
        public void WriteMessage(params object[] parameters)
        {
            foreach (object parameter in parameters)
                Console.WriteLine(parameter);
        }

        public void ShowTable(Table table)
        {
            AnsiConsole.Write(table);
        }

        public string Read()
        {
            return Console.ReadLine();
        }
    }
}