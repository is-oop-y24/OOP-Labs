using Backups.FileSystem;

namespace Backups
{
    public interface IArchiver
    {
        void AddFile(BackupFile backupFile, string filePath);
        BackupFile MakeArchive(FileName archiveName);
    }
}