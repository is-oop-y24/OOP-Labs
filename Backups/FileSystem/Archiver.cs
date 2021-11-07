using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.FileSystem;
using Microsoft.Win32.SafeHandles;

namespace Backups
{
    public class Archiver : IArchiver
    {
        private readonly List<BackupFile> _files = new List<BackupFile>();

        public void AddFile(BackupFile backupFile)
        {
            if (_files.Exists(archFile => archFile.Name == backupFile.Name))
                throw new FileSystemException("The file with such name is already archived.");
            _files.Add(backupFile);
        }

        public BackupFile MakeArchive(FileName archiveName)
        {
            using var memoryStream = new MemoryStream();
            foreach (BackupFile file in _files)
            {
                using var fileMemoryStream = new MemoryStream(file.Content.ToArray());
                byte[] offsetBytes = BitConverter.GetBytes(fileMemoryStream.Length);
                memoryStream.Write(offsetBytes);
                fileMemoryStream.CopyTo(memoryStream);
            }

            return new BackupFile(archiveName, memoryStream.GetBuffer());
        }
    }
}