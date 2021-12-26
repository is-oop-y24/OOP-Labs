namespace BackupsExtra.Services.Services
{
    public interface ILogger
    {
        public ILogMessageMaker MessageMaker { get; }
        void Log(string message);
    }
}