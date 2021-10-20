using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Backups.FileSystem
{
    [Serializable]
    public class File
    {
        public File(FileName name, byte[] content)
        {
            Name = name;
            Content = content;
        }

        public FileName Name { get; }
        public byte[] Content { get; set; }
    }
}