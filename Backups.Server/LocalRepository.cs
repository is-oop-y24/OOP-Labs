using System;
using System.Collections.Generic;
using System.IO;
using Backups;
using Backups.FileSystem;
using File = Backups.FileSystem.File;

namespace Isu
{
    public class LocalRepository : IFileRepository
    {
        private readonly string _realPath;

        public LocalRepository(string path)
        {
            _realPath = path;
        }

        public void AddFile(File file, string path)
        {
            string filePath = Path.Combine(path, file.Name);
            if (System.IO.File.Exists(filePath))
                throw new FileSystemException("File with such name already exists.");

            string absDirPath = Path.Combine(_realPath, path);
            string absFilePath = Path.Combine(absDirPath, file.Name);
            Directory.CreateDirectory(absDirPath);
            using FileStream fileStream = System.IO.File.Create(absFilePath);
            fileStream.Write(file.GetBytes());
        }

        public File GetFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
                throw new FileSystemException("File doesnt exist.");
            
            string absFilePath = Path.Combine(_realPath, filePath);
            using FileStream fileStream = System.IO.File.OpenRead(absFilePath);
            using MemoryStream memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            return new File(new FileName(Path.GetFileName(filePath)), memoryStream.GetBuffer());
        }
    }
}