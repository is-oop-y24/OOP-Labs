using System;
using BackupsExtra.Services.Services;

namespace BackupsExtra.Services.Implementations.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger(ILogMessageMaker messageMaker)
        {
            MessageMaker = messageMaker;
        }

        public ILogMessageMaker MessageMaker { get; }

        public void Log(string message)
        {
            Console.WriteLine(
                MessageMaker.MakeMessage(message));
        }
    }
}