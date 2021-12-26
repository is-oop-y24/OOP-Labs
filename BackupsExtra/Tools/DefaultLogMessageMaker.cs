namespace BackupsExtra
{
    public class DefaultLogMessageMaker : ILogMessageMaker
    {
        public string MakeMessage(string message)
        {
            return message;
        }
    }
}