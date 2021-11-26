using System.Text;

namespace Backups.FileSystem
{
    public interface IFileRepository
    {
        void AddFile(BackupFile backupFile, string destinationPath);
        BackupFile GetFile(string filePath);
    }
}