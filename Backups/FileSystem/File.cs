using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Backups.FileSystem
{
    [Serializable]
    public class File
    {
        private readonly byte[] _content;
        private readonly FileName _name;

        public File(FileName name, byte[] content)
        {
            _name = name;
            _content = content;
        }

        public string Name => _name.GetName();
        public ReadOnlyCollection<byte> Content => Array.AsReadOnly(_content);
    }
}