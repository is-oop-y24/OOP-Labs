using System.IO;
using BackupsExtra.Services.Services;

namespace BackupsExtra.Services.Implementations.Loggers
{
    public class FileLogger : ILogger
    {
        private readonly ILogMessageMaker _messageMaker;
        public FileLogger(ILogMessageMaker messageMaker)
        {
            _messageMaker = messageMaker;
        }

        public string LogFilePath { get; }

        public void Log(string message)
        {
            using var streamWriter =
                new StreamWriter(File.OpenWrite(LogFilePath));
            streamWriter.Write(_messageMaker.MakeMessage(message));
        }
    }
}