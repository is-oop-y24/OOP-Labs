using System;
using System.Collections.Generic;
using System.IO;
using Backups.FileSystem;
using Microsoft.Win32.SafeHandles;
using File = Backups.FileSystem.File;

namespace Backups
{
    public class Archiver : IArchiver
    {
        private readonly List<File> _files = new List<File>();

        public void AddFile(File file)
        {
            if (_files.Exists(archFile => archFile.Name == file.Name))
                throw new FileSystemException("The file with such name is already archived.");
            _files.Add(file);
        }

        public File MakeArchive(FileName archiveName)
        {
            using var memoryStream = new MemoryStream();
            foreach (File file in _files)
            {
                using MemoryStream fileMemoryStream = file.GetMemoryStream();
                byte[] offsetBytes = BitConverter.GetBytes(fileMemoryStream.Length);
                memoryStream.Write(offsetBytes);
                fileMemoryStream.CopyTo(memoryStream);
            }

            return new File(archiveName, memoryStream.GetBuffer());
        }
    }
}