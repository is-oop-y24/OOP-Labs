namespace Backups
{
    public class JobObject
    {
        private readonly string _path;

        public JobObject(string path)
        {
            _path = path;
        }

        public string Path => _path;
    }
}