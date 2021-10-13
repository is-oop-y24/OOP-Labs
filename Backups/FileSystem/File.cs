namespace Backups.FileSystem
{
    public class File
    {
        private readonly byte[] _content;
        private readonly FileName _name;

        public File(string name, byte[] content)
        {
            _name = new FileName(name);
            _content = content;
        }

        public string Name => _name.GetName();
    }
}