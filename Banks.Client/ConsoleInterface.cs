using System;

namespace Banks
{
    public class ConsoleInterface : IUserInterface
    {
        public void WriteMessage(params object[] parameters)
        {
            foreach (object parameter in parameters) 
                Console.WriteLine(parameter);
        }

        public string Read()
        {
            return Console.ReadLine();
        }
    }
}