using System;

namespace Backups.FileSystem
{
    public class FileName
    {
        public FileName(string name)
        {
            if (name.Contains('\\'))
                throw new FileSystemException(@"File name cannot contain '\' symbol.");
            if (string.IsNullOrWhiteSpace(name))
                throw new FileSystemException("File name cannot be empty.");
            Name = name;
        }

        public string Name { get; }
    }
}