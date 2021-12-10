using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            using var archiveDataStream = new MemoryStream();
            foreach (BackupFile file in _files)
            {
                long nameLength = file.Name.Name.Length;
                archiveDataStream.Write(BitConverter.GetBytes(nameLength));
                archiveDataStream.Write(Encoding.Default.GetBytes(file.Name.Name));

                long fileSize = file.Content.Length;
                archiveDataStream.Write(BitConverter.GetBytes(fileSize));
                archiveDataStream.Write(file.Content);
            }

            byte[] content = archiveDataStream.GetBuffer();
            Array.Resize(ref content, (int)archiveDataStream.Position);
            return new BackupFile(archiveName, content);
        }
    }
}