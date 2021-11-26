using System.Collections.Generic;
using System.IO;
using Backups.FileSystem;

namespace Backups.Tests
{
    public class FileRepositoryMock : IFileRepository
    {
        public Dictionary<string, BackupFile> Files { get; } = new Dictionary<string, BackupFile>();

        public void AddFile(BackupFile backupFile, string destinationPath)
        {
            Files.Add(Path.Combine(destinationPath, backupFile.Name.Name), backupFile);
        }

        public BackupFile GetFile(string filePath)
        {
            if (!Files.ContainsKey(filePath))
                throw new FileSystemException("File doesnt exist");
            return Files[filePath];
        }
    }
}