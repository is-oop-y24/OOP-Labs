using System;
using BackupsExtra.Services.Services;

namespace BackupsExtra.Services.Implementations.Loggers
{
    public class ConsoleLogger : ILogger
    {
        private readonly ILogMessageMaker _messageMaker;

        public ConsoleLogger(ILogMessageMaker messageMaker)
        {
            _messageMaker = messageMaker;
        }
        
        public void Log(string message)
        {
            Console.WriteLine(
                _messageMaker.MakeMessage(message));
        }
    }
}