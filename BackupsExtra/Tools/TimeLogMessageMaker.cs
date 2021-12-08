using System;

namespace BackupsExtra
{
    public class TimeLogMessageMaker : ILogMessageMaker
    {
        public string MakeMessage(string message)
        {
            return $"[{DateTime.Now.TimeOfDay}]: " + message;
        }
    }
}