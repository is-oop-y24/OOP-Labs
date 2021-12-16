using System.IO;
using System.Linq;

namespace Backups.FileSystem
{
    public class LocalRepository : IFileRepository
    {
        public LocalRepository(string path)
        {
            RepositoryPath = path;
        }

        public string RepositoryPath { get; }

        public void AddFile(BackupFile backupFile, string destinationPath)
        {
            string filePath = Path.Combine(destinationPath, backupFile.Name.Name);
            if (System.IO.File.Exists(filePath))
                throw new FileSystemException("File with such name already exists.");

            string absDirPath = Path.Combine(RepositoryPath, destinationPath);
            string absFilePath = Path.Combine(absDirPath, backupFile.Name.Name);
            Directory.CreateDirectory(absDirPath);
            using FileStream fileStream = System.IO.File.Create(absFilePath);
            fileStream.Write(backupFile.Content.ToArray());
        }

        public BackupFile GetFile(string filePath)
        {
            string absFilePath = Path.Combine(RepositoryPath, filePath);
            if (!System.IO.File.Exists(absFilePath))
                throw new FileSystemException("File doesnt exist.");

            using FileStream fileStream = System.IO.File.OpenRead(absFilePath);
            using MemoryStream memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            return new BackupFile(new FileName(Path.GetFileName(filePath)), memoryStream.GetBuffer());
        }
    }
}