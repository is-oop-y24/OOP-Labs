using System;

namespace Backups.Server
{
    public class Logger : ILogger
    {
        public void Log(string logMessage)
        {
            Console.WriteLine(logMessage);
        }
    }
}