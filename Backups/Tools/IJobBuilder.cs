using Backups.FileSystem;

namespace Backups
{
    public interface IJobBuilder
    {
        void SetDestinationPath(string destinationPath);
        void SetJobName(string jobName);
        void SetFileRepository(IFileRepository fileRepository);
        void SetStoragePacker(IStoragePacker storagePacker);
        IBackupJob GetJob();
    }
}