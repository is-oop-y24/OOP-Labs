using System.IO;
using BackupsExtra.Services.Services;

namespace BackupsExtra.Services.Implementations.Loggers
{
    public class FileLogger : ILogger
    {
        public FileLogger(ILogMessageMaker messageMaker, string logFilePath)
        {
            MessageMaker = messageMaker;
            LogFilePath = logFilePath;
        }

        public ILogMessageMaker MessageMaker { get; }
        public string LogFilePath { get; }

        public void Log(string message)
        {
            using var streamWriter =
                new StreamWriter(File.OpenWrite(LogFilePath));
            streamWriter.Write(MessageMaker.MakeMessage(message));
        }
    }
}