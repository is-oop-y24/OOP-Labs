using System;

namespace Backups.FileSystem
{
    public class FileName
    {
        private string _name;

        public FileName(string name)
        {
            if (name.Contains('\\'))
                throw new FileSystemException(@"File name cannot contain '\' symbol.");
            if (string.IsNullOrWhiteSpace(name))
                throw new FileSystemException("File name cannot be empty.");
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }
    }
}