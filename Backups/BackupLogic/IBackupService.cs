namespace Backups
{
    public interface IBackupService
    {
        BackupJob CreateJob(string jobName, StorageMode storageMode, string jobPath = null);
        BackupJob GetJob(string jobName);
    }
}