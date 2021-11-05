using Backups.FileSystem;

namespace Backups
{
    public interface IArchiver
    {
        void AddFile(BackupFile backupFile);
        BackupFile MakeArchive(FileName archiveName);
    }
}