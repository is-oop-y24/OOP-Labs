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
        private readonly Dictionary<BackupFile, string> _files = new Dictionary<BackupFile, string>();

        public void AddFile(BackupFile backupFile, string filePath)
        {
            if (_files.ContainsValue(filePath))
                throw new FileSystemException("The file with such name is already archived.");
            _files.Add(backupFile, filePath);
        }

        public BackupFile MakeArchive(FileName archiveName)
        {
            using var archiveDataStream = new MemoryStream();
            foreach (BackupFile file in _files.Keys)
            {
                string path = _files[file];
                long pathLength = path.Length;
                archiveDataStream.Write(BitConverter.GetBytes(pathLength));
                archiveDataStream.Write(Encoding.Default.GetBytes(path));

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