using System.Text;

namespace Backups.FileSystem
{
    public interface IFileRepository
    {
        void AddFile(BackupFile backupFile, string path);
        BackupFile GetFile(string filePath);
    }
}