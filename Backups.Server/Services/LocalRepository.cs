using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups;
using Backups.FileSystem;

namespace Isu
{
    public class LocalRepository : IFileRepository
    {
        private readonly string _path;

        public LocalRepository(string path)
        {
            _path = path;
        }

        public void AddFile(BackupFile backupFile, string destinationPath)
        {
            string filePath = Path.Combine(destinationPath, backupFile.Name.Name);
            if (System.IO.File.Exists(filePath))
                throw new FileSystemException("File with such name already exists.");

            string absDirPath = Path.Combine(_path, destinationPath);
            string absFilePath = Path.Combine(absDirPath, backupFile.Name.Name);
            Directory.CreateDirectory(absDirPath);
            using FileStream fileStream = System.IO.File.Create(absFilePath);
            fileStream.Write(backupFile.Content.ToArray());
        }

        public BackupFile GetFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
                throw new FileSystemException("File doesnt exist.");
            
            string absFilePath = Path.Combine(_path, filePath);
            using FileStream fileStream = System.IO.File.OpenRead(absFilePath);
            using MemoryStream memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            return new BackupFile(new FileName(Path.GetFileName(filePath)), memoryStream.GetBuffer());
        }
    }
}