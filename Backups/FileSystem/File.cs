using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Backups.FileSystem
{
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

        public MemoryStream GetMemoryStream()
        {
            return new MemoryStream(_content, writable: false);
        }
    }
}